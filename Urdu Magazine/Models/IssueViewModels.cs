using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Urdu_Magazine.Models
{
    public class IssueCreateVM
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SelectError")]
        [Display(Name = "MagazineDisplay", ResourceType = typeof(Resources.Resource))]
        public int Magazine_Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        [Display(Name = "IssueDateDisplay", ResourceType = typeof(Resources.Resource))]
        public System.DateTime Issue_date { get; set; }

        [Display(Name = "IssuesCover", ResourceType =typeof(Resources.Resource))]
        public HttpPostedFileBase Cover { get; set; }

        [Display(Name = "ArticlesSumbissionDate",ResourceType =typeof(Resources.Resource))]
        public Nullable<System.DateTime> Submission_date { get; set; }

        
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        //[Display(Name = "اجراء کے زمرے")]
        //public string Categories { get; set; }
    }
}