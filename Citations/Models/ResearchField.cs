using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

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
        [Display(Name = "م")]
        public int Fieldid { get; set; }
        [Display(Name = "مجال البحث")]
        [Remote("CheckField", "ResearchFields", AdditionalFields = "Fieldid", HttpMethod = "POST", ErrorMessage = "مجال البحث موجود من قبل ")]
        public string Name { get; set; }
        //[Display(Name = "مجال البحث باللغة الإنجليزية")]
        //public string NameEn { get; set; }
        [Display(Name = "نشط ؟")]
        public bool Active { get; set; }

        public virtual ICollection<AuthorResearchField> AuthorResearchFields { get; set; }
        public virtual ICollection<MagazineResearchField> MagazineResearchFields { get; set; }
    }
}
