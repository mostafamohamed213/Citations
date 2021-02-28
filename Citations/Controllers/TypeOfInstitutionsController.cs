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
    public class TypeOfInstitutionsController : Controller
    {
        private readonly CitationContext _context;

        public TypeOfInstitutionsController(CitationContext context)
        {
            _context = context;
        }

        // GET: TypeOfInstitutions
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeOfInstitutions.ToListAsync());
        }
        public IActionResult checkname(String TypeName, int? TypeInstitutionid)
        {
            if (TypeName == null)
            {
                return Json(data: "الرجاء ادخال اسم صحيح");
            }

            if (TypeInstitutionid == null)
            {
                var TypeName1 = TypeName.Trim();
                if (_context.TypeOfInstitutions.FirstOrDefault(i => i.TypeName == TypeName1) == null)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                var TypeName1 = TypeName.Trim();
                if (_context.TypeOfInstitutions.FirstOrDefault(i => i.TypeInstitutionid != TypeInstitutionid && i.TypeName == TypeName1) == null)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
        }
        // GET: TypeOfInstitutions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfInstitution = await _context.TypeOfInstitutions
                .FirstOrDefaultAsync(m => m.TypeInstitutionid == id);
            if (typeOfInstitution == null)
            {
                return NotFound();
            }

            return View(typeOfInstitution);
        }

        // GET: TypeOfInstitutions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfInstitutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeInstitutionid,TypeName,Active")] TypeOfInstitution typeOfInstitution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfInstitution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfInstitution);
        }

        // GET: TypeOfInstitutions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfInstitution = await _context.TypeOfInstitutions.FindAsync(id);
            if (typeOfInstitution == null)
            {
                return NotFound();
            }
            return View(typeOfInstitution);
        }

        // POST: TypeOfInstitutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeInstitutionid,TypeName,Active")] TypeOfInstitution typeOfInstitution)
        {
            if (id != typeOfInstitution.TypeInstitutionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfInstitution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfInstitutionExists(typeOfInstitution.TypeInstitutionid))
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
            return View(typeOfInstitution);
        }

        // GET: TypeOfInstitutions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfInstitution = await _context.TypeOfInstitutions
                .FirstOrDefaultAsync(m => m.TypeInstitutionid == id);
            if (typeOfInstitution == null)
            {
                return NotFound();
            }

            return View(typeOfInstitution);
        }

        // POST: TypeOfInstitutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfInstitution = await _context.TypeOfInstitutions.FindAsync(id);
            _context.TypeOfInstitutions.Remove(typeOfInstitution);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfInstitutionExists(int id)
        {
            return _context.TypeOfInstitutions.Any(e => e.TypeInstitutionid == id);
        }
    }
}
