using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class ConferenceProceeding
    {
        public ConferenceProceeding()
        {
            ArticleReferences = new HashSet<ArticleReference>();
            ConferenceProceedingsAuthors = new HashSet<ConferenceProceedingsAuthor>();
        }

        public int Conferenceid { get; set; }
        public string Conferencetittle { get; set; }
        public string Year { get; set; }
        public string ConferencePublicationName { get; set; }
        public int? Country { get; set; }
        public int? Publisherid { get; set; }
        public bool Active { get; set; }
        public string Page { get; set; }

        public virtual Country CountryNavigation { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<ArticleReference> ArticleReferences { get; set; }
        public virtual ICollection<ConferenceProceedingsAuthor> ConferenceProceedingsAuthors { get; set; }
    }
}
