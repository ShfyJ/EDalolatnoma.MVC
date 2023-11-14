using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDalolatnoma.MVC.Models
{
public class ManageUserRolesViewModel
{
    public string UserId { get; set; }
    public IList<UserRolesViewModel> UserRoles { get; set; }
    
        
       
    
    }

public class UserRolesViewModel
{
    public string RoleName { get; set; }
    public bool Selected { get; set; }
}
}