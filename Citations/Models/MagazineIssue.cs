using System;
using System.Collections.Generic;

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
        public int Issuenumber { get; set; }
        public int Magazineid { get; set; }
        public int Publisherid { get; set; }
        public DateTime DateOfPublication { get; set; }

        public virtual Magazine Magazine { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<ArticleIssue> ArticleIssues { get; set; }
    }
}
