using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citations.Models
{
    public class articleauthors
    {

        public List<articleauthors1> articleauthors1 { get; set; }

    }
public class articleauthors1
  {
    public int author { get; set; }
    public bool main { get; set; }
}
}
