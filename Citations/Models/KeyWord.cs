using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class KeyWord
    {
        public KeyWord()
        {
            ArticlesKeywords = new HashSet<ArticlesKeyword>();
        }

        public int KeyWordid { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Remote("checkname", "KeyWords", AdditionalFields = "KeyWordid",
         ErrorMessage = "هذا الإسم موجود من قبل")]
        [Display(Name = "الاسم باللغة العربيه")]
        public string KeyWord1 { get; set; }
        [Display(Name = "الاسم باللغة الانجليزية")]
        public string KeyWordEn { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<ArticlesKeyword> ArticlesKeywords { get; set; }
    }
}
