using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class AuthorResearchField
    {
        public int Authorid { get; set; }
        public int Fieldid { get; set; }

        public virtual Author Author { get; set; }
        public virtual ResearchField Field { get; set; }
    }
}
