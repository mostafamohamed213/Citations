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
    public class KeyWordsController : Controller
    {
        private readonly CitationContext _context;

        public KeyWordsController(CitationContext context)
        {
            _context = context;
        }

        // GET: KeyWords
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

            var students = from s in _context.KeyWords
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.KeyWord1.Contains(searchString));
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
            return View(await PaginatedList<KeyWord>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: KeyWords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyWord = await _context.KeyWords
                .FirstOrDefaultAsync(m => m.KeyWordid == id);
            if (keyWord == null)
            {
                return NotFound();
            }

            return View(keyWord);
        }

        public IActionResult checkname(String KeyWord1, int? KeyWordid)
        {
            if (KeyWord1 == null)
            {
                return Json(data: "الرجاء ادخال اسم صحيح");
            }

            if (KeyWordid == null)
            {
                var name = KeyWord1.Trim();
                if (_context.KeyWords.FirstOrDefault(i => i.KeyWord1 == name) == null)
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
                var name = KeyWord1.Trim();
                if (_context.KeyWords.FirstOrDefault(i => i.KeyWordid != KeyWordid && i.KeyWord1 == name) == null)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
        }

        // GET: KeyWords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KeyWords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeyWordid,KeyWord1,KeyWordEn,Active")] KeyWord keyWord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keyWord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(keyWord);
        }

        // GET: KeyWords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyWord = await _context.KeyWords.FindAsync(id);
            if (keyWord == null)
            {
                return NotFound();
            }
            return View(keyWord);
        }

        // POST: KeyWords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KeyWordid,KeyWord1,KeyWordEn,Active")] KeyWord keyWord)
        {
            if (id != keyWord.KeyWordid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keyWord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeyWordExists(keyWord.KeyWordid))
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
            return View(keyWord);
        }

        // GET: KeyWords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyWord = await _context.KeyWords
                .FirstOrDefaultAsync(m => m.KeyWordid == id);
            if (keyWord == null)
            {
                return NotFound();
            }

            return View(keyWord);
        }

        // POST: KeyWords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keyWord = await _context.KeyWords.FindAsync(id);
            _context.KeyWords.Remove(keyWord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeyWordExists(int id)
        {
            return _context.KeyWords.Any(e => e.KeyWordid == id);
        }
    }
}
