using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDalolatnoma.MVC.Models
{
    public class Well:BaseModel
    {
        public int Id { get; set; }
       
        
        [Required(ErrorMessage = "Введите название скважины")]
        [Display(Name = "Наименование скважины")]
        public string WellName { get; set; }
        
        
        [Required(ErrorMessage = "Выберите месторождение")]
        [Display(Name = "Месторождение")]
        public int Field_id { get; set; }
        
        
        [ForeignKey("Field_id")]
        public Field Field { get; set; }


        [Display(Name = "Состояние скважины")]
        public int WellStatus_id { get; set; }


        [ForeignKey("WellStatus_id")]
        public WellStatus WellStatus { get; set; }

    }
}
