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
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "اسم المؤلف")]
        public string Name { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "البلد ")]
        public int Country { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "العنوان ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "نشط")]
        public bool Active { get; set; }

        public virtual Country CountryNavigation { get; set; }
        public virtual ICollection<MagazineIssue> MagazineIssues { get; set; }
    }
}
