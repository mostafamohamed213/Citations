using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Citations.Models;

namespace Citations.Controllers
{
    public class MagazinesController : Controller
    {
        private readonly CitationContext _context;

        public MagazinesController(CitationContext context)
        {
            _context = context;
        }

        // GET: Magazines
        public async Task<IActionResult> Index()
        {
            var citationContext = _context.Magazines.Include(m => m.Institution);
            return View(await citationContext.Where(a=>a.Active==true).ToListAsync());
        }

        // GET: Magazines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _context.Magazines
                .Include(m => m.Institution)
                .FirstOrDefaultAsync(m => m.Magazineid == id);
            if (magazine == null)
            {
                return NotFound();
            }

            return View(magazine);
        }

        // GET: Magazines/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<ResearchField> Fields = await _context.ResearchFields.Where(a=>a.Active==true).ToListAsync();
            ViewBag.ResearchFields = Fields;
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name");
            return View();
        }

        // POST: Magazines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Isbn,ImpactFactor,ImmediateCoefficient,AppropriateValue,NumberOfCitations,Institutionid")] Magazine magazine, IEnumerable<int> Field)
        {
            if (ModelState.IsValid)
            {
                magazine.Active = true;
                _context.Add(magazine);
                await _context.SaveChangesAsync();
                foreach (var item in Field)
                {
                    var magazineResearchField = new MagazineResearchField() { Fieldid = item, Magazineid = magazine.Magazineid };
                    _context.Add(magazineResearchField);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", magazine.Institutionid);
            return View(magazine);
        }

        // GET: Magazines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _context.Magazines.FindAsync(id);
            if (magazine == null)
            {
                return NotFound();
            }
            IEnumerable<ResearchField> Fields = await _context.MagazineResearchFields.Where(a => a.Magazineid == id).Select(a => a.Field).ToListAsync();
            ViewBag.ResearchFields = Fields;
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", magazine.Institutionid);
            return View(magazine);
        }

        // POST: Magazines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Magazineid,Name,Isbn,ImpactFactor,ImmediateCoefficient,AppropriateValue,NumberOfCitations,Institutionid,Active")] Magazine magazine, IEnumerable<int> Field)
        {

            if (id != magazine.Magazineid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                IEnumerable<int> Fields = await _context.MagazineResearchFields.Where(a => a.Magazineid == id).Select(a => a.Fieldid).ToListAsync();
                IEnumerable<int> UnCheckedField = Fields.Except(Field);
                try
                {
                    _context.Update(magazine);
                    foreach (var item in UnCheckedField)
                    {
                        var magazineResearchField = new MagazineResearchField() { Fieldid = item, Magazineid = id };
                        _context.Remove(magazineResearchField);

                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagazineExists(magazine.Magazineid))
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
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", magazine.Institutionid);
            return View(magazine);
        }

        // GET: Magazines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _context.Magazines
                .Include(m => m.Institution)
                .FirstOrDefaultAsync(m => m.Magazineid == id);
            if (magazine == null)
            {
                return NotFound();
            }

            return View(magazine);
        }

        // POST: Magazines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var magazine = await _context.Magazines.FindAsync(id);
            magazine.Active = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Magazines/AddResearchField/5
        public async Task<IActionResult> AddResearchField(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _context.Magazines.FirstOrDefaultAsync(m => m.Magazineid == id);
            if (magazine == null)
            {
                return NotFound();
            }
            IEnumerable<ResearchField> MagazineFields = await _context.MagazineResearchFields.Where(a => a.Magazineid == id).Select(a => a.Field).ToListAsync();
            IEnumerable<ResearchField> AllFields = await _context.ResearchFields.Where(a => a.Active == true).ToListAsync();
            IEnumerable<ResearchField> AvailableFields = AllFields.Except(MagazineFields);
            ViewBag.magazineName = magazine.Name;
            return View(AvailableFields);
        }
        // POST: Magazines/AddResearchField/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddResearchField(int id,int[] Field)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _context.Magazines.FirstOrDefaultAsync(m => m.Magazineid == id);
            if (magazine == null)
            {
                return NotFound();
            }
            foreach(var item in Field)
            {
                var magazineResearchField = new MagazineResearchField() { Fieldid = item, Magazineid = id };
                _context.Add(magazineResearchField);
                
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /*// GET: Magazines/DeleteResearchField/5
        public async Task<IActionResult> DeleteResearchField(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _context.Magazines.FirstOrDefaultAsync(m => m.Magazineid == id);
            if (magazine == null)
            {
                return NotFound();
            }
            IEnumerable<ResearchField> Fields = await _context.MagazineResearchFields.Where(a => a.Magazineid == id).Select(a => a.Field).ToListAsync();
            ViewBag.magazineName = magazine.Name;
            return View(Fields);
        }
        // POST: Magazines/DeleteResearchField/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteResearchField(int id, int[] Field)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _context.Magazines.FirstOrDefaultAsync(m => m.Magazineid == id);
            if (magazine == null)
            {
                return NotFound();
            }
            foreach (var item in Field)
            {
                var magazineResearchField = new MagazineResearchField() { Fieldid = item, Magazineid = id };
                _context.Remove(magazineResearchField);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
        private bool MagazineExists(int id)
        {
            return _context.Magazines.Any(e => e.Magazineid == id);
        }
    }
}
