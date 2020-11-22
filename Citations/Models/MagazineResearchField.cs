using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class MagazineResearchField
    {
        public int Magazineid { get; set; }
        public int Fieldid { get; set; }

        public virtual ResearchField Field { get; set; }
        public virtual Magazine Magazine { get; set; }
    }
}
