using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Urdu_Magazine.Models;
using Urdu_Magazine.UserDefinedFunctions;

namespace Urdu_Magazine.Controllers
{
    public class UserController : Controller
    {
        private fyp_dbEntities db = new fyp_dbEntities();


        public ActionResult loadComments(int pageIndex, int pageSize)
        {
         
            if (Session["User"] == null)
            {
                return Content("login");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            var noti = db.get_notification(UserId).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            if (noti.Count == 0) {
                return Content("finished");
            }
            return PartialView(noti);
        }




        // GET: User
        public ActionResult EditProfile() {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            var user = db.Users.Find(UserId);

            UserEditVM userEdit = new UserEditVM
            {
                full_name = user.full_name,
                
            };
            ViewBag.user = user;
            return View(userEdit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserEditVM Model) {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            int UserId = ((Urdu_Magazine.Models.User)Session["User"]).id;
            if (ModelState.IsValid)
            {
                bool passwordIsCorrect = db.Users.Find(UserId).password.Equals(Model.CurrentPassword);
                if (!passwordIsCorrect)
                {
                    ModelState.AddModelError("CurrentPassword", Resources.Resource.PasswordDoesNotMatch);
                }
                if (ModelState.IsValid)
                {

                    if (Model.ProfilePicture == null) {
                        User user = db.Users.Find(UserId);
                        user.full_name = Model.full_name;
                        if (Model.NewPassword != null)
                        {
                            user.password = Model.NewPassword;
                        }
                       
                        db.SaveChanges();
                        Session["User"] = user;
                        return RedirectToAction("EditProfile");
                    }
                    Dictionary<bool, string> uploadResults = uploadProfilePicture(Model.ProfilePicture);
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
                        User user = db.Users.Find(UserId);
                        user.full_name = Model.full_name;
                        if (Model.NewPassword != null) {
                            user.password = Model.NewPassword;
                        }
                        user.profile_picture = fileNameAndImagePath[1];
                        db.SaveChanges();
                        Session["User"] = user;
                        return RedirectToAction("EditProfile");
                    }
                }
            }
            ViewBag.user = db.Users.Find(UserId);
            Model.ProfilePicture = null;
            return View(Model);
        }



        private FileOperations fileOp = new FileOperations();
        public Dictionary<bool, string> uploadProfilePicture(HttpPostedFileBase file)
        {
            string fileName = Guid.NewGuid().ToString();
            string path = Server.MapPath("~/Uploads/UserImages");

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
                result.Add(true, fileName + ">" + "~/Uploads/UserImages/" + fileName + Path.GetExtension(file.FileName));
                return result;
            }


        }







        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "User");
        }
        public ActionResult LogIn()
        {
            if (Session["User"] == null)
            {
                return View("Index");

            }
            else { return RedirectToAction("Index", "Home"); }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(UserLogInVM model)
        {
            if (ModelState.IsValid)
            {
                

                var user = db.Users.Where(u => u.email.Equals(model.LogInEmail) && u.password.Equals(model.LogInPassword)).SingleOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("", Resources.Resource.LoginFailedWithGivenInformation);
                }
                else
                {
                    Session["User"] = user;

                    return RedirectToAction("Index", "Home");
                }
            }
            UserIndex userIndexModel = new UserIndex();
            userIndexModel.logIn = model;
            ViewBag.activeTab = "login";

            return View("Index", userIndexModel);


        }
        public ActionResult SignUp()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index");

            }
            else { return RedirectToAction("Index", "Home"); }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserSignUpVM model)
        {
            if (Session["User"] == null)
            {
                if (ModelState.IsValid)
                {


                    var email = db.Users.Where(e => e.email.ToLower().Equals(model.SignUpEmail.ToLower())).SingleOrDefault();
                    if (email != null)
                    {
                        ModelState.AddModelError("SignUpEmail", Resources.Resource.EmailAlreadyExistsError);
                    }
                    if (ModelState.IsValid)
                    {
                        User user = new User
                        {
                            full_name=model.full_name,
                            email = model.SignUpEmail.ToLower(),
                            password = model.SignUpPassword,
                            status = true
                        };
                        db.Users.Add(user);
                        db.SaveChanges();
                        Session["User"] = user;

                        return RedirectToAction("Index", "Home");

                    }

                }

            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
            UserIndex userIndexModel = new UserIndex();
            userIndexModel.signUp = model;
            ViewBag.activeTab = "signup";
            return View("Index", userIndexModel);
        }
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                UserIndex model = new UserIndex();
                return View(model);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}