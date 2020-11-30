using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class ArticlesKeyword
    {
        public int Articleid { get; set; }
        public int KeyWordid { get; set; }

        public virtual Article Article { get; set; }
        public virtual KeyWord KeyWord { get; set; }
    }
}
