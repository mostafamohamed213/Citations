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
    public class FacultyInstitutionDepartmentsController : Controller
    {
        private readonly CitationContext _context;

        public FacultyInstitutionDepartmentsController(CitationContext context)
        {
            _context = context;
        }

        // GET: FacultyInstitutionDepartments
        public async Task<IActionResult> Index(int? id)
        {
            var citationContext = _context.FacultyInstitutionDepartments.Where(fid=>fid.FacultyInstitutionid==id).Include(f => f.Department).Include(f => f.FacultyInstitution).ThenInclude(fi=>fi.Faculty).Include(f=>f.FacultyInstitution).ThenInclude(f=>f.Institution);
           if(citationContext.Count()==0)
            citationContext = _context.FacultyInstitutionDepartments.Include(f => f.Department).Include(f => f.FacultyInstitution).ThenInclude(fi => fi.Faculty).Include(f => f.FacultyInstitution).ThenInclude(f => f.Institution);
            return View(await citationContext.ToListAsync());
        }

        // GET: FacultyInstitutionDepartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyInstitutionDepartment = await _context.FacultyInstitutionDepartments
                .Include(f => f.Department)
                .Include(f => f.FacultyInstitution)
                .FirstOrDefaultAsync(m => m.FacultyInstitutionDepartmentid == id);
            if (facultyInstitutionDepartment == null)
            {
                return NotFound();
            }

            return View(facultyInstitutionDepartment);
        }

        // GET: FacultyInstitutionDepartments/Create
        public IActionResult Create()
        {
            ViewData["Departmentid"] = new SelectList(_context.Departments, "Departmentid", "Name");
            ViewData["FacultyInstitutionid"] = new SelectList(_context.FacultyInstitutions, "FacultyInstitutionid", "FacultyInstitutionid");
            return View();
        }

        // POST: FacultyInstitutionDepartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacultyInstitutionDepartmentid,FacultyInstitutionid,Departmentid")] FacultyInstitutionDepartment facultyInstitutionDepartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facultyInstitutionDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departmentid"] = new SelectList(_context.Departments, "Departmentid", "Name", facultyInstitutionDepartment.Departmentid);
            ViewData["FacultyInstitutionid"] = new SelectList(_context.FacultyInstitutions, "FacultyInstitutionid", "FacultyInstitutionid", facultyInstitutionDepartment.FacultyInstitutionid);
            return View(facultyInstitutionDepartment);
        }

        // GET: FacultyInstitutionDepartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyInstitutionDepartment = await _context.FacultyInstitutionDepartments.FindAsync(id);
            if (facultyInstitutionDepartment == null)
            {
                return NotFound();
            }
            ViewData["Departmentid"] = new SelectList(_context.Departments, "Departmentid", "Name", facultyInstitutionDepartment.Departmentid);
            ViewData["FacultyInstitutionid"] = new SelectList(_context.FacultyInstitutions, "FacultyInstitutionid", "FacultyInstitutionid", facultyInstitutionDepartment.FacultyInstitutionid);
            return View(facultyInstitutionDepartment);
        }

        // POST: FacultyInstitutionDepartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacultyInstitutionDepartmentid,FacultyInstitutionid,Departmentid")] FacultyInstitutionDepartment facultyInstitutionDepartment)
        {
            if (id != facultyInstitutionDepartment.FacultyInstitutionDepartmentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultyInstitutionDepartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyInstitutionDepartmentExists(facultyInstitutionDepartment.FacultyInstitutionDepartmentid))
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
            ViewData["Departmentid"] = new SelectList(_context.Departments, "Departmentid", "Name", facultyInstitutionDepartment.Departmentid);
            ViewData["FacultyInstitutionid"] = new SelectList(_context.FacultyInstitutions, "FacultyInstitutionid", "FacultyInstitutionid", facultyInstitutionDepartment.FacultyInstitutionid);
            return View(facultyInstitutionDepartment);
        }

        // GET: FacultyInstitutionDepartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyInstitutionDepartment = await _context.FacultyInstitutionDepartments
                .Include(f => f.Department)
                .Include(f => f.FacultyInstitution)
                .FirstOrDefaultAsync(m => m.FacultyInstitutionDepartmentid == id);
            if (facultyInstitutionDepartment == null)
            {
                return NotFound();
            }

            return View(facultyInstitutionDepartment);
        }

        // POST: FacultyInstitutionDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facultyInstitutionDepartment = await _context.FacultyInstitutionDepartments.FindAsync(id);
            _context.FacultyInstitutionDepartments.Remove(facultyInstitutionDepartment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyInstitutionDepartmentExists(int id)
        {
            return _context.FacultyInstitutionDepartments.Any(e => e.FacultyInstitutionDepartmentid == id);
        }
    }
}
