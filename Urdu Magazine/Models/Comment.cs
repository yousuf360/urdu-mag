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
    
    public partial class Comment
    {
        public int Id { get; set; }
        public string comment1 { get; set; }
        public int userId { get; set; }
        public int issueId { get; set; }
        public System.DateTime timestamp { get; set; }
    
        public virtual Issue Issue { get; set; }
        public virtual User User { get; set; }
    }
}
