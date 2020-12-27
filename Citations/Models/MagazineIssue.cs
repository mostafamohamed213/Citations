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
            IssueOfIssues = new HashSet<IssueOfIssue>();
        }

        public int Issueid { get; set; }
        [Display(Name = "رقم العدد")]
        [Remote("CheckIssuenumber", "MagazineIssues", AdditionalFields = "Magazineid,Issueid", HttpMethod = "POST", ErrorMessage = "هذا العدد موجود من قبل ")]
        public int Issuenumber { get; set; }
        [Display(Name = "إسم المجلة")]
        public int Magazineid { get; set; }

        public virtual Magazine Magazine { get; set; }
        public virtual ICollection<IssueOfIssue> IssueOfIssues { get; set; }
    }
}
