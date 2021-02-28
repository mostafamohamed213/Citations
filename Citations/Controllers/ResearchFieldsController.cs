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
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.ResearchFields.Where(a=>a.Active==true).ToListAsync());
        //}

        public async Task<IActionResult> Index(
           string sortOrder,
           string currentFilter,
           string searchString,
           int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            //  ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.ResearchFields
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString));
            }
            //switch (sortOrder)
            //{
            //    //3kstha 
            //    case "name_desc":
            //        students = students.OrderBy(s => s.NumberOfCitations);
            //        break;
            //    //case "Date":
            //    //    students = students.OrderBy(s => s.EnrollmentDate);
            //    //    break;
            //    //case "date_desc":
            //    //    students = students.OrderByDescending(s => s.EnrollmentDate);
            //    //    break;
            //    default:
            //        students = students.OrderByDescending(s => s.NumberOfCitations);
            //        break;
            //}

            int pageSize = 5;
            return View(await PaginatedList<ResearchField>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
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
        public async Task<IActionResult> Create(/*[Bind("Fieldid,Name")]*/ ResearchField researchField)
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
        public async Task<IActionResult> Edit(int id, /*[Bind("Fieldid,Name,Active")]*/ ResearchField researchField)
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
        
         public JsonResult CheckEnField(string NameEn, int? Fieldid)
         {
            var name = NameEn.Trim();
            if (Fieldid == null)
            {
                return Json(!_context.ResearchFields.Any(e => e.NameEn == name));

            }
            else
            {

                if (_context.ResearchFields.Any(e => e.NameEn == name && e.Fieldid == Fieldid))
                {
                    return Json(true);
                }
                else if (_context.ResearchFields.Any(e => e.NameEn == name))
                {
                    return Json(false);
                }
                return Json(true);
            }
        }
        public JsonResult CheckField(string Name, int? Fieldid)
        {
            if (Name == null)
            {
                return Json(data: "الرجاء ادخال اسم صحيح");
            }
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
