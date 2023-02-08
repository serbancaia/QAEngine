//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Question_Answer_Engine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Answer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Answer()
        {
            this.AnswerComments = new HashSet<AnswerComment>();
        }
    
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Content is required")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The Content cannot exceed 1000 characters.")]
        public string Content { get; set; }        
        public string UserName { get; set; }
        public System.DateTime Date { get; set; }
        public int Upvotes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnswerComment> AnswerComments { get; set; }
        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
    }
}
