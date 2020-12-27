using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class MagazineResearchField
    {
        [Display(Name = "المجلة")]
        public int Magazineid { get; set; }
        [Display(Name = "مجال البحث")]
        public int Fieldid { get; set; }

        public virtual ResearchField Field { get; set; }
        public virtual Magazine Magazine { get; set; }
    }
}
