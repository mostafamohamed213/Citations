using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name="إسم المجلة")]
        public string Name { get; set; }
        [Display(Name = "الرقم التسلسلي")]
        public string Isbn { get; set; }
        [Display(Name = "معامل التأثير")]
        public int ImpactFactor { get; set; }
        [Display(Name = "المعامل الفوري")]
        public int ImmediateCoefficient { get; set; }
        [Display(Name = "القيمة الملائمة")]
        public int AppropriateValue { get; set; }
        [Display(Name = "عدد الإقتباسات")]
        public int NumberOfCitations { get; set; }
        [Display(Name = "المؤسسة التعليمية")]
        public int Institutionid { get; set; }
        [Display(Name = "نشط ؟")]
        public bool Active { get; set; }

        public virtual Institution Institution { get; set; }
        public virtual ICollection<MagazineIssue> MagazineIssues { get; set; }
        public virtual ICollection<MagazineResearchField> MagazineResearchFields { get; set; }
    }
}
