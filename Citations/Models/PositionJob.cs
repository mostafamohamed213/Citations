using Microsoft.AspNetCore.Mvc;
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

        [Required(ErrorMessage = "المسمى الوظيفي مطلوب", AllowEmptyStrings = false), Display(Name = "المسمى الوظيفي"), Remote("positionjobExistsremote", "RemoteValidation", AdditionalFields = "PositionJobid")]
        public string PositionJob1 { get; set; }
        [Display(Name = "نشط")]
        public bool Active { get; set; }

        public virtual ICollection<AuthorsPositionInstitution> AuthorsPositionInstitutions { get; set; }
    }
}
