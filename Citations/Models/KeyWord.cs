using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class KeyWord
    {
        public KeyWord()
        {
            ArticlesKeywords = new HashSet<ArticlesKeyword>();
        }

        public int KeyWordid { get; set; }
        public string KeyWord1 { get; set; }
        public string KeyWordEn { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<ArticlesKeyword> ArticlesKeywords { get; set; }
    }
}
