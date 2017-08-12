using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Urdu_Magazine.Models
{
    public class MagazineCreateVM
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        [Display(Name = "MagazineDisplay", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Display(Name = "MagazinesCover",ResourceType =typeof(Resources.Resource))]
        public HttpPostedFileBase Cover { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        [Display(Name = "MagazinesCategories", ResourceType = typeof(Resources.Resource))]
        public string Categories { get; set; }
    }
}