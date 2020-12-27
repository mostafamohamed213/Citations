using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class PositionJob
    {
        public PositionJob()
        {
            AuthorsPositionInstitutions = new HashSet<AuthorsPositionInstitution>();
        }

        public int PositionJobid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string PositionJob1 { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = "نشط")]
        public bool Active { get; set; }

        public virtual ICollection<AuthorsPositionInstitution> AuthorsPositionInstitutions { get; set; }
    }
}
