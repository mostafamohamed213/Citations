using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class AuthorsPositionInstitution
    {
        public int AuthorsPositionInstitutionId { get; set; }
        public int Authorid { get; set; }
        public int Institutionid { get; set; }
        public int PositionJobid { get; set; }
        public bool MainIntitute { get; set; }

        public virtual Author Author { get; set; }
        public virtual Institution Institution { get; set; }
        public virtual PositionJob PositionJob { get; set; }
    }
}
