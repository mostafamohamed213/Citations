using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class MagazineIssue
    {
        public MagazineIssue()
        {
            IssueOfIssues = new HashSet<IssueOfIssue>();
        }

        public int Issueid { get; set; }
        public int Issuenumber { get; set; }
        public int Magazineid { get; set; }

        public virtual Magazine Magazine { get; set; }
        public virtual ICollection<IssueOfIssue> IssueOfIssues { get; set; }
    }
}
