using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class ConferenceProceedingsAuthor
    {
        public int Authorid { get; set; }
        public int Conferenceid { get; set; }
        public bool MainAuthor { get; set; }

        public virtual Author Author { get; set; }
        public virtual ConferenceProceeding Conference { get; set; }
    }
}
