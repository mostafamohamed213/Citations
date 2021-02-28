using Microsoft.AspNetCore.Mvc;
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
            Books = new HashSet<Book>();
            ConferenceProceedings = new HashSet<ConferenceProceeding>();
            Magazines = new HashSet<Magazine>();
        }

        public int Publisherid { get; set; }
        [Required(ErrorMessage = "اسم الناشر مطلوب"), Display(Name = "اسم الناشر"), Remote("publisherExistsremote", "RemoteValidation", AdditionalFields = "Publisherid")]
        public string Name { get; set; }
        [Display(Name = "المؤسسة")]
        public int? Institutionid { get; set; }
        [Display(Name = "نوع الناشر")]
        public int? TypeOfPublisher { get; set; }
        [Display(Name = "البلد")]
        public int Country { get; set; }
        [Display(Name = "العنوان")]
        [Required(ErrorMessage = "عنوان الناشر مطلوب")]
        public string Address { get; set; }
        [Display(Name = "نشط")]
        public bool Active { get; set; }
        [Display(Name = "البلد")]
        public virtual Country CountryNavigation { get; set; }
        public virtual Institution Institution { get; set; }
        [Display(Name = "نوع الناشر")]
        public virtual TypeOfPublisher TypeOfPublisherNavigation { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<ConferenceProceeding> ConferenceProceedings { get; set; }
        public virtual ICollection<Magazine> Magazines { get; set; }
    }
}
