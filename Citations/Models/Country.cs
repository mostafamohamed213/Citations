using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class Country
    {
        public Country()
        {
            Books = new HashSet<Book>();
            ConferenceProceedings = new HashSet<ConferenceProceeding>();
            Institutions = new HashSet<Institution>();
            Publishers = new HashSet<Publisher>();
        }

        public int Countryid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Remote("checkname", "Countries", AdditionalFields = "Countryid",
        ErrorMessage = "هذا الإسم موجود من قبل")]
        [Display(Name = "البلد")]
        public string Name { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<ConferenceProceeding> ConferenceProceedings { get; set; }
        public virtual ICollection<Institution> Institutions { get; set; }
        public virtual ICollection<Publisher> Publishers { get; set; }
    }
}
