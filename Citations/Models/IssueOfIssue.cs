using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class IssueOfIssue
    {
        public IssueOfIssue()
        {
            Articles = new HashSet<Article>();
        }

        public int IssueOfIssueid { get; set; }
        public string IssuenumberOfIssue { get; set; }
        public int MagazineIssueId { get; set; }
        public DateTime DateOfPublication { get; set; }

        public virtual MagazineIssue MagazineIssue { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
