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
    public class WellStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WellStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WellStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.WellStatus.ToListAsync());
        }

        // GET: WellStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wellStatus = await _context.WellStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wellStatus == null)
            {
                return NotFound();
            }

            return View(wellStatus);
        }

        // GET: WellStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WellStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusName")] WellStatus wellStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wellStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wellStatus);
        }

        // GET: WellStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wellStatus = await _context.WellStatus.FindAsync(id);
            if (wellStatus == null)
            {
                return NotFound();
            }
            return View(wellStatus);
        }

        // POST: WellStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusName")] WellStatus wellStatus)
        {
            if (id != wellStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wellStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WellStatusExists(wellStatus.Id))
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
            return View(wellStatus);
        }

        // GET: WellStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wellStatus = await _context.WellStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wellStatus == null)
            {
                return NotFound();
            }

            return View(wellStatus);
        }

        // POST: WellStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wellStatus = await _context.WellStatus.FindAsync(id);
            _context.WellStatus.Remove(wellStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WellStatusExists(int id)
        {
            return _context.WellStatus.Any(e => e.Id == id);
        }
    }
}
