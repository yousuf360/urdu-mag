using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Urdu_Magazine.Models
{
    public class HelpingModels
    {
        public class issueInfoForClassification {
            public int articleId { get; set; }
            public string name { get; set; }
            public string writer { get; set; }
        }
        public class FullPageAds {

            public int issueId { get; set; }
            public List<fullPageAd> fullPage { get; set; }
        }
        public class MediaUpload
        {
            [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
            [Display(Name = "FileName", ResourceType = typeof(Resources.Resource))]
            public string Name { get; set; }


            [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
            [Display(Name = "SelectFile", ResourceType = typeof(Resources.Resource))]
            public HttpPostedFileBase Image { get; set; }
        }
    }
}