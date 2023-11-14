using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDalolatnoma.MVC.Data;
using EDalolatnoma.MVC.Models;
using EDalolatnoma.MVC.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EDalolatnoma.MVC.ViewModels;

namespace EDalolatnoma.MVC.Controllers
{
    [Authorize]
    public class DalolatnomalarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DalolatnomalarController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Dalolatnomalar
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Dalolatnomlar.Include(d => d.BuyerCompany).Include(d => d.SellerCompany);
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpGet]
        public async Task<JsonResult> GetDalolatnomalar()
        {
           
           
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
            var dalolatnoma =await _context.Dalolatnomlar
                                                .Include(i => i.SellerCompany)
                                                .Include(b=>b.BuyerCompany)
                                                .AsNoTracking().ToListAsync();
            var viewDalolatnoma = dalolatnoma.Select(i => new DalolatnomaViewModel 
            {
                Id= i.Id,
                BuyerCompany=i.BuyerCompany.CompanyFullName,
                SellerCompany=i.SellerCompany.CompanyFullName,
                ActNumberDate = "№"+i.ActNumber+", "+i.ActDate.ToShortDateString(),
                BeginDateEndDate=i.BeginDate.ToShortDateString()+" "+i.BeginDate.ToShortTimeString()+" дан "+i.EndDate.ToShortDateString() + " " + i.EndDate.ToShortTimeString()+" гача",
                ContractNumberDate="№"+i.ContractNumber+", "+i.ContractDate.ToShortDateString(),  
                GasAmount=i.GasAmount,
                GasMeterNetwork=i.GasMeterNetwork,
                SendStatus = i.Status.ToString(),
            
            });
            return Json(new { data = viewDalolatnoma });
        }



        // GET: Dalolatnomalar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dalolatnomlar == null)
            {
                return NotFound();
            }

            var dalolatnoma = await _context.Dalolatnomlar
                .Include(d => d.BuyerCompany)
                .Include(d => d.SellerCompany)
                .FirstOrDefaultAsync(m => m.Id == id);
            
       
            if (dalolatnoma == null)
            {
                return NotFound();
            }

            return View(dalolatnoma);
        }

        // GET: Dalolatnomalar/Create
        public IActionResult Create()
        {
            ViewData["BuyerCompanyId"] = new SelectList(_context.Company, "Id", "CompanyFullName");
            ViewData["SellerCompanyId"] = new SelectList(_context.Company, "Id", "CompanyFullName");
            return View();
        }

        // POST: Dalolatnomalar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SellerCompanyId,BuyerCompanyId,ContractDate,ContractNumber,GasAmount,ActDate,ActNumber,MeterNumber,GasMeterNetwork,BeginDate,EndDate")] Dalolatnoma dalolatnoma)
        {
            if (ModelState.IsValid)
            {
                var user =await _userManager.GetUserAsync(User);
                dalolatnoma.CreateBy=user.Id;   
                _context.Add(dalolatnoma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuyerCompanyId"] = new SelectList(_context.Company, "Id", "CompanyFullName", dalolatnoma.BuyerCompanyId);
            ViewData["SellerCompanyId"] = new SelectList(_context.Company, "Id", "CompanyFullName", dalolatnoma.SellerCompanyId);
            return View(dalolatnoma);
        }

        // GET: Dalolatnomalar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dalolatnomlar == null)
            {
                return NotFound();
            }

            var dalolatnoma = await _context.Dalolatnomlar.FindAsync(id);
            if (dalolatnoma == null)
            {
                return NotFound();
            }
            ViewData["BuyerCompanyId"] = new SelectList(_context.Company, "Id", "CompanyFullName", dalolatnoma.BuyerCompanyId);
            ViewData["SellerCompanyId"] = new SelectList(_context.Company, "Id", "CompanyFullName", dalolatnoma.SellerCompanyId);
            return View(dalolatnoma);
        }

        // POST: Dalolatnomalar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SellerCompanyId,BuyerCompanyId,ContractDate,ContractNumber,GasAmount,ActDate,ActNumber,MeterNumber,GasMeterNetwork,BeginDate,EndDate")] Dalolatnoma dalolatnoma)
        {
            if (id != dalolatnoma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userManager.GetUserAsync(User).Result;
                    dalolatnoma.UpdateBy = user.Id;
                    dalolatnoma.UpdateAt=DateTime.Now;  
                    _context.Update(dalolatnoma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DalolatnomaExists(dalolatnoma.Id))
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
            ViewData["BuyerCompanyId"] = new SelectList(_context.Company, "Id", "CompanyFullName", dalolatnoma.BuyerCompanyId);
            ViewData["SellerCompanyId"] = new SelectList(_context.Company, "Id", "CompanyFullName", dalolatnoma.SellerCompanyId);
            return View(dalolatnoma);
        }

        // GET: Dalolatnomalar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dalolatnomlar == null)
            {
                return NotFound();
            }

            var dalolatnoma = await _context.Dalolatnomlar
                .Include(d => d.BuyerCompany)
                .Include(d => d.SellerCompany)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dalolatnoma == null)
            {
                return NotFound();
            }

            return View(dalolatnoma);
        }

        // POST: Dalolatnomalar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dalolatnomlar == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Dalolatnomlar'  is null.");
            }
            var dalolatnoma = await _context.Dalolatnomlar.FindAsync(id);
            if (dalolatnoma != null)
            {
                _context.Dalolatnomlar.Remove(dalolatnoma);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DalolatnomaExists(int id)
        {
          return _context.Dalolatnomlar.Any(e => e.Id == id);
        }
    }
}
