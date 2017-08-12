using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Urdu_Magazine.Models;
using Urdu_Magazine.UserDefinedFunctions;

namespace Urdu_Magazine.Controllers
{
    public class MagazineController : Controller
    {
        // GET: Magazine
        public ActionResult Index()
        {
            ViewBag.Magazine = db.Issues.ToList();
            return View();
        }
        public ActionResult Create()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
          
            return View();
        }
        private fyp_dbEntities db = new fyp_dbEntities();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MagazineCreateVM model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            if (db.Magazines.Where(m => m.Name == model.Name && m.User_Id == UserId).FirstOrDefault() != null)
                ModelState.AddModelError("Name", Resources.Resource.MagazineNameAlreadyUsed);
           
            if (ModelState.IsValid)
            {
                
                Dictionary<bool, string> uploadResults = uploadMagazineDocuments(model.Cover);
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
                    var folderandCover = result.Split('>');
                    Magazine mag = new Magazine
                    {
                        Name = model.Name,
                        User_Id = UserId,
                        Status = true,
                        Folder=folderandCover[0],
                        Cover=folderandCover[1]                        
                    };
                    
                    db.Magazines.Add(mag);
                    db.SaveChanges();
                    var categories = model.Categories.Remove(model.Categories.Length - 1).Split(',');
                    foreach (var cat in categories)
                    {
                        db.Magazine_Category.Add(new Magazine_Category
                        {
                            magazine_id = mag.Id,
                            category = cat
                        });
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Home");
                }
               
            }
            return View(model);
        }
        private FileOperations fileOp = new FileOperations();
        public Dictionary<bool, string> uploadMagazineDocuments(HttpPostedFileBase file)
        {
            string folderName = Guid.NewGuid().ToString();
            string path = Server.MapPath("~/Uploads/" + folderName);
            bool? directoryFlag = fileOp.createDirectory(path);
            if (directoryFlag == null)
            {
                return null;
            }
            while (directoryFlag == false)
            {

                folderName = Guid.NewGuid().ToString();
                path = Server.MapPath("~/Uploads/" + folderName);
                directoryFlag = fileOp.createDirectory(path);
            }

            //foreach (var key in files.Keys)
            //{
            //    HttpPostedFileBase file;
            //    files.TryGetValue(key, out file);
            //    string fileURL=upload(file, path+"/", key.Name);

            //    if (fileURL.Equals("error"))
            //    {
            //        DeleteDirectory(Path.Combine(Server.MapPath(path)));
            //        Dictionary<bool, string> error = new Dictionary<bool, string>();
            //        error.Add(false, key.Name);
            //        return error;    
            //    }

            //}

            //Dictionary<bool, string> success = new Dictionary<bool, string>();
            //success.Add(true, folderName);
            //return success;
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
                result.Add(true, folderName+">"+ "~/Uploads/" + folderName+"/Cover" +  Path.GetExtension(file.FileName));
                return result;
            }
            

        }
    }
}