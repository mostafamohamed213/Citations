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
            BookAuthores = new HashSet<BookAuthore>();
            ConferenceProceedingsAuthors = new HashSet<ConferenceProceedingsAuthor>();
        }

        public int Authorid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب", AllowEmptyStrings = false), Display(Name = "الاسم")]
        public string Name { get; set; }
        [Display(Name = "الصورة")]
        public string ScannedAuthorimage { get; set; }
        [Display(Name = "الحالة")]
        public string AuthorBio { get; set; }
        [Display(Name = "معامل H"), Required(ErrorMessage = "هذا الحقل مطلوب", AllowEmptyStrings = false)]
        public int PointerH { get; set; }
        [Display(Name = "نشط")]
        public bool Active { get; set; }

        public virtual ICollection<ArticleAuthore> ArticleAuthores { get; set; }
        [Display(Name = "مجالات البحث")]
        public virtual ICollection<AuthorResearchField> AuthorResearchFields { get; set; }
        [Display(Name = "الموقع الوظيفي")]
        public virtual ICollection<AuthorsPositionInstitution> AuthorsPositionInstitutions { get; set; }
        public virtual ICollection<BookAuthore> BookAuthores { get; set; }
        public virtual ICollection<ConferenceProceedingsAuthor> ConferenceProceedingsAuthors { get; set; }
    }
}
