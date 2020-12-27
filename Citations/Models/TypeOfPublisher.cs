using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class TypeOfPublisher
    {
        public TypeOfPublisher()
        {
            Publishers = new HashSet<Publisher>();
        }

        public int TypePublisherid { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Publisher> Publishers { get; set; }
    }
}
