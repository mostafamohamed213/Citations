using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class ArticleReference
    {
        public int ArticleReferencesid { get; set; }
        public int Articleid { get; set; }
        public int? Articlerefid { get; set; }
        public int? Bookid { get; set; }
        public int? Conferenceid { get; set; }
        public int? TypeSourceid { get; set; }

        public virtual Article Article { get; set; }
        public virtual Article Articleref { get; set; }
        public virtual Book Book { get; set; }
        public virtual ConferenceProceeding Conference { get; set; }
        public virtual TypeOfSource TypeSource { get; set; }
    }
}
