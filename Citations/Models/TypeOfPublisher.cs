using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "هذا الحقب  مطلوب", AllowEmptyStrings = false), Display(Name = "نوع الناشر"), Remote("typepublisherExistsremote", "RemoteValidation", AdditionalFields = "TypePublisherid")]

        public string TypeName { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Publisher> Publishers { get; set; }
    }
}
