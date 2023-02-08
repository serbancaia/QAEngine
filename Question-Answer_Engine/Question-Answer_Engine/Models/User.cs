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

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Answers = new HashSet<Answer>();
            this.AnswerComments = new HashSet<AnswerComment>();
            this.Questions = new HashSet<Question>();
            this.QuestionComments = new HashSet<QuestionComment>();
        }

        [Required(ErrorMessage = "A user name is required")]
        [StringLength(30, ErrorMessage = "The user name must be between 4 and 30 characters.", MinimumLength = 4)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "A Password is required")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "A Password must be between 4 and 30 characters.", MinimumLength = 4)]
        public string Password { get; set; }
        [Required(ErrorMessage = "A Last name is required")]
        [StringLength(30, ErrorMessage = "A Last name cannot exceed 30 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "A First name is required")]
        [StringLength(30, ErrorMessage = "A First name cannot exceed 30 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "An Email address is required")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "An Email address cannot exceed 50 characters.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A Country is required")]
        [StringLength(100, ErrorMessage = "The country cannot exceed 100 characters.")]
        public string Country { get; set; }
            
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnswerComment> AnswerComments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionComment> QuestionComments { get; set; }
    }
}
