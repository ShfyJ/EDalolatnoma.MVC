using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EDalolatnoma.MVC.Data;
using EDalolatnoma.MVC.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using EDalolatnoma.MVC.Constants;
using Microsoft.AspNetCore.Identity;

namespace EDalolatnoma.MVC.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notyf;
        private readonly IAuthorizationService _authorizationService;
/*        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;*/
        public CompaniesController(ApplicationDbContext context, INotyfService notyf,
                                    IAuthorizationService authorizationService,
                                    SignInManager<ApplicationUser> signInManager,
                                    UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _notyf = notyf;
            _authorizationService = authorizationService;
        }

        // GET: Companies

        [Authorize(Permissions.Company.View)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Company.ToListAsync());
        }

        // GET: Companies/Details/5
        [Authorize(Permissions.Company.View)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        [HttpGet]
        [Authorize(Permissions.Company.Create)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Company.Create)]
        public async Task<IActionResult> Create([Bind("Id,CompanyFullName,CompanyShortName, Inn")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company); await _context.SaveChangesAsync();
                _notyf.Success("Успешно сохранено!");
                return RedirectToAction(nameof(Index));
            }
            _notyf.Warning("Пожалуйста, заполните все поля.");
            return View(company);
            
        }

        // GET: Companies/Edit/5
        [Authorize(Permissions.Company.Edit)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Company.Edit)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyFullName,CompanyShortName, Inn")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                    _notyf.Success("Успешно!");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _notyf.Error(ex.Message);
                    if (!CompanyExists(company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            _notyf.Warning("Пожалуйста, заполните все поля.");
            return View(company);
        }

        // GET: Companies/Delete/5
        [Authorize(Permissions.Company.Delete)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Company.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Company.FindAsync(id);
            _context.Company.Remove(company);
            await _context.SaveChangesAsync();
            _notyf.Success("Успешно удалено!");
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Company.Any(e => e.Id == id);
        }
    }
}
