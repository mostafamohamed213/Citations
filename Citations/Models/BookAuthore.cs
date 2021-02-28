using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class BookAuthore
    {
        public int Authorid { get; set; }
        public int Bookid { get; set; }
        public bool MainAuthor { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
