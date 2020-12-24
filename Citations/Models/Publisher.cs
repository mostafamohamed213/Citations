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
            Magazines = new HashSet<Magazine>();
        }

       
      
     


        public int Publisherid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "اسم المؤلف")]
        public string Name { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "البلد ")]

        public int Country { get; set; }
        [ Display(Name = "المؤسسة ")]
        public int? Institutionid { get; set; }

        [ Display(Name = "نوع الناشر ")]
        public int? TypeOfPublisher { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "العنوان ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "نشط")]
        public bool Active { get; set; }

        public virtual Country CountryNavigation { get; set; }
        public virtual Institution Institution { get; set; }
        public virtual TypeOfPublisher TypeOfPublisherNavigation { get; set; }
        public virtual ICollection<Magazine> Magazines { get; set; }
    }
}
