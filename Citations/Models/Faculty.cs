using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            FacultyInstitutions = new HashSet<FacultyInstitution>();
        }
        public int Facultyid { get; set; }
        [Required(ErrorMessage = "اسم الكلية مطلوب"), Display(Name = "الكلية"), Remote(action: "FacultyExistsremote", controller: "RemoteValidation", AdditionalFields = "Facultyid")]
        public string Name { get; set; }
        [Display(Name = "نشط")]
        public bool Active { get; set; }
        [Display(Name = "الجامعات")]
        public virtual ICollection<FacultyInstitution> FacultyInstitutions { get; set; }
    }
}
