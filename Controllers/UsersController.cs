using EDalolatnoma.MVC.Data;
using EDalolatnoma.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EDalolatnoma.MVC.Controllers
{

[Authorize]
public class UsersController : Controller
{


    private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
            _context = context; 
    }
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
            foreach (var item in allUsersExceptCurrentUser)
            {
                var company = _context.Company.Find(item.Company_id);
                if (company != null) item.Company = company.CompanyShortName;    
                var roles = await _userManager.GetRolesAsync(item);
                if (roles != null) item.Roles = string.Join(", ", roles);
            }

        return View(allUsersExceptCurrentUser);
    }
}
}