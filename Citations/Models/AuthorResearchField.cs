using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class AuthorResearchField
    {
        public int Authorid { get; set; }
        public int Fieldid { get; set; }
        [Display(Name = "الاسم ")]
        public virtual Author Author { get; set; }
        [Display(Name = "مجال البحث")]
        public virtual ResearchField Field { get; set; }




    }
}
