using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Citations.Models
{
    public partial class Magazine
    {
        public Magazine()
        {
            MagazineIssues = new HashSet<MagazineIssue>();
            MagazineResearchFields = new HashSet<MagazineResearchField>();
        }

        public int Magazineid { get; set; }
        [Remote("CheckName", "Magazines", AdditionalFields = "Magazineid", HttpMethod = "POST", ErrorMessage = "هذا الإسم موجود من قبل ")]
        [Display(Name = "إسم المجلة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Name { get; set; }
        [Remote("CheckIsbn", "Magazines", AdditionalFields = "Magazineid", HttpMethod = "POST", ErrorMessage = "هذا الرقم التسلسلي موجود من قبل ")]
        [Display(Name = "الرقم التسلسلي")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string Isbn { get; set; }
        [Display(Name = "الموقع الالكتروني للمجله")]
        public string WebsiteUrl { get; set; }
        [Display(Name = "معامل التأثير")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int ImpactFactor { get; set; }
        [Display(Name = "المعامل الفوري")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int ImmediateCoefficient { get; set; }
        [Display(Name = "القيمة الملائمة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int AppropriateValue { get; set; }
        [Display(Name = "عدد الاستشهادات")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int NumberOfCitations { get; set; }

        [Display(Name = "الناشر")]
        public int Publisherid { get; set; }
        [Display(Name = "المؤسسة التعليمية")]
        public int Institutionid { get; set; }
        public bool Active { get; set; }

        public virtual Institution Institution { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<MagazineIssue> MagazineIssues { get; set; }
        public virtual ICollection<MagazineResearchField> MagazineResearchFields { get; set; }
        [NotMapped]
        public int[] ResearchFields { get; set; }

   
    }
}
