using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class Magazine
    {
        public Magazine()
        {
            MagazineIssues = new HashSet<MagazineIssue>();
            MagazineResearchFields = new HashSet<MagazineResearchField>();
        }

        public int Magazineid { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string WebsiteUrl { get; set; }
        public int ImpactFactor { get; set; }
        public int ImmediateCoefficient { get; set; }
        public int AppropriateValue { get; set; }
        public int NumberOfCitations { get; set; }
        public int Publisherid { get; set; }
        public int Institutionid { get; set; }
        public bool Active { get; set; }

        public virtual Institution Institution { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<MagazineIssue> MagazineIssues { get; set; }
        public virtual ICollection<MagazineResearchField> MagazineResearchFields { get; set; }
    }
}
