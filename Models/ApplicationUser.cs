using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDalolatnoma.MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FioName { get; set; }
        public string Position { get; set; }
        public int Company_id { get; set; }

        
        [NotMapped]
        public string Roles { get; set; }
        [NotMapped]
        public string  Company { get; set; } 
    }
}
