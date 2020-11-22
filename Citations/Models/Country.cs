using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class Country
    {
        public Country()
        {
            Institutions = new HashSet<Institution>();
            Publishers = new HashSet<Publisher>();
        }

        public int Countryid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Institution> Institutions { get; set; }
        public virtual ICollection<Publisher> Publishers { get; set; }
    }
}
