using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class Institution
    {
        public Institution()
        {
            AuthorsPositionInstitutions = new HashSet<AuthorsPositionInstitution>();
            Magazines = new HashSet<Magazine>();
            Publishers = new HashSet<Publisher>();
        }

        public int Institutionid { get; set; }
        public string Name { get; set; }
        public int Country { get; set; }
        public int TypeOfInstitution { get; set; }
        public string ScannedCoverImage { get; set; }
        public string ScannedLogoImage { get; set; }
        public int ImpactFactor { get; set; }
        public int PointerH { get; set; }
        public int NumberOfCitations { get; set; }
        public bool? IsPublisher { get; set; }
        public bool Active { get; set; }

        public virtual Country CountryNavigation { get; set; }
        public virtual TypeOfInstitution TypeOfInstitutionNavigation { get; set; }
        public virtual ICollection<AuthorsPositionInstitution> AuthorsPositionInstitutions { get; set; }
        public virtual ICollection<Magazine> Magazines { get; set; }
        public virtual ICollection<Publisher> Publishers { get; set; }
    }
}
