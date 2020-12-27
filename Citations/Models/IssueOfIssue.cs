using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

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
        [Display(Name = " رقم الإصدار")]
        [Remote("CheckIssueNum", "IssueOfIssues", AdditionalFields = "MagazineIssueId,IssueOfIssueid", HttpMethod = "POST", ErrorMessage = "هذا الإصدار موجود من قبل ")]
        public string IssuenumberOfIssue { get; set; }
        [Display(Name = " العدد ")]
        public int MagazineIssueId { get; set; }
        [Display(Name = "تاريخ النشر")]
        [DataType(DataType.Date)]
        public DateTime DateOfPublication { get; set; }

        public virtual MagazineIssue MagazineIssue { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
