using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDalolatnoma.MVC.Models
{
    public class KernoDataFiles
    {

        public int Id { get; set; }
        public int KernoData_id { get; set; } 
        [ForeignKey("KernoData_id")]
        public KernoData KernoData { get; set; }
        public string FileName { get; set; }
    }
}
