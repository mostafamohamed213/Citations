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
    public class DepartmentsController : Controller
    {
        private readonly CitationContext _context;

        public DepartmentsController(CitationContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.Departmentid == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
 
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Departmentid,Name,Active")] Department department,string[] institutions, string[] Faculty)
        {
            if (ModelState.IsValid)
            {

              var existeddep=  _context.Departments.FirstOrDefault(d=>d.Name.ToLower()==department.Name.ToLower());
                if (existeddep != null)
                {
                    ViewBag.exist = "Department Already Exists";
                    return View(department);

                }
                _context.Add(department);

                 _context.SaveChanges();
                int depid=department.Departmentid;
                for (int i = 0; i < institutions.Length; i++)
                {

                    var instid = int.Parse(institutions[i]);
                    var facid = int.Parse(Faculty[i]);
                
                    
                    if (instid == 0)
                    {
                        continue;
                    }
                    int facinstid = _context.FacultyInstitutions.FirstOrDefault(fi => fi.Institutionid == instid && fi.Facultyid == facid).FacultyInstitutionid;
                    
                    _context.FacultyInstitutionDepartments.Add(new FacultyInstitutionDepartment() { FacultyInstitutionid = facinstid ,Departmentid=department.Departmentid}) ;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Departmentid,Name,Active")] Department department)
        {
            if (id != department.Departmentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Departmentid))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.Departmentid == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Departmentid == id);
        }
    }
}
