using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class PositionJob
    {
        public PositionJob()
        {
            AuthorsPositionInstitutions = new HashSet<AuthorsPositionInstitution>();
        }

        public int PositionJobid { get; set; }
        public string PositionJob1 { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<AuthorsPositionInstitution> AuthorsPositionInstitutions { get; set; }
    }
}
