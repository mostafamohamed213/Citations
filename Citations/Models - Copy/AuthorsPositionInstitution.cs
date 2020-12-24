using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class AuthorsPositionInstitution
    {
        public int AuthorsPositionInstitutionId { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = " المؤلف")]
        public int Authorid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = " المؤسسة التعليمية")]
        public int Institutionid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int PositionJobid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب"), Display(Name = " المؤسسة التعليمية الرئيسية")]
        public bool MainIntitute { get; set; }

        public virtual Author Author { get; set; }
        public virtual Institution Institution { get; set; }
        public virtual PositionJob PositionJob { get; set; }
    }
}
