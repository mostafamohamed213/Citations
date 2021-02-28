using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Citations.Models
{
    public partial class Article
    {
        public Article()
        {
            ArticleAuthores = new HashSet<ArticleAuthore>();
            ArticleReferenceArticlerefs = new HashSet<ArticleReference>();
            ArticleReferenceArticles = new HashSet<ArticleReference>();
            ArticlesKeywords = new HashSet<ArticlesKeyword>();
        }

        public int Articleid { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Remote("checkname", "Articles", AdditionalFields = "Articleid",
        ErrorMessage = "هذا الإسم موجود من قبل")]

        [Display(Name = "عنوان المقال")]
        public string Articletittle { get; set; }

        [Remote("checkenname", "Articles", AdditionalFields = "Articleid",
         ErrorMessage = "هذا الإسم موجود من قبل")]
        [Display(Name = "عنوان المقال باللغة الانجليزيه")]
        public string ArticletittleEn { get; set; }

        [Display(Name = "المقال")]
        public string ScannedArticlePdf { get; set; }
        [Display(Name = "النبذه المختصره")]
        public string BriefQuote { get; set; }
        [Display(Name = "النبذه المختصره باللغة الانجليزيه")]
        public string BriefQuoteEn { get; set; }
        [Display(Name = "عدد الاستشهادات")]
        public int? NumberOfCitations { get; set; }
        [Display(Name = "عدد المراجع")]
        public int? NumberOfReferences { get; set; }
        [Display(Name = "اصدار العدد للمقال")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int ArticleIssue { get; set; }
        public bool Active { get; set; }
        [Display(Name = "الصفحة")]
        public string Page { get; set; }

        public virtual IssueOfIssue ArticleIssueNavigation { get; set; }
        public virtual ICollection<ArticleAuthore> ArticleAuthores { get; set; }
        public virtual ICollection<ArticleReference> ArticleReferenceArticlerefs { get; set; }
        public virtual ICollection<ArticleReference> ArticleReferenceArticles { get; set; }
        public virtual ICollection<ArticlesKeyword> ArticlesKeywords { get; set; }
        [NotMapped]
        public int[] KeyWords { get; set; }
    }
}
