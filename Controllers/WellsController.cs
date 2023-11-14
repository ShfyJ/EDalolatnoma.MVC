using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDalolatnoma.MVC.Data;
using EDalolatnoma.MVC.Models;

namespace EDalolatnoma.MVC.Controllers
{
    public class WellsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WellsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wells
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Well.Include(w => w.Field).Include(w => w.WellStatus);
            return View(await applicationDbContext.ToListAsync());
        }



        [HttpGet]
        public JsonResult GetWells()
        {
            var fields = _context.Well
                         .Include(i => i.Field).ThenInclude(i=>i.Company)
                         .Include(i=>i.WellStatus)
                         .AsNoTracking().ToList();
            return Json(new { data = fields });
        }

        [HttpGet]
        public JsonResult GetFields(int id)
        {
            var fields = _context.Field.Where(c=>c.Company_id==id)
                         .AsNoTracking().ToList();
            return Json(new SelectList(fields, "Id", "FieldName"));
        }
        
        [DbFunction(Name = "SOUNDEX", Schema = "")]
        public static string SoundsLike(string keyword)
        {
            throw new NotImplementedException();
        }
        // GET: Wells/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var well = await _context.Well
                .Include(w => w.Field)
                .Include(w => w.WellStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (well == null)
            {
                return NotFound();
            }

            return View(well);
        }

        // GET: Wells/Create
        public IActionResult Create()
        {
           
            ViewData["Company_id"] = new SelectList(_context.Company, "Id", "CompanyFullName");
            ViewData["Field_id"] = new SelectList(_context.Field,"Id", "FieldName");
            ViewData["WellStatus_id"] = new SelectList(_context.Set<WellStatus>(), "Id", "StatusName");
            return View();
        }

       

        // POST: Wells/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WellName,Field_id,WellStatus_id")] Well well)
        {
            if (ModelState.IsValid)
            {
                _context.Add(well);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Field_id"] = new SelectList(_context.Field, "Id", "FieldName", well.Field_id);
            ViewData["WellStatus_id"] = new SelectList(_context.Set<WellStatus>(), "Id", "StatusName", well.WellStatus_id);
            return View(well);
        }

        // GET: Wells/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var well = await _context.Well.FindAsync(id);
            if (well == null)
            {
                return NotFound();
            }
            ViewData["Field_id"] = new SelectList(_context.Field, "Id", "FieldName", well.Field_id);
            ViewData["WellStatus_id"] = new SelectList(_context.Set<WellStatus>(), "Id", "StatusName", well.WellStatus_id);
            return View(well);
        }

        // POST: Wells/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WellName,Field_id,WellStatus_id")] Well well)
        {
            if (id != well.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(well);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WellExists(well.Id))
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
            ViewData["Field_id"] = new SelectList(_context.Field, "Id", "FieldName", well.Field_id);
            ViewData["WellStatus_id"] = new SelectList(_context.Set<WellStatus>(), "Id", "StatusName", well.WellStatus_id);
            return View(well);
        }

        // GET: Wells/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var well = await _context.Well
                .Include(w => w.Field).ThenInclude(c=>c.Company)
                .Include(w => w.WellStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (well == null)
            {
                return NotFound();
            }

            return View(well);
        }

        // POST: Wells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var well = await _context.Well.FindAsync(id);
            _context.Well.Remove(well);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WellExists(int id)
        {
            return _context.Well.Any(e => e.Id == id);
        }
    }
}
