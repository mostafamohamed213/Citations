using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class TypeOfSource
    {
        public TypeOfSource()
        {
            ArticleReferences = new HashSet<ArticleReference>();
        }

        public int TypeSourceid { get; set; }
        public string TypeName { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<ArticleReference> ArticleReferences { get; set; }
    }
}
