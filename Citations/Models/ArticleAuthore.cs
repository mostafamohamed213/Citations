using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class ArticleAuthore
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "إسم المؤلف")]
        public int Authorid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "إسم المقالة")]
        public int Articleid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "الكاتب الرئيسي")]
        public bool MainAuthor { get; set; }


        public virtual Article Article { get; set; }
        public virtual Author Author { get; set; }
    }
}
