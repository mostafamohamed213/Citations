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
    public class MagazineIssuesController : Controller
    {
        private readonly CitationContext _context;

        public MagazineIssuesController(CitationContext context)
        {
            _context = context;
        }

        // GET: MagazineIssues
        public async Task<IActionResult> Index(int id)
        {
            Magazine magazine = await _context.Magazines.FirstOrDefaultAsync(a => a.Magazineid == id);
            ViewBag.magazineName = magazine.Name;
            ViewBag.magazineId = id;

            var citationContext = _context.MagazineIssues.Where(a=>a.Magazineid==id).Include(m => m.Magazine);
            return View(await citationContext.ToListAsync());
        }
        /*CheckIssuenumber*/
        // GET: MagazineIssues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazineIssue = await _context.MagazineIssues
                .Include(m => m.Magazine)
                .FirstOrDefaultAsync(m => m.Issueid == id);
            if (magazineIssue == null)
            {
                return NotFound();
            }
            return View(magazineIssue);
        }

        // GET: MagazineIssues/Create
        public IActionResult Create(int id)
        {
            Magazine magazine = _context.Magazines.FirstOrDefault(a => a.Magazineid == id);
            ViewBag.magazineName = magazine.Name;
            ViewBag.magazineId = id;
            return View();
        }

        // POST: MagazineIssues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,/*[Bind("Issueid,Issuenumber,Magazineid,Publisherid,DateOfPublication")] */ MagazineIssue magazineIssue)
        {
           
            if (ModelState.IsValid)
            {
                _context.Add(magazineIssue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),"MagazineIssues", new
                {
                    id = magazineIssue.Magazineid
                   
                });
            }
            Magazine magazine = _context.Magazines.FirstOrDefault(a => a.Magazineid == id);
            ViewBag.magazineName = magazine.Name;
            ViewBag.magazineId = id;
            //ViewData["Magazineid"] = new SelectList(_context.Magazines, "Magazineid", "Name", magazineIssue.Magazineid);
           
            return View(magazineIssue);
        }

        // GET: MagazineIssues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazineIssue = await _context.MagazineIssues.FindAsync(id);
            if (magazineIssue == null)
            {
                return NotFound();
            }
            //MagazineIssue magazineIssue1 = _context.MagazineIssues.FirstOrDefault(a => a.Issueid == id);
            ////ViewBag.magazineName = magazineIssue1.Magazineid;
            //ViewBag.magazineId = magazineIssue1.Magazineid;
            ////ViewData["Magazineid"] = new SelectList(_context.Magazines, "Magazineid", "Name", magazineIssue.Magazineid);
            
            Magazine magazine = _context.Magazines.FirstOrDefault(a => a.Magazineid == magazineIssue.Magazineid);
            ViewBag.magazineName = magazine.Name;
            ViewBag.magazineId = magazine.Magazineid;

            return View(magazineIssue);
        }

        // POST: MagazineIssues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,/* [Bind("Issueid,Issuenumber,Magazineid,Publisherid,DateOfPublication")]*/ MagazineIssue magazineIssue)
        {
            if (id != magazineIssue.Issueid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(magazineIssue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagazineIssueExists(magazineIssue.Issueid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "MagazineIssues", new
                {
                    id = magazineIssue.Magazineid

                });
            }
            //MagazineIssue magazineIssue1 = _context.MagazineIssues.FirstOrDefault(a => a.Issueid == id);
            ////ViewBag.magazineName = magazineIssue1.Magazineid;
            //ViewBag.magazineId = magazineIssue1.Magazineid;
            ////ViewData["Magazineid"] = new SelectList(_context.Magazines, "Magazineid", "Isbn", magazineIssue.Magazineid);
            Magazine magazine = _context.Magazines.FirstOrDefault(a => a.Magazineid == magazineIssue.Magazineid);
            ViewBag.magazineName = magazine.Name;
            ViewBag.magazineId = magazine.Magazineid;

            return View(magazineIssue);
        }

        // GET: MagazineIssues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazineIssue = await _context.MagazineIssues
                .Include(m => m.Magazine)
                .FirstOrDefaultAsync(m => m.Issueid == id);
            if (magazineIssue == null)
            {
                return NotFound();
            }

            return View(magazineIssue);
        }

        // POST: MagazineIssues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var magazineIssue = await _context.MagazineIssues.FindAsync(id);
            _context.MagazineIssues.Remove(magazineIssue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public JsonResult CheckIssuenumber(int Issuenumber, int Magazineid,int? Issueid)
        {
            if(Issueid==null)
            {
                return Json(!_context.MagazineIssues.Any(e => e.Issuenumber == Issuenumber && e.Magazineid == Magazineid));
                
            }
            else
            {
                
                if (_context.MagazineIssues.Any(e => e.Issuenumber == Issuenumber && e.Magazineid == Magazineid && e.Issueid == Issueid))
                {
                    return Json(true);
                }
                else if (_context.MagazineIssues.Any(e => e.Issuenumber == Issuenumber && e.Magazineid == Magazineid))
                {
                    return Json(false);
                }
                return Json(true);
                
                               
            }
               
        }
        private bool MagazineIssueExists(int id)
        {
            return _context.MagazineIssues.Any(e => e.Issueid == id);
        }
    }
}
