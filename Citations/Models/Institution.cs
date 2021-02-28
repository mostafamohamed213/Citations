using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Citations.Models
{
    public partial class Institution
    {
        public Institution()
        {
            FacultyInstitutions = new HashSet<FacultyInstitution>();
            Magazines = new HashSet<Magazine>();
            Publishers = new HashSet<Publisher>();
        }

        public int Institutionid { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Remote("checkname", "Institutions", AdditionalFields = "Institutionid",
        ErrorMessage = "هذا الإسم موجود من قبل")]
        [Display(Name = "المؤسسة")]
        public string Name { get; set; }
        [Display(Name = "البلد")]
        public int Country { get; set; }
        [Display(Name = "نوع المؤسسة")]
        public int TypeOfInstitution { get; set; }
        [Display(Name = "صورة الغلاف")]
        public string ScannedCoverImage { get; set; }
        [Display(Name = "صورة اللوجو")]
        public string ScannedLogoImage { get; set; }
        [Display(Name = "معامل التأثير")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int ImpactFactor { get; set; }
        [Display(Name = "المؤشر H")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int PointerH { get; set; }
        [Display(Name = "عدد الاستشهادات")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int NumberOfCitations { get; set; }
        public bool? IsPublisher { get; set; }
        public bool Active { get; set; }

        [Display(Name = "البلد")]
        public virtual Country CountryNavigation { get; set; }
        [Display(Name = "نوع المؤسسة")]
        public virtual TypeOfInstitution TypeOfInstitutionNavigation { get; set; }
        public virtual ICollection<FacultyInstitution> FacultyInstitutions { get; set; }
        public virtual ICollection<Magazine> Magazines { get; set; }
        public virtual ICollection<Publisher> Publishers { get; set; }
    }
}
