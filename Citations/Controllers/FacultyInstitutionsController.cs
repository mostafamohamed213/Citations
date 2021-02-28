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
    public class FacultyInstitutionsController : Controller
    {
        private readonly CitationContext _context;

        public FacultyInstitutionsController(CitationContext context)
        {
            _context = context;
        }

        // GET: FacultyInstitutions
        public async Task<IActionResult> Index()
        {
            var citationContext = _context.FacultyInstitutions.Include(f => f.Faculty).Include(f => f.Institution);
            return View(await citationContext.ToListAsync());
        }

        // GET: FacultyInstitutions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyInstitution = await _context.FacultyInstitutions
                .Include(f => f.Faculty)
                .Include(f => f.Institution)
                .FirstOrDefaultAsync(m => m.FacultyInstitutionid == id);
            if (facultyInstitution == null)
            {
                return NotFound();
            }

            return View(facultyInstitution);
        }

        // GET: FacultyInstitutions/Create
        public IActionResult Create()
        {
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Name");
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name");
            return View();
        }

        // POST: FacultyInstitutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacultyInstitutionid,Institutionid,Facultyid")] FacultyInstitution facultyInstitution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facultyInstitution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Name", facultyInstitution.Facultyid);
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", facultyInstitution.Institutionid);
            return View(facultyInstitution);
        }

        // GET: FacultyInstitutions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyInstitution = await _context.FacultyInstitutions.FindAsync(id);
            if (facultyInstitution == null)
            {
                return NotFound();
            }
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Name", facultyInstitution.Facultyid);
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", facultyInstitution.Institutionid);
            return View(facultyInstitution);
        }

        // POST: FacultyInstitutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacultyInstitutionid,Institutionid,Facultyid")] FacultyInstitution facultyInstitution)
        {
            if (id != facultyInstitution.FacultyInstitutionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultyInstitution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyInstitutionExists(facultyInstitution.FacultyInstitutionid))
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
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Name", facultyInstitution.Facultyid);
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", facultyInstitution.Institutionid);
            return View(facultyInstitution);
        }

        // GET: FacultyInstitutions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyInstitution = await _context.FacultyInstitutions
                .Include(f => f.Faculty)
                .Include(f => f.Institution)
                .FirstOrDefaultAsync(m => m.FacultyInstitutionid == id);
            if (facultyInstitution == null)
            {
                return NotFound();
            }

            return View(facultyInstitution);
        }

        // POST: FacultyInstitutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facultyInstitution = await _context.FacultyInstitutions.FindAsync(id);
            _context.FacultyInstitutions.Remove(facultyInstitution);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyInstitutionExists(int id)
        {
            return _context.FacultyInstitutions.Any(e => e.FacultyInstitutionid == id);
        }
    }
}
