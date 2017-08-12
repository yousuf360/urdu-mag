
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Urdu_Magazine.Models;
using Urdu_Magazine.UserDefinedFunctions;
using static Urdu_Magazine.Models.HelpingModels;

namespace Urdu_Magazine.Controllers
{
    public class HomeController : Controller
    {
        private fyp_dbEntities db = new fyp_dbEntities();
     
        public ActionResult Index()
        {
            ViewBag.Issues = db.Issues.Where(x => x.isPublished == true).ToList();
            return View();
        }

        public ActionResult UploadMedia() {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            ViewBag.UploadedMedia = db.UploadMedias.Where(x => x.userId == UserId).ToList();
            return View();
        }
        [HttpPost]
        public  ActionResult UploadMedia(MediaUpload model) {

            if (Session["User"] == null)
            {
                return Content("login");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;

            if (ModelState.IsValid)
            {

                Dictionary<bool, string> uploadResults = uploadMediaFiles(model.Image);
                string result;
                if (uploadResults == null)
                {
                    return Content("Cannot create directory.");
                }
                else if (uploadResults.TryGetValue(false, out result))
                {
                    ModelState.AddModelError(result, "Cannot upload this file. Try Again.");
                }
                else if (uploadResults.TryGetValue(true, out result))
                {
                    var fileNameAndImagePath = result.Split('>');
                    UploadMedia uploadMedia = new UploadMedia
                    {
                        imgName = model.Name,
                        userId = UserId,
                        imgFileName = fileNameAndImagePath[0],
                        imgPath = fileNameAndImagePath[1]
                    };

                    db.UploadMedias.Add(uploadMedia);
                    db.SaveChanges();
                    return Json(new
                    {
                        message = "success",
                        Id=uploadMedia.Id,
                        file = uploadMedia.imgName,
                        path = uploadMedia.imgPath
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Content("failed");
        }

        private FileOperations fileOp = new FileOperations();
        public Dictionary<bool, string> uploadMediaFiles(HttpPostedFileBase file)
        {
            string fileName = Guid.NewGuid().ToString();
            string path = Server.MapPath("~/Uploads/Images");
            
            var response = fileOp.upload(file, path, fileName);
            if (response.Equals("error"))
            {
                var result = new Dictionary<bool, string>();
                result.Add(false, fileName);
                return result;
            }
            else
            {
                var result = new Dictionary<bool, string>();
                result.Add(true, fileName + ">" + "~/Uploads/Images/"+ fileName + Path.GetExtension(file.FileName));
                return result;
            }


        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}