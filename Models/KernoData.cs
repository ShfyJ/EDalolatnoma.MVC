using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDalolatnoma.MVC.Models
{
     
    public class KernoData:BaseModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Регистрационный номер")]
        [Required(ErrorMessage = "Введите регистрационный номер")]
        public string RegNum { get; set; }

        [Display(Name = "Скважина")]
        [Required(ErrorMessage = "Введите наименование скважины")]
        public int Well_id { get; set; }


        [Display(Name = "Скважина")]
        [ForeignKey("Well_id")]
        public Well Well { get; set; } 


        [Display(Name = "Интервал-1")]
        [Required(ErrorMessage = "Введите интервал")]
        public double Interval_1 { get; set; }


        [Display(Name = "Интервал-2")]
        [Required(ErrorMessage = "Введите интервал")]
       
        public double Interval_2 { get; set; }
       

        [Display(Name = "Поднятия керно")]
        [Required(ErrorMessage = "Введите величину поднятия керно")]
        
        public double Core_raise { get; set; }
     
        [DataType(DataType.Date)]
        [Display(Name = "Дата отбора керна")]
        [Required(ErrorMessage = "Выберите дату отбора керна")]
        public DateTime Date_Selection { get; set; }


        [Display(Name = "Ф.И.О. ответственного за вынос керна")]
        [Required(ErrorMessage = "Введите ответственного за вынос керна")]
        public string PersonName { get; set; }
        
        public string PhotoName { get; set; }
        [Display(Name = "Фото керна")]
        [Required(ErrorMessage = "Добавьте фото керна")]
        [NotMapped]
        public IFormFile Photo { get; set; }

        [NotMapped]
        public List<IFormFile> Files { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Введите величину поднятия керно")]
        public string Core_raise_str { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Введите интервал")]
        public string Interval_1_str { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Введите интервал")]
        public string Interval_2_str { get; set; }

        [NotMapped]
        [Display(Name = "Интервал отбора керна")]
        public string Interval
        {
            get
            {
                return "от "+Interval_1+" до " + Interval_2;
            }
        }

        public ICollection<KernoDataFiles> KernoDataFiles { get; set; } 

    }
}
