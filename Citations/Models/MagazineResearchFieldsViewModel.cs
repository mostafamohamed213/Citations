using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Citations.Models
{
    public class MagazineResearchFieldsViewModel
    {
        [Display(Name = "إسم المجلة")]
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
        [Display(Name = "م")]
        public int Fieldid { get; set; }
        [Display(Name = "مجال البحث")]
        public string Name1 { get; set; }

    }
}
