using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Urdu_Magazine.Helpers;
using Urdu_Magazine.Models;

namespace Urdu_Magazine.Controllers
{
    public class ArticleController : Controller
    {
        

        private fyp_dbEntities db = new fyp_dbEntities();


        public JsonResult getArticleJsonForClassification(int articleId,int issueId) {
            var article = db.Articles.Where(x => x.Id == articleId).Select(x => new {
                data = x.article_data,
                writer = x.User.full_name,
                columns = x.columns,
                name = x.name,
                ad = db.inPageAds.Where(y => y.articleId == articleId && y.issueId==issueId).FirstOrDefault().html

            }).FirstOrDefault();
            
            return Json(article,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Rate(int? articleId, decimal rate) {
            if (Session["User"] == null)
            {
                return Content("login");
            }
           
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            Article_Rating article_rating = db.Article_Rating.Where(x => x.user_Id == UserId && x.article_Id == articleId).SingleOrDefault();
            if (article_rating != null)
            {
                article_rating.rate = rate;
                db.Entry(article_rating).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else {
                db.Article_Rating.Add(new Article_Rating
                {
                    article_Id = articleId.Value,
                    user_Id = UserId,
                    rate = rate
                });
                db.SaveChanges();
            }
            return Content("success");
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (Session["User"] == null)
            {
                return Content("login");
            }
            if (id == null)
            {
                return Content("null id");
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return Content("article not found");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            if (article.User_Id != UserId) {
                return Content("no rights");
            }
            if (!article.Available_To_Select) {
                return Content("Cannot be deleted because published!");
            }
            //db.Article_Issue.RemoveRange(article.Article_Issue);
            db.Article_Submit_Info.RemoveRange(article.Article_Submit_Info);
            db.Articles.Remove(article);
            db.SaveChanges();
            return Content("success");
        }
        public ActionResult List()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            ViewBag.Articles = db.Articles.Where(x => x.User_Id == UserId)
                .OrderBy(x=>x.Id)
                .ToList();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveArticleToPreview(string Id,string articleName, string articleData) {
            if (Session["User"] == null)
            {
                return Content("login");
            }
            var article = db.PreviewAricles.Find(Id);
            if (article == null)
            {
                db.PreviewAricles.Add(new PreviewAricle {
                    Id = Id,
                    article_name=articleName,
                    article_data=articleData
                });
                db.SaveChanges();
            }
            else
            {
                article.article_name = articleName;
                article.article_data = articleData;
                db.Entry(article).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Content("success");
        }
        public ActionResult previewArticle(string Id,int? columns) {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            if (columns==null)
            {
                ViewBag.columns = 1;
            }
            else
            {
                if (columns < 1)
                {
                    ViewBag.columns = 1;
                }
                else
                {
                    ViewBag.columns = columns;
                }
            }
            return PartialView(db.PreviewAricles.Find(Id));
        }
        public ActionResult previewArticleForEditor(string Id, int? columns,string writerName)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            if (columns == null)
            {
                ViewBag.columns = 1;
            }
            else
            {
                if (columns < 1)
                {
                    ViewBag.columns = 1;
                }
                else
                {
                    ViewBag.columns = columns;
                }
            }
            ViewBag.writerName = writerName;
            return PartialView(db.PreviewAricles.Find(Id));
        }

        public ActionResult getArticleToRead(int Id) {
            if (Session["User"] == null)
            {
                 return RedirectToAction("Index", "User");
            }
            return PartialView(db.Articles.Find(Id));
        }



        //this action finally allows editor to save articles for his particular issue
        [HttpPost]
        public ActionResult submitArticles(int issueid, IEnumerable<int> ArticlesChecked,IEnumerable<int> AllArticleIds)
        {
            
            if (Session["User"] == null)
            {
                return Content("login");
            }
            User user = ((Urdu_Magazine.Models.User)Session["User"]);
            int UserId = user.id;
            var username = user.full_name;
            var profilepic = user.profile_picture;

            var userIds = new List<int>();

            var AllArticlesSubmittedForIssue = db.Article_Submit_Info.Where(x => x.SubmittedForIssue == issueid && AllArticleIds.Contains(x.Article_Id)).ToList();

            //Ye khuwari sirf null exception na aye is waja se kri hai.
            if (ArticlesChecked == null)
            {
                foreach (var item in AllArticlesSubmittedForIssue)
                {
                    if (!item.Article.Available_To_Select)
                    {
                        db.Articles.Find(item.Article_Id).Available_To_Select = true;
                        db.Article_Issue.RemoveRange(db.Article_Issue.Where(x => x.Issue_Id == issueid && x.Article_Id == item.Article_Id));
                        
                    }
                }
                db.Notifications.RemoveRange(db.Notifications.Where(x => x.issue_id == issueid && x.type == 5));
            }
            else
            {
               
                foreach (var item in AllArticlesSubmittedForIssue)
                {
                    if (ArticlesChecked.Contains(item.Article_Id))
                    {
                        if (item.Article.Available_To_Select)
                        {
                            db.Article_Issue.Add(new Article_Issue
                            {
                                Issue_Id = issueid,
                                Article_Id = item.Article_Id,
                                In_category=item.In_Category
                            });
                            db.Articles.Find(item.Article_Id).Available_To_Select = false;
                            Notification tempNot = new Notification
                            {
                                article_id = item.Article_Id,
                                date = DateTime.Now,
                                issue_id = issueid,
                                who = UserId,
                                type = 5,

                            };
                            db.Notifications.Add(tempNot);


                            userIds.Add(item.Article.User_Id);
                          
                        }
                    }
                    else
                    {
                        if (!item.Article.Available_To_Select)
                        {
                            db.Articles.Find(item.Article_Id).Available_To_Select = true;
                            db.Article_Issue.RemoveRange(db.Article_Issue.Where(x => x.Issue_Id == issueid && x.Article_Id == item.Article_Id));
                            db.Notifications.Remove(db.Notifications.FirstOrDefault(x=>x.type==5 && x.issue_id == issueid && x.article_id == item.Article_Id));
                        }
                    }
                  
                }
                
            }
            db.SaveChanges();
            Task task = new Task(delegate
            {
                userIds.RemoveAll(x => x == UserId);
                var userIdsGroups = userIds.Distinct().Select(x => "user-id-" + x).ToList();
                LiveConnectivityHelper liveConnectivityHelper = new LiveConnectivityHelper();
                liveConnectivityHelper.sendNotification(userIdsGroups, 5, username, profilepic, DateTime.Now.ToString(), "#");
            });
            task.Start();
            //foreach(var item in Articles)
            //{
            //    if (db.Articles.Find(item).Available_To_Select)
            //    {
            //        db.Article_Issue.Add(new Article_Issue
            //        {
            //            Issue_Id = issueid,
            //            Article_Id = item
            //        });
            //        db.Articles.Find(item).Available_To_Select = false;
            //    }
            //}
            //db.SaveChanges();
            return Content("success");
        }



        public ActionResult getSubmittedArticles(int issueId)
        {
            if (Session["User"] == null)
            {
                return Content("login");
            }
            ViewBag.issueId = issueId;
            List<Article> articles = db.Article_Submit_Info.Where(a => a.SubmittedForIssue == issueId && a.Article.Available_To_Select==true).Select(a=>a.Article).ToList();
            articles.AddRange(db.Article_Issue.Where(x => x.Issue_Id == issueId).Select(x => x.Article));
            if (articles.Count == 0)
                return Content("no article");
            return PartialView(articles);
        }


        
        public ActionResult SubmitArticle()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            ViewBag.Articles = db.Articles
                .Where(a => a.User_Id == UserId && a.Available_To_Select==true)
                .OrderBy(a => a.Id)
                .ToList();
            return View();
        }
        [HttpPost]
        public ActionResult SubmitArticle(int articleId,int issueId, int catId)
        {
            if (Session["User"] == null)
            {
                return Content("login");
            }
            User user = ((Urdu_Magazine.Models.User)Session["User"]);
            int UserId = user.id;
            var username = user.full_name;
            var profilepic = user.profile_picture;
            var article = db.Articles.Find(articleId);
            if(article.Number_Of_Submissions==5)
            {
                return Content("limitreached");
            }
            db.Article_Submit_Info.Add(new Article_Submit_Info {
                Article_Id=articleId,
                SubmittedForIssue=issueId,
                In_Category = catId,
                Available=true
            });
            article.Number_Of_Submissions++;
            db.SaveChanges();
            
            Task task = new Task(delegate {
                Notification tempNot = new Notification
                {
                    who = UserId,
                    issue_id = issueId,
                    article_id = articleId,
                    date = DateTime.Now,
                    link = "~/Issue/publishIssue",
                    type = 4,
                    //description = user.id+" "

                };
                db.Notifications.Add(tempNot);
                db.SaveChanges();
                var editorId = db.Issues.Find(issueId).Magazine.User_Id;
                if (editorId != issueId)
                {
                    var userIds = new List<int>();
                    userIds.Add(editorId);
                    var userIdsGroups = userIds.Distinct().Select(x => "user-id-" + x).ToList();

                    LiveConnectivityHelper liveConnectivityHelper = new LiveConnectivityHelper();
                    liveConnectivityHelper.sendNotification(userIdsGroups, 4, username, profilepic, tempNot.date.ToString(), "~/Issue/publishIssue");
                }
            });
            task.Start();


            return Content("success");
        }


        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            //var IssueData = db.Issues.Select(x => new
            //{
            //    Id = x.Id,
            //    Name = x.Magazine.Name + " (" + x.Issue_date.ToString() + ")"
            //});
            //ViewBag.Issue_Id = new SelectList(IssueData, "Id", "Name");
            ViewBag.UploadedMedia = db.UploadMedias.Where(x => x.userId == UserId).ToList();
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleViewModel model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            if (ModelState.IsValid)
            {
                Article article = new Article
                {
                    article_data = model.article_data,
                    name = model.name,
                    User_Id = UserId,
                    Available_To_Select = true,
                    columns = Convert.ToByte(model.columns),
                    
                   
                };
                db.Articles.Add(article);
                db.SaveChanges();
                //db.Article_Submit_Info.Add(new Article_Submit_Info
                //{
                //    Article_Id = article.Id,
                //    SubmittedForIssue = model.Issue_Id,
                //    Available = true
                //});
                //db.SaveChanges();
                return RedirectToAction("List", "Article");
            }
            //var IssueData = db.Issues.Select(x => new
            //{
            //    Id = x.Id,
            //    Name = x.Magazine.Name + " (" + x.Issue_date.ToString() + ")"
            //});
            //ViewBag.Issue_Id = new SelectList(IssueData, "Id", "Name");
            ViewBag.UploadedMedia = db.UploadMedias.Where(x => x.userId == UserId).ToList();
            return View(model);

        }


        public ActionResult Edit(int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            Article article = db.Articles.Find(id);

            if (article == null)
            {
                return HttpNotFound();
            }
            if (article.User_Id != UserId)
            { // so that no other user can edit this
                return HttpNotFound();
            }
            ArticleViewModel articleVM = new ArticleViewModel
            {
                article_data = article.article_data,
               
                columns = article.columns,
                name = article.name,
                Id=article.Id
            };
            ViewBag.UploadedMedia = db.UploadMedias.Where(x => x.userId == UserId).ToList();
            return View(articleVM);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleViewModel model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            if (ModelState.IsValid)
            {
                Article article = db.Articles.Find(model.Id);
                article.name = model.name;
                article.columns = Convert.ToByte(model.columns);
                article.article_data = model.article_data;
              
                db.SaveChanges();
                return RedirectToAction("List", "Article");
            }
            ViewBag.UploadedMedia = db.UploadMedias.Where(x => x.userId == UserId).ToList();
            return View(model);
        }




        public ActionResult GetPartialViewByIssueId(int Id)
        {
            var articleList = db.Issues.Find(Id).Article_Issue.Select(x => x.Article).OrderBy(x=>x.Article_Issue.ElementAt(0).Magazine_Category.category).ThenBy(x=>x.name).ToList();  
            return PartialView(articleList);
        }
        public ActionResult GetPartialViewByIssueIdForPdf(int Id)
        {
            ViewBag.inPageAds = db.inPageAds.Where(x=>x.issueId==Id).ToList();
            ViewBag.fullPageAds = db.fullPageAds.Where(x => x.issueId == Id).ToList();
            var articleList = db.Issues.Find(Id).Article_Issue.Select(x => x.Article).OrderBy(x => x.Article_Issue.ElementAt(0).Magazine_Category.category).ThenBy(x => x.name).ToList();
            return PartialView(articleList);
        }
        public ActionResult GetPartialViewWithTurnJs(int Id)
        {
            var articleList = db.Issues.Find(Id).Article_Issue.Select(x => x.Article).OrderBy(x => x.Article_Issue.ElementAt(0).Magazine_Category.category).ThenBy(x => x.name).ToList();
            return PartialView(articleList);
        }
    }
}