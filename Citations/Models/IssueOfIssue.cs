using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Remote("CheckIssueNum", "IssueOfIssues", AdditionalFields = "MagazineIssueId,IssueOfIssueid", HttpMethod = "POST", ErrorMessage = "هذا الإصدار موجود من قبل ")]
        [Display(Name = "الاصدار")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string IssuenumberOfIssue { get; set; }
        public int MagazineIssueId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "تاريخ النشر")]
        public DateTime DateOfPublication { get; set; }

        public virtual MagazineIssue MagazineIssue { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
