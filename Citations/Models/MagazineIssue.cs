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
        [Remote("CheckIssuenumber", "MagazineIssues", AdditionalFields = "Magazineid,Issueid", HttpMethod = "POST", ErrorMessage = "هذا العدد موجود من قبل ")]
        [Display(Name = "العدد")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Issuenumber { get; set; }
        public int Magazineid { get; set; }

        public virtual Magazine Magazine { get; set; }
        public virtual ICollection<IssueOfIssue> IssueOfIssues { get; set; }
    }
}
