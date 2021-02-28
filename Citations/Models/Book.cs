using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class Book
    {
        public Book()
        {
            ArticleReferences = new HashSet<ArticleReference>();
            BookAuthores = new HashSet<BookAuthore>();
        }

        public int Bookid { get; set; }
        public string Booktittle { get; set; }
        public string Year { get; set; }
        public int? Country { get; set; }
        public int? Publisherid { get; set; }
        public bool Active { get; set; }
        public string Page { get; set; }

        public virtual Country CountryNavigation { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<ArticleReference> ArticleReferences { get; set; }
        public virtual ICollection<BookAuthore> BookAuthores { get; set; }
    }
}
