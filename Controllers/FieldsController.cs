using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDalolatnoma.MVC.Data;
using EDalolatnoma.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using EDalolatnoma.MVC.Constants;
using AspNetCoreHero.ToastNotification.Abstractions;
using EDalolatnoma.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace EDalolatnoma.MVC.Controllers
{
    [Authorize]
    public class FieldsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notyf;
        private readonly IAuthorizationService _authorizationService;
        public FieldsController(ApplicationDbContext context,
                                       INotyfService notyf,
                            IAuthorizationService authorizationService,
                            UserManager<ApplicationUser> userManager) 
        {
            _context = context;
            _notyf = notyf; 
            _authorizationService = authorizationService;  
            _userManager = userManager; 
        }

        // GET: Fields
        [Authorize(Permissions.Fields.View)]
        public async Task<IActionResult> Index()
        {
           var model=await _context.Field.Include(c=>c.Company).ToListAsync();
            return View(model);
        }


        [HttpGet]
        public JsonResult GetFields()
        {
            var user = _userManager.GetUserAsync(User);
            if (user == null) return Json(new { data = "" });

            bool viewAll = (_authorizationService.AuthorizeAsync(User, Permissions.ViewAll.View)).Result.Succeeded;
            int companyId = user.Result.Company_id;
            List<Field> fields = new ();
            /*if (viewAll)
            {
               fields = _context.Field.Include(i => i.Company)
                           .AsNoTracking().ToList();
            }
            else
            {
                 fields = _context.Field.Where(i => i.Company_id == companyId)
                                       .Include(i => i.Company)
                                       .AsNoTracking().ToList();
            }*/
            fields = _context.Field.Include(i => i.Company)
                           .AsNoTracking().ToList();
            return Json(new { data = fields });
        }

        [HttpGet]
        public JsonResult GetPermission() 
        {
         bool create = (_authorizationService.AuthorizeAsync(User, Permissions.Company.Create)).Result.Succeeded,
                 edit = (_authorizationService.AuthorizeAsync(User, Permissions.Company.Edit)).Result.Succeeded,
                 delete = (_authorizationService.AuthorizeAsync(User, Permissions.Company.Delete)).Result.Succeeded,
                 view = (_authorizationService.AuthorizeAsync(User, Permissions.Company.Delete)).Result.Succeeded;
            GetPermissionView permissionViewModel = new() {
                View=view,
                Create = create,
               // Edit = edit,
               // Delete = delete
            
            };
            return Json( permissionViewModel);
        }
        // GET: Fields/Details/5
        [Authorize(Permissions.Fields.View)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = await _context.Field
                .Include(i => i.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@field == null)
            {
                return NotFound();
            }
            CheckCompany(field.Company_id);
            return View(@field);
        }

        // GET: Fields/Create
        [Authorize(Permissions.Fields.Create)]
        public IActionResult Create()
        {
           // var user = _userManager.GetUserAsync(User);
          //  bool viewAll = (_authorizationService.AuthorizeAsync(User, Permissions.ViewAll.View)).Result.Succeeded;
            ///int companyId = user.Result.Company_id;
            ViewData["Company_id"] = new SelectList(_context.Company.ToList(), "Id", "CompanyFullName"); //Where(i => i.Id == companyId)
          /*  if (viewAll)
            {
              ViewData["Company_id"] = new SelectList(_context.Company.Where(i => i.Id == companyId), "Id", "CompanyFullName");
            }*/
            return View();
        }

        // POST: Fields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Fields.Create)]
        public async Task<IActionResult> Create([Bind("Id,FieldName,Company_id")] Field @field)
        {
            if (ModelState.IsValid)
            {
              //  CheckCompany(field.Company_id);
                _context.Add(@field);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Company_id"] = new SelectList(_context.Company, "Id", "CompanyFullName", @field.Company_id);
            return View(@field);
        }

        // GET: Fields/Edit/5
        [Authorize(Permissions.Fields.Edit)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = await _context.Field.FindAsync(id);
            if (@field == null)
            {
                return NotFound();
            }
            CheckCompany(field.Company_id);
            var user = _userManager.GetUserAsync(User);
            bool viewAll = (_authorizationService.AuthorizeAsync(User, Permissions.ViewAll.View)).Result.Succeeded;
            int companyId = user.Result.Company_id;
            if (viewAll)
            {
                ViewData["Company_id"] = new SelectList(_context.Company.Where(i => i.Id == companyId), "Id", "CompanyFullName", @field.Company_id);
            }
            else
            {
               ViewData["Company_id"] = new SelectList(_context.Company.Where(i => i.Id == companyId), "Id", "CompanyFullName", @field.Company_id);
            }


            return View(@field);
        }

        // POST: Fields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Fields.Edit)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FieldName,Company_id")] Field @field)
        {
            if (id != @field.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   CheckCompany(field.Company_id);
                    _context.Update(@field);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldExists(@field.Id))
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
            var user = _userManager.GetUserAsync(User);
            bool viewAll = (_authorizationService.AuthorizeAsync(User, Permissions.ViewAll.View)).Result.Succeeded;
            int companyId = user.Result.Company_id;
            if (viewAll)
            {
                ViewData["Company_id"] = new SelectList(_context.Company.Where(i => i.Id == companyId), "Id", "CompanyFullName", @field.Company_id);
            }
            else
            {
                ViewData["Company_id"] = new SelectList(_context.Company.Where(i => i.Id == companyId), "Id", "CompanyFullName", @field.Company_id);
            }
       
            return View(@field);
        }

        // GET: Fields/Delete/5
        [Authorize(Permissions.Fields.Delete)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var @field = await _context.Field
                .Include(@i => @i.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@field == null)
            {
                return NotFound();
            }
            CheckCompany(field.Company_id);
            



            return View(@field);
        }

        // POST: Fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Fields.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

           
            var @field = await _context.Field.FindAsync(id); 
            CheckCompany(field.Company_id);
            _context.Field.Remove(@field);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private void CheckCompany(int company_id) 
        {
            var user = _userManager.GetUserAsync(User);
            bool viewAll = (_authorizationService.AuthorizeAsync(User, Permissions.ViewAll.View)).Result.Succeeded;
            int companyId = user.Result.Company_id;
            if (!viewAll)
            {
                if (company_id != companyId)
                {
                   _notyf.Error("Запрос вне доступа.");
                   RedirectToAction("Index");
                }
            }

        }
        private bool FieldExists(int id)
        {
            return _context.Field.Any(e => e.Id == id);
        }
    }
}
