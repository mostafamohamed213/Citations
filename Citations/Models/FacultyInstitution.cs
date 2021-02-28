using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class FacultyInstitution
    {
        public FacultyInstitution()
        {
            FacultyInstitutionDepartments = new HashSet<FacultyInstitutionDepartment>();
        }

        public int FacultyInstitutionid { get; set; }
        [Display(Name = "المؤسسة")]
        public int Institutionid { get; set; }
        [Display(Name = "الكلية")]
        public int Facultyid { get; set; }
        [Display(Name = "الكلية")]
        public virtual Faculty Faculty { get; set; }
        [Display(Name = "المؤسسة")]
        public virtual Institution Institution { get; set; }
        public virtual ICollection<FacultyInstitutionDepartment> FacultyInstitutionDepartments { get; set; }
    }
}
