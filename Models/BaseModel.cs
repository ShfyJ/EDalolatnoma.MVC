using System;

namespace EDalolatnoma.MVC.Models
{
    public class BaseModel
    {
        public string CreateBy { get; set; }=string.Empty;  
        public DateTime CreateAt { get; set; }= DateTime.Now;
        public string UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
