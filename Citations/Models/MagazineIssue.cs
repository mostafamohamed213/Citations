using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class MagazineIssue
    {
        public MagazineIssue()
        {
            ArticleIssues = new HashSet<ArticleIssue>();
        }
        
        public int Issueid { get; set; }
        [Display(Name = "رقم العدد")]
        [Remote("CheckIssuenumber", "MagazineIssues", AdditionalFields = "Magazineid", HttpMethod = "POST", ErrorMessage = "هذا العدد موجود من قبل ")]
        public int Issuenumber { get; set; }
        [Display(Name = "إسم المجلة")]
        public int Magazineid { get; set; }
        [Display(Name = "إسم الناشر")]
        public int Publisherid { get; set; }
        [Display(Name = "تاريخ النشر")]
        [DataType(DataType.Date)]
        public DateTime DateOfPublication { get; set; }

        public virtual Magazine Magazine { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<ArticleIssue> ArticleIssues { get; set; }
    }
}
