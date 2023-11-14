using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDalolatnoma.MVC.Data;
using EDalolatnoma.MVC.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;
using EDalolatnoma.MVC.ViewModels;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;

namespace EDalolatnoma.MVC.Controllers
{
    public class KernoDatabaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileProvider _fileProvider;
        public KernoDatabaseController(ApplicationDbContext context,
            IFileProvider fileProvider,
             UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _fileProvider = fileProvider;
            _userManager = userManager; 
       
        }

        // GET: KernoDatabase
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.KernoData.Include(k => k.Well);
            return View(await applicationDbContext.ToListAsync());
        }
       [HttpGet]
        public JsonResult GetKernoData() 
        {
            var kerno = _context.KernoData
                         .Include(i=>i.Well).ThenInclude(i=>i.Field).ThenInclude(i=>i.Company)
                         .Include(i => i.Well).ThenInclude(i => i.WellStatus)
                         .AsNoTracking().ToList();
            
            return Json(new { data = kerno });
        }

        // GET: KernoDatabase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var kernoData = await _context.KernoData
                .Include(k => k.Well).ThenInclude(c=>c.Field).ThenInclude(c=>c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kernoData == null)
            {
                return NotFound();
            }
            var model = new FilesViewModel();
            foreach (var item in _fileProvider.GetDirectoryContents("files//documents//" + id.ToString()))
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, Path = item.PhysicalPath });
            }
            ViewData["Documents"]= model;
            return View(kernoData);
        }



        public async Task<IActionResult> Download(string filename, int id)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot//files//documents//" + id.ToString(), filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".mp4", "video/mp4"}
            };
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFiles(int id, List<IFormFile> fileModel)
        {
            if (fileModel != null || fileModel.Count > 0)
            {
                foreach (var file in fileModel)
                {
                    var pathId = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot//files//documents" + "//" + id.ToString(),
                            file.GetFilename());

                    using (var stream = new FileStream(pathId, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return RedirectToAction("Details", new { id = id });
        }


        public IActionResult DeleteFile(string fileName, int id)
        {
            var pathId = Path.Combine(
                               Directory.GetCurrentDirectory(), "wwwroot//files//documents" + "//" + id.ToString());

            if (System.IO.File.Exists(Path.Combine(pathId, fileName)))
            {
                // If file found, delete it    
                System.IO.File.Delete(Path.Combine(pathId, fileName));
                Console.WriteLine("File deleted.");
            }

            return RedirectToAction("Details", new { id = id });
        }






        // GET: KernoDatabase/Create
        public IActionResult Create()
        {
            
            ViewData["Company_id"] = new SelectList(_context.Company, "Id", "CompanyShortName");
            return View();
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Well_id,RegNum,Files,Photo,Interval_1_str,Interval_2_str,Core_raise_str,Date_added,Date_Selection,PersonName")] KernoData kernoData)
        {
          
            if (ModelState.IsValid)
            {
                string extention = Path.GetExtension(kernoData.Photo.FileName);
                string photoname=Guid.NewGuid().ToString()+extention;
                
                kernoData.CreateBy = User.Identity.Name;
                kernoData.Interval_1=Convert.ToDouble(kernoData.Interval_1_str.Replace(",","."));
                kernoData.Interval_2 = Convert.ToDouble(kernoData.Interval_2_str.Replace(",", "."));
                kernoData.Core_raise = Convert.ToDouble(kernoData.Core_raise_str.Replace(",", "."));
                kernoData.Date_Selection =kernoData.Date_Selection.ToUniversalTime();
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//files//photo",photoname);
                kernoData.PhotoName = photoname;
                using (var filestream = new FileStream(pathFile, FileMode.Create))
                {
                 await kernoData.Photo.CopyToAsync(filestream);
                }
                _context.Add(kernoData);
                await _context.SaveChangesAsync();
                var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot//files//documents");
                Directory.CreateDirectory(path + "//" + kernoData.Id.ToString());

                if (kernoData.Files != null)
                {

                    foreach (var file in kernoData.Files)
                    {
/*                      string filename = Path.GetFileNameWithoutExtension(file.FileName);
                        string extention = Path.GetExtension(file.FileName);
                        
                        filename = xodim.Firstname + "_" + xodim.Lastname + "_" + xodim.Middlename + "_" + fileturi + extention;*/

                        var pathId = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot//files//documents" + "//" + kernoData.Id.ToString(),file.GetFilename());

                           
                        using (var stream = new FileStream(pathId, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            } 
            
            ViewData["Company_id"] = new SelectList(_context.Company, "Id", "CompanyShortName", kernoData.Well.Field.Company_id);
            ViewData["Field_id"] = new SelectList(_context.Field, "Id", "FieldName", kernoData.Well.Field_id);
            ViewData["Well_id"] = new SelectList(_context.Well, "Id", "WellName", kernoData.Well_id);
           
            return View(kernoData);
        }

        // GET: KernoDatabase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kernoData = await _context.KernoData.FindAsync(id);
            if (kernoData == null)
            {
                return NotFound();
            }
            ViewData["Well_id"] = new SelectList(_context.Well, "Id", "WellName", kernoData.Well_id);
            return View(kernoData);
        }

        public JsonResult GetFields(int id)
        {
                return Json(new SelectList(_context.Field.Where(i=>i.Company_id==id), "Id", "FieldName"));
        }
        public JsonResult GetWells(int id)
        {
            return Json(new SelectList(_context.Well.Where(i => i.Field_id == id), "Id", "WellName"));
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Well_id,Interval_1,Interval_2,Core_raise,Date_added,Date_Selection,PersonName")] KernoData kernoData)
        {
            if (id != kernoData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kernoData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KernoDataExists(kernoData.Id))
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
            ViewData["Well_id"] = new SelectList(_context.Well, "Id", "WellName", kernoData.Well_id);
            return View(kernoData);
        }

        // GET: KernoDatabase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kernoData = await _context.KernoData
                .Include(k => k.Well)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kernoData == null)
            {
                return NotFound();
            }

            return View(kernoData);
        }

        // POST: KernoDatabase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kernoData = await _context.KernoData.FindAsync(id);
            _context.KernoData.Remove(kernoData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KernoDataExists(int id)
        {
            return _context.KernoData.Any(e => e.Id == id);
        }
    }
}
