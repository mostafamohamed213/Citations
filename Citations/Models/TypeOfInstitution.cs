using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class TypeOfInstitution
    {
        public TypeOfInstitution()
        {
            Institutions = new HashSet<Institution>();
        }

        public int TypeInstitutionid { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Institution> Institutions { get; set; }
    }
}
