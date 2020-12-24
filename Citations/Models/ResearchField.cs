using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public string NameEn { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<AuthorResearchField> AuthorResearchFields { get; set; }
        public virtual ICollection<MagazineResearchField> MagazineResearchFields { get; set; }
    }
}
