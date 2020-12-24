using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class AuthorResearchField
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "إسم المؤلف")]
        public int Authorid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = " مجالات البحث")]
        public int Fieldid { get; set; }

        public virtual Author Author { get; set; }
        public virtual ResearchField Field { get; set; }
    }
}
