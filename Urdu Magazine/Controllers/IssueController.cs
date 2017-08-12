using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Urdu_Magazine.Models;
using Urdu_Magazine.UserDefinedFunctions;
using NReco.PdfGenerator;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Urdu_Magazine.Helpers;

namespace Urdu_Magazine.Controllers
{


    public class IssueController : Controller
    {
        private fyp_dbEntities db = new fyp_dbEntities();

        public ActionResult insertClassificationInPageAdsPartialView(int Id)//issueid
        {
            if (Session["user"] == null) {
                return Content("login");
            }
            var articleList = db.Issues.Find(Id).Article_Issue.Select(x => x.Article).OrderBy(x => x.Article_Issue.ElementAt(0).Magazine_Category.category).ThenBy(x => x.name).Select(x => new HelpingModels.issueInfoForClassification
            {
                articleId = x.Id,
                name = x.name,
                writer = x.User.full_name
            }).ToList();
            if (articleList.Count == 0) {
                return Content("selectArticles");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            ViewBag.UploadedMedia = db.UploadMedias.Where(x => x.userId == UserId).ToList();
            
            return PartialView(articleList);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult insertClassificationInPageAds(inPageAd data)
        {
            if (Session["user"] == null)
            {
                return Content("login");
            }
            db.Database.ExecuteSqlCommand("Delete from inPageAds where articleId={0}", new object[] { data.articleId });

            if (data.html != null)
            {
                db.inPageAds.Add(new inPageAd
                {
                    issueId = data.issueId,
                    articleId = data.articleId,
                    html = data.html
                });
                db.SaveChanges();
            }


            return Content("done");
        }





        public ActionResult insertClassificationFullPageAds(int Id) {
            if (Session["User"] == null)
            {
                return Content("<script> top.window.location.href=\""+Url.Action("Index", "User")+"\"</script>");
            }
           // var articleList = db.Issues.Find(Id).Article_Issue.Select(x => x.Article).OrderBy(x => x.category).ThenBy(x => x.name).ToList();
            var articleList = db.Issues.Find(Id).Article_Issue.Select(x => x.Article).OrderBy(x => x.Article_Issue.ElementAt(0).Magazine_Category.category).ThenBy(x => x.name).ToList();
            if (articleList.Count == 0)
            {
                return Content(Resources.Resource.ChooseArticles);
            }

            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            ViewBag.UploadedMedia = db.UploadMedias.Where(x => x.userId == UserId).ToList();
         
            return PartialView(articleList);
        }
    
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult insertClassificationFullPageAds(HelpingModels.FullPageAds data) {
       
            db.Database.ExecuteSqlCommand("Delete from fullPageAds where issueId={0}", new object[] { data.issueId });
            if (data.fullPage != null)
            {
                foreach (var item in data.fullPage)
                {
                    db.fullPageAds.Add(item);
                }
            }
            db.SaveChanges();
            return Content("done");
        }
        // GET: Issue
        public ActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public ActionResult postComment(int issueId,string commentText) {
            if (Session["User"] == null)
            {
                return Content("login");
            }

            var user = ((Urdu_Magazine.Models.User)Session["User"]);
            var username = user.full_name;
            var profilepic = user.profile_picture;
            var userId = user.id;

            var date = DateTime.Now;
            Comment comment = new Comment
            {
                comment1 = commentText,
                issueId = issueId,
                userId = user.id,
                timestamp = date
            };
            db.Comments.Add(comment);
            db.SaveChanges();
            Task task = new Task(delegate
            {
                db.Notifications.Add(new Notification
                {
                    who = userId,
                    issue_id = issueId,
                    date = date,
                    link = "~/Issue/Webview/" + issueId,
                    type =1,
                    //description = user.id+" "
                    
                });
                db.SaveChanges();
                var userIds = db.Article_Issue.Where(a => a.Issue_Id == issueId).Select(x => x.Article.User.id).ToList(); //To get all writers;
                var PublisheruserId = db.Issues.Find(issueId).Magazine.User_Id;
                userIds.Add(PublisheruserId);
                userIds.RemoveAll(x=>x==userId);
//                var userIds = db.Database.ExecuteSqlCommand("Delete from inPageAds where articleId={0}", new object[] { data.articleId });
                







                var liveConnectivity = GlobalHost.ConnectionManager.GetHubContext<LiveConnectivity>();
                liveConnectivity.Clients.Group("web-view-" + issueId).addComment(username, (profilepic==null)?"default": profilepic, commentText, date.ToString());

                var userIdsGroups = userIds.Distinct().Select(x => "user-id-" + x).ToList();
                LiveConnectivityHelper liveConnectivityHelper = new LiveConnectivityHelper();
                liveConnectivityHelper.sendNotification(userIdsGroups, 1, username, profilepic, date.ToString(), "~/Issue/Webview/" + issueId);
                //liveConnectivity.Clients.Groups(userIdsGroup).showNotification(username, (profilepic == null) ? "default" : profilepic,);

            });
            task.Start();
            return Content("success");
           

        }
    


        //Ye method issues lekar aata hai jab koi apna article sub krna aata hai us k liye
        public ActionResult getIssues(string magazineName,int articleId)
        {
            if (Session["User"] == null)
            {
                return Content("login");
            }
            //Is there a better way? Recheck kro code ko
            int[] issueIds = db.Article_Submit_Info.
                Where(a => a.Available == true 
                && a.Article_Id == articleId)
                .Select(a => a.SubmittedForIssue).ToArray();

            if (magazineName.Equals(""))
            {
                return PartialView(db.Issues.
                    Where(i => i.Submission_date > DateTime.Now
                    && !issueIds.Contains(i.Id) && i.isPublished==false
                    ).ToList());
            }

            return PartialView(db.Issues.
                Where(i=>i.Magazine.Name.Contains(magazineName) 
                && i.Submission_date > DateTime.Now
                && !issueIds.Contains(i.Id) && i.isPublished == false
                ).ToList());
        }





        public ActionResult CallForArticles()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            ViewBag.Issues = db.Issues
                .Where(i => i.Magazine.User_Id == UserId && i.isPublished == false)
                .OrderBy(i => i.Issue_date)
                .ThenBy(i => i.Magazine.Name)
                .ToList();
            return View();
        }


        [HttpPost]
        public ActionResult CallForArticle(int IssueId,DateTime submissionDate,string action)
        {



            if (Session["User"] == null)
            {
                return Content("login");
            }

            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;

            if (action.Equals("call") || action.Equals("change"))
            {
                db.Issues.Find(IssueId).Submission_date = submissionDate;
                db.SaveChanges();
                
                Task task = new Task(delegate {
                    if (action.Equals("change")) {
                        var entities = db.Notifications.Where(x => x.issue_id == IssueId && x.type == 2);
                        db.Notifications.RemoveRange(entities);
                        db.SaveChanges();
                    }
                    db.Notifications.Add(new Notification
                    {
                        who = UserId,
                        issue_id = IssueId,
                        date = DateTime.Now,
                        link = "~/Article/SubmitArticle",
                        type = 2,
                        //description = user.id+" "

                    });
                    db.SaveChanges();
                });
                task.Start();
            }
            else if (action.Equals("stop"))
            {
                db.Issues.Find(IssueId).Submission_date = null;
                db.SaveChanges();
                Task task = new Task(delegate {
                    var entities = db.Notifications.Where(x => x.issue_id == IssueId && x.type == 2);
                    db.Notifications.RemoveRange(entities);
                    db.SaveChanges();
                });
                task.Start();
            }
           
           
            return Content("success");
        }
   
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            ViewBag.Magazine_Id = new SelectList(db.Magazines.Where(i => i.User_Id == UserId).ToList(), "Id", "Name");
            
            return View();
        }
        private FileOperations fileOp = new FileOperations();
        public Dictionary<bool, string> uploadIssueDocuments(HttpPostedFileBase file, string magfolder)
        {
            string folderName = Guid.NewGuid().ToString();
            string path = Server.MapPath("~/Uploads/"+ magfolder + "/" + folderName);
            bool? directoryFlag = fileOp.createDirectory(path);
            if (directoryFlag == null)
            {
                return null;
            }
            while (directoryFlag == false)
            {

                folderName = Guid.NewGuid().ToString();
                path = Server.MapPath("~/Uploads/" + magfolder + "/" + folderName);
                directoryFlag = fileOp.createDirectory(path);
            }

     
            var response = fileOp.upload(file, path, "Cover");
            if (response.Equals("error"))
            {
                var result = new Dictionary<bool, string>();
                result.Add(false, "Cover");
                return result;
            }
            else
            {
                var result = new Dictionary<bool, string>();
                result.Add(true, magfolder + "/" + folderName + ">" + "~/Uploads/" + magfolder + "/" + folderName + "/Cover" + Path.GetExtension(file.FileName));
                return result;
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IssueCreateVM model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            if (ModelState.IsValid)
            {
                string magFolder = db.Magazines.Find(model.Magazine_Id).Folder;
                Dictionary<bool, string> uploadResults = new Dictionary<bool, string>();
                if (model.Cover != null)
                {
                    uploadResults = uploadIssueDocuments(model.Cover, magFolder);
                }
                else
                {
                    string folderName = Guid.NewGuid().ToString();
                    string path = Server.MapPath("~/Uploads/" + magFolder + "/" + folderName);
                    bool? directoryFlag = fileOp.createDirectory(path);
                    if (directoryFlag == null)
                    {
                        return null;
                    }
                    while (directoryFlag == false)
                    {

                        folderName = Guid.NewGuid().ToString();
                        path = Server.MapPath("~/Uploads/" + magFolder + "/" + folderName);
                        directoryFlag = fileOp.createDirectory(path);
                    }
                    String dummyResult = magFolder + "/" + folderName+">"+ db.Magazines.Find(model.Magazine_Id).Cover;
                    uploadResults.Add(true, dummyResult);
                
                }
                string result;
                if (uploadResults == null)
                {
                    return Content("Cannot create directory.");
                }
                else if (uploadResults.TryGetValue(false, out result))
                {
                    ModelState.AddModelError(result, Resources.Resource.CannotUploadThisFileTryAgain);
                }
                else if (uploadResults.TryGetValue(true, out result))
                {
                    var folderandCover = result.Split('>');
                    Issue issue = new Issue
                    {
                        Magazine_Id = model.Magazine_Id,
                        Issue_date = model.Issue_date,
                        Status = true,
                        Folder = folderandCover[0],
                        Cover = folderandCover[1],
                        Submission_date=model.Submission_date,
                        isPublished=false

                    };
                    db.Issues.Add(issue);
                    db.SaveChanges();
                    //var categories = model.Categories.Remove(model.Categories.Length - 1).Split(',');
                    //foreach (var cat in categories)
                    //{
                    //    db.Issue_Category.Add(new Issue_Category
                    //    {
                    //        Issue_id = issue.Id,
                    //        category = cat
                    //    });
                    //    db.SaveChanges();
                    //}
                    return RedirectToAction("Index", "Home");
                }
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            ViewBag.Magazine_Id = new SelectList(db.Magazines.Where(i => i.User_Id == UserId).ToList(), "Id", "Name");
            return View(model);
        }


        public ActionResult WebView(int Id)
        {
            ViewBag.Issue = db.Issues.Find(Id);
            return View();
        }
        public ActionResult publishIssue()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            ViewBag.Issues = db.Issues
              .Where(i => i.Magazine.User_Id == UserId)
              .OrderBy(i => i.isPublished)
              .ThenBy(i => i.Issue_date)
              .ThenBy(i => i.Magazine.Name)
              .ToList();
            return View();
        }



        public ActionResult Download(int Id)
        {

            string file = db.Issues.Where(x => x.Id == Id).Select(x => x.Folder + "/" + x.fileName).SingleOrDefault();
            string path = Server.MapPath("~/Uploads/" + file);
            string contentType = "application/pdf";
            return File(path, contentType, Path.GetFileName(file));
        }






        [HttpPost]
        public ActionResult publishIssue(int issueId)
        {
            if (Session["User"] == null)
            {
                return Content("login");
            }
            if (db.Issues.Find(issueId).Article_Issue.Count > 0)
            {


                User user= ((Urdu_Magazine.Models.User)Session["User"]);
                int UserId = user.id;
                var username = user.full_name;
                var profilepic = user.profile_picture;
                Issue issue = db.Issues.Find(issueId);
                issue.isPublished = true;
                issue.pdfStatus = 2;
                db.Entry(issue).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Task task = new Task(delegate {
                    string filename = makePDF(issueId);
                    Notification tempNot = new Notification
                    {
                        who = UserId,
                        issue_id = issueId,
                        date = DateTime.Now,
                        link = "~/Issue/Webview/" + issueId,
                        type = 3,
                        //description = user.id+" "

                    };
                    db.Notifications.Add(tempNot);
                    db.SaveChanges();
                    var userIds = db.Article_Issue.Where(a => a.Issue_Id == issueId).Select(x => x.Article.User.id).ToList(); //To get all writers/users to notify;
                  
                   
                    userIds.RemoveAll(x => x == UserId); //jis ki waja se notification arahi hai usko kata do 
                    var userIdsGroups = userIds.Distinct().Select(x => "user-id-" + x).ToList(); //distinct lagaya and ids ko groups k format mae le aye
                    LiveConnectivityHelper liveConnectivityHelper = new LiveConnectivityHelper();
                    liveConnectivityHelper.sendNotification(userIdsGroups, 3, username, profilepic, tempNot.date.ToString(), "~/Issue/Webview/" + issueId);
                });
                task.Start();
                return Content("published");

            }
            return Content("selectArticles");
        }

        //public ActionResult makeIronPdf()
        //{

        //    HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
        //    HtmlToPdf.PrintOptions.FitToPaperWidth = true;
        //    HtmlToPdf.PrintOptions.EnableJavaScript = true;
        //    HtmlToPdf.PrintOptions.RenderDelay = 5500;
        //    HtmlToPdf.PrintOptions.MarginBottom = 0;
        //    HtmlToPdf.PrintOptions.MarginLeft = 0;
        //    HtmlToPdf.PrintOptions.MarginRight = 0;
        //    HtmlToPdf.PrintOptions.MarginTop = 0;
        //    PdfResource PDF = HtmlToPdf.RenderUrlAsPdf(new Uri("http://localhost:3541/Article/GetPartialViewByIssueIdForPdf?Id=46"));

        //    PDF.SaveAs(@"C:\Users\yousu\Desktop\File.Pdf");
        //    return Content("Done");
        //}




        public string makePDF(int Id)
        {
            string fileName = Guid.NewGuid().ToString();
            Issue issue = db.Issues.Find(Id);
            string path = Server.MapPath("~/Uploads/" + issue.Folder + "/" + fileName);
            var htmlToPdf = new HtmlToPdfConverter();
            htmlToPdf.Margins = new PageMargins { Top = 0, Bottom = 0, Left = 0, Right = 0 };
            htmlToPdf.CustomWkHtmlArgs = "--encoding UTF-8 --javascript-delay 5500";
            htmlToPdf.Size = PageSize.A4;

            //htmlToPdf.CustomWkHtmlArgs = "  --print-media-type ";
            //string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            //Console.WriteLine(baseUrl);
            string url = "http://"+"localhost/fyp"+ "/Article/GetPartialViewByIssueIdForPdf/" + Id;
            
            htmlToPdf.GeneratePdfFromFile(url, null, path + ".Pdf");

            issue.fileName = fileName + ".PDF";
            issue.pdfStatus = 1;
            db.Entry(issue).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return fileName;
        }




        [HttpPost]
        public ActionResult unPublishIssue(int issueId)
        {
            if (Session["User"] == null)
            {
                return Content("login");
            }
            db.Issues.Find(issueId).isPublished = false;
            db.SaveChanges();
            Task task = new Task(delegate {
                var entities = db.Notifications.Where(x => x.issue_id == issueId && x.type == 3);
                db.Notifications.RemoveRange(entities);
                db.SaveChanges();
            });
            task.Start();
            return Content("unpublished");

        }
    }
}