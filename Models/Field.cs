using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDalolatnoma.MVC.Models
{
    public class Field:BaseModel
    {
        public int Id { get; set; }
        
        
        [Required(ErrorMessage = "Введите месторождение")]
        [Display(Name = "Месторождение/Площадь")]
        public string FieldName { get; set; }

        
        [Required]
        [Display(Name = "Наименование организации")]
        public int Company_id { get; set; }
        
        
        [ForeignKey("Company_id")]
        public Company Company { get; set; }


        [NotMapped]
        public bool Create { get; set; }=false;
        [NotMapped]
        public bool Edit { get; set; }=false;
        [NotMapped]
        public bool Delete { get; set; }=false ;
    }
}
