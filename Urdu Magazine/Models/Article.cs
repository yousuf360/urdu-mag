//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Urdu_Magazine.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            this.Article_Issue = new HashSet<Article_Issue>();
            this.Article_Rating = new HashSet<Article_Rating>();
            this.Article_Submit_Info = new HashSet<Article_Submit_Info>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string article_data { get; set; }
        public int User_Id { get; set; }
        public bool Available_To_Select { get; set; }
        public byte columns { get; set; }
        public byte Number_Of_Submissions { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article_Issue> Article_Issue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article_Rating> Article_Rating { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article_Submit_Info> Article_Submit_Info { get; set; }
    }
}
