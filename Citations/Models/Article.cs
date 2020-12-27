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
            ArticlesKeywords = new HashSet<ArticlesKeyword>();
        }

        public int Articleid { get; set; }
       [Required]
        public string Articletittle { get; set; }
        public string ArticletittleEn { get; set; }
        public string ScannedArticlePdf { get; set; }
        [Required]
        public string BriefQuote { get; set; }
        public string BriefQuoteEn { get; set; }
        public int NumberOfCitations { get; set; }
        public int NumberOfReferences { get; set; }
        public int ArticleIssue { get; set; }
        public bool Active { get; set; }

        public virtual IssueOfIssue ArticleIssueNavigation { get; set; }
        public virtual ICollection<ArticleAuthore> ArticleAuthores { get; set; }
        public virtual ICollection<ArticlesKeyword> ArticlesKeywords { get; set; }
        [NotMapped]
        public int[] KeyWords { get; set; }
    }
}
