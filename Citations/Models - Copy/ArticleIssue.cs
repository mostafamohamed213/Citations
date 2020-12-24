using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class ArticleIssue
    {
        public int Articleid { get; set; }
        public int MagazineIssueId { get; set; }

        public virtual Article Article { get; set; }
        public virtual MagazineIssue MagazineIssue { get; set; }
    }
}
