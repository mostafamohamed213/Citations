using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class TypeOfInstitution
    {
        public TypeOfInstitution()
        {
            Institutions = new HashSet<Institution>();
        }

        public int TypeInstitutionid { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Remote("checkname", "TypeOfInstitutions", AdditionalFields = "TypeInstitutionid",
        ErrorMessage = "هذا الإسم موجود من قبل")]
        [Display(Name = "نوع المؤسسة")]
        public string TypeName { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Institution> Institutions { get; set; }
    }
}
