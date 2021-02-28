using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class FacultyInstitutionDepartment
    {
        public FacultyInstitutionDepartment()
        {
            AuthorsPositionInstitutions = new HashSet<AuthorsPositionInstitution>();
        }

        public int FacultyInstitutionDepartmentid { get; set; }
        [Display(Name = "الكلية")]
        public int FacultyInstitutionid { get; set; }
        [Display(Name = "القسم")]
        public int Departmentid { get; set; }
        [Display(Name = "القسم")]
        public virtual Department Department { get; set; }
        [Display(Name = "الكلية")]
        public virtual FacultyInstitution FacultyInstitution { get; set; }
        public virtual ICollection<AuthorsPositionInstitution> AuthorsPositionInstitutions { get; set; }
    }
}
