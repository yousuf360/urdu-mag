using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Urdu_Magazine.Models
{
    public class UserLogInVM
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailRequiredError")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailValidError")]
        [Display(Name = "EmailDisplay", ResourceType = typeof(Resources.Resource))]
        public string LogInEmail { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordDisplay", ResourceType = typeof(Resources.Resource))]
        public string LogInPassword { get; set; }
    }
    public class UserSignUpVM
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailRequiredError")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "EmailValidError")]
        [Display(Name = "EmailDisplay", ResourceType = typeof(Resources.Resource))]
        public string SignUpEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        [Display(Name = "FullName", ResourceType = typeof(Resources.Resource))]
        public string full_name { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordDisplay", ResourceType = typeof(Resources.Resource))]
        public string SignUpPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPasswordDisplay", ResourceType = typeof(Resources.Resource))]
        [System.ComponentModel.DataAnnotations.Compare("SignUpPassword", ErrorMessageResourceName = "ConfirmPasswordError",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public string SignUpConfirmPassword { get; set; }

    }

    public class UserEditVM
    {
        [Display(Name = "ProfilePicture", ResourceType = typeof(Resources.Resource))]
        public HttpPostedFileBase ProfilePicture { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        [Display(Name = "FullName", ResourceType = typeof(Resources.Resource))]
        public string full_name { get; set; }




        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(Resources.Resource))]
        public string CurrentPassword { get; set; }



       
        [DataType(DataType.Password)]
        [Display(Name = "NewPasswordDisplay", ResourceType = typeof(Resources.Resource))]
        public string NewPassword { get; set; }




        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPasswordDisplay", ResourceType = typeof(Resources.Resource))]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessageResourceName = "ConfirmPasswordError",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public string NewPasswordConfirm { get; set; }

    }



    public class UserIndex
    {
        public UserIndex()
        {
            this.signUp = new UserSignUpVM();
            this.logIn = new UserLogInVM();
        }
        public UserSignUpVM signUp { get; set; }
        public UserLogInVM logIn { get; set; }
    }
}