using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EDalolatnoma.MVC.Models
{
    public class Company:BaseModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите полное наименование организации")]
        [Display(Name = "Полное наименование организации")]
        public string CompanyFullName { get; set; }
        [Required(ErrorMessage = "Введите название предприятия")]
        [Display(Name = "Краткое наименование организации")]
        public string CompanyShortName { get; set; }
        public int Inn { get; set; }



    }
}
