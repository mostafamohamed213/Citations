using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class Author
    {
        public Author()
        {
            ArticleAuthores = new HashSet<ArticleAuthore>();
            AuthorResearchFields = new HashSet<AuthorResearchField>();
            AuthorsPositionInstitutions = new HashSet<AuthorsPositionInstitution>();
        }

        public int Authorid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "إسم المؤلف")]
        public string Name { get; set; }
        [Display(Name = "اختر صورة")]
        public string ScannedAuthorimage { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "الحالة")]
        public string AuthorBio { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "معامل H")]
        public int PointerH { get; set; }
        [Display(Name = "نشط")]
        public bool Active { get; set; }

        public virtual ICollection<ArticleAuthore> ArticleAuthores { get; set; }
        public virtual ICollection<AuthorResearchField> AuthorResearchFields { get; set; }
        public virtual ICollection<AuthorsPositionInstitution> AuthorsPositionInstitutions { get; set; }
    }
}
