using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            MagazineIssues = new HashSet<MagazineIssue>();
        }

        public int Publisherid { get; set; }
        [Display(Name = "إسم الناشر")]
        public string Name { get; set; }
        public int Country { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }

        public virtual Country CountryNavigation { get; set; }
        public virtual ICollection<MagazineIssue> MagazineIssues { get; set; }
    }
}
