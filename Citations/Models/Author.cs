using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class Author
    {
        public Author()
        {
            ArticleAuthores = new HashSet<ArticleAuthore>();
            AuthorResearchFields = new HashSet<AuthorResearchField>();
        }

        public int Authorid { get; set; }
        public string Name { get; set; }
        public string ScannedAuthorimage { get; set; }
        public string AuthorBio { get; set; }
        public int PointerH { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<ArticleAuthore> ArticleAuthores { get; set; }
        public virtual ICollection<AuthorResearchField> AuthorResearchFields { get; set; }
    }
}
