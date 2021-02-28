using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class Department
    {
        public Department()
        {
            FacultyInstitutionDepartments = new HashSet<FacultyInstitutionDepartment>();
        }

        public int Departmentid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب", AllowEmptyStrings = false), Display(Name = "الاسم")]
        public string Name { get; set; }
        [Display(Name = "نشط")]
        public bool Active { get; set; }

        public virtual ICollection<FacultyInstitutionDepartment> FacultyInstitutionDepartments { get; set; }
    }
}
