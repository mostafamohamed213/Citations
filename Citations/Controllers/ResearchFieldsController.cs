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
    public class ResearchFieldsController : Controller
    {
        private readonly CitationContext _context;

        public ResearchFieldsController(CitationContext context)
        {
            _context = context;
        }

        // GET: ResearchFields
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResearchFields.Where(a=>a.Active==true).ToListAsync());
        }

        // GET: ResearchFields/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchField = await _context.ResearchFields
                .FirstOrDefaultAsync(m => m.Fieldid == id);
            if (researchField == null)
            {
                return NotFound();
            }

            return View(researchField);
        }

        // GET: ResearchFields/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResearchFields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Fieldid,Name")] ResearchField researchField)
        {
            if (ModelState.IsValid)
            {
                researchField.Active = true;
                _context.Add(researchField);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(researchField);
        }

        // GET: ResearchFields/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchField = await _context.ResearchFields.FindAsync(id);
            if (researchField == null)
            {
                return NotFound();
            }
            return View(researchField);
        }

        // POST: ResearchFields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Fieldid,Name,Active")] ResearchField researchField)
        {
            if (id != researchField.Fieldid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(researchField);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchFieldExists(researchField.Fieldid))
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
            return View(researchField);
        }

        // GET: ResearchFields/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchField = await _context.ResearchFields
                .FirstOrDefaultAsync(m => m.Fieldid == id);
            if (researchField == null)
            {
                return NotFound();
            }

            return View(researchField);
        }

        // POST: ResearchFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var researchField = await _context.ResearchFields.FindAsync(id);
            researchField.Active = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public JsonResult CheckField(string Name, int? Fieldid)
        {
            var name = Name.Trim();
            if (Fieldid == null)
            {
                return Json(!_context.ResearchFields.Any(e => e.Name == name));

            }
            else
            {

                if (_context.ResearchFields.Any(e => e.Name == name && e.Fieldid == Fieldid))
                {
                    return Json(true);
                }
                else if (_context.ResearchFields.Any(e => e.Name == name))
                {
                    return Json(false);
                }
                return Json(true);


            }



        }

        private bool ResearchFieldExists(int id)
        {
            return _context.ResearchFields.Any(e => e.Fieldid == id);
        }
    }
}
