using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class ArticleAuthore
    {
        public int Authorid { get; set; }
        public int Articleid { get; set; }
        public bool MainAuthor { get; set; }

        public virtual Article Article { get; set; }
        public virtual Author Author { get; set; }
    }
}
