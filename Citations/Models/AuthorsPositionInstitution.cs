using System;
using System.Collections.Generic;

#nullable disable

namespace Citations.Models
{
    public partial class AuthorsPositionInstitution
    {
        public int AuthorsPositionInstitutionId { get; set; }
        public int Authorid { get; set; }
        public int FacultyInstitutionDepartmentid { get; set; }
        public int PositionJobid { get; set; }
        public bool MainIntitute { get; set; }

        public virtual Author Author { get; set; }
        public virtual FacultyInstitutionDepartment FacultyInstitutionDepartment { get; set; }
        public virtual PositionJob PositionJob { get; set; }
    }
}
