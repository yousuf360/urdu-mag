using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Urdu_Magazine.Models
{
    public class ArticleViewModel
    {
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SelectError")]
        //[Display(Name = "IssueDisplay", ResourceType = typeof(Resources.Resource))]
        //public int Issue_Id { get; set; }
        public int Id { get; set; }

       
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        [Display(Name = "ArticleNameDisplay", ResourceType = typeof(Resources.Resource))]
        public string name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        [Range(1, 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "InvalidNumber")]
        [Display(Name = "NumberOfColumnsDisplay", ResourceType = typeof(Resources.Resource))]
        public int columns { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "Required")]
        [Display(Name = "ArticleDataDisplay", ResourceType = typeof(Resources.Resource))]
        public string article_data { get; set; }
    }
    public class SelectArticleToIssueViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SelectError")]
        [Display(Name = "IssueDisplay", ResourceType = typeof(Resources.Resource))]
        public int Issue_Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "SelectError")]
        [Display(Name = "SubmittedArticlesListDisplay", ResourceType = typeof(Resources.Resource))]
        public IEnumerable<int> Articles { get; set; }
    }
}