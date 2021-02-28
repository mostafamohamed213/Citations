using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class ResearchField
    {
        public ResearchField()
        {
            AuthorResearchFields = new HashSet<AuthorResearchField>();
            MagazineResearchFields = new HashSet<MagazineResearchField>();
        }

        public int Fieldid { get; set; }
        [Remote("CheckField", "ResearchFields", AdditionalFields = "Fieldid", HttpMethod = "POST", ErrorMessage = "مجال البحث موجود من قبل ")]
        [Display(Name = "مجال البحث باللغة العربيه")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Name { get; set; }
        [Remote("CheckEnField", "ResearchFields", AdditionalFields = "Fieldid", HttpMethod = "POST", ErrorMessage = "مجال البحث موجود من قبل ")]
        [Display(Name = "مجال البحث باللغة الانجليزيه")]
        public string NameEn { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<AuthorResearchField> AuthorResearchFields { get; set; }
        public virtual ICollection<MagazineResearchField> MagazineResearchFields { get; set; }
    }
}
