using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class Article
    {
        public Article()
        {
            ArticleAuthores = new HashSet<ArticleAuthore>();
            ArticleIssues = new HashSet<ArticleIssue>();
        }

        public int Articleid { get; set; }
        public string Articletittle { get; set; }
        public string ScannedArticlePdf { get; set; }
        public string ScannedBriefQuote { get; set; }
        public int NumberOfCitations { get; set; }
        public int NumberOfReferences { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<ArticleAuthore> ArticleAuthores { get; set; }
        public virtual ICollection<ArticleIssue> ArticleIssues { get; set; }
    }
}
