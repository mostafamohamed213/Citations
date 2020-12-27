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
    public class IssueOfIssuesController : Controller
    {
        private readonly CitationContext _context;

        public IssueOfIssuesController(CitationContext context)
        {
            _context = context;
        }

        // GET: IssueOfIssues
        public async Task<IActionResult> Index(int id)
        {
            var citationContext = _context.IssueOfIssues.Where(a=>a.MagazineIssueId==id).Include(i => i.MagazineIssue);
            MagazineIssue magazineIssue = await _context.MagazineIssues.FirstOrDefaultAsync(a => a.Issueid == id);
            ViewBag.Issuenumber = magazineIssue.Issuenumber;
            ViewBag.magazineid = magazineIssue.Magazineid;
            ViewBag.Issueid = id;
            return View(await citationContext.ToListAsync());
        }

        // GET: IssueOfIssues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueOfIssue = await _context.IssueOfIssues
                .Include(i => i.MagazineIssue)
                .FirstOrDefaultAsync(m => m.IssueOfIssueid == id);
            if (issueOfIssue == null)
            {
                return NotFound();
            }
            MagazineIssue magazineIssue = await _context.MagazineIssues.FirstOrDefaultAsync(a => a.Issueid == issueOfIssue.MagazineIssueId);
            ViewBag.Issuenumber = magazineIssue.Issuenumber;
            ViewBag.Issueid = issueOfIssue.MagazineIssueId;

            return View(issueOfIssue);
        }

        // GET: IssueOfIssues/Create
        public async Task<IActionResult> Create(int id)
        {
            MagazineIssue magazineIssue = await _context.MagazineIssues.FirstOrDefaultAsync(a => a.Issueid == id);
            ViewBag.Issuenumber = magazineIssue.Issuenumber;
            ViewBag.Issueid = id;
            return View();
        }

        // POST: IssueOfIssues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,/*[Bind("IssueOfIssueid,IssuenumberOfIssue,MagazineIssueId,DateOfPublication")]*/ IssueOfIssue issueOfIssue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueOfIssue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "IssueOfIssues", new
                {
                    id = issueOfIssue.MagazineIssueId

                });
            }
            MagazineIssue magazineIssue = await _context.MagazineIssues.FirstOrDefaultAsync(a => a.Issueid == id);
            ViewBag.Issuenumber = magazineIssue.Issuenumber;
            ViewBag.Issueid = id;
            return View(issueOfIssue);
        }

        // GET: IssueOfIssues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueOfIssue = await _context.IssueOfIssues.FindAsync(id);
            if (issueOfIssue == null)
            {
                return NotFound();
            }
            MagazineIssue magazineIssue = await _context.MagazineIssues.FirstOrDefaultAsync(a => a.Issueid == issueOfIssue.MagazineIssueId);
            ViewBag.Issuenumber = magazineIssue.Issuenumber;
            ViewBag.Issueid = issueOfIssue.MagazineIssueId; 
            return View(issueOfIssue);
        }

        // POST: IssueOfIssues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssueOfIssueid,IssuenumberOfIssue,MagazineIssueId,DateOfPublication")] IssueOfIssue issueOfIssue)
        {
            if (id != issueOfIssue.IssueOfIssueid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueOfIssue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueOfIssueExists(issueOfIssue.IssueOfIssueid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "IssueOfIssues", new
                {
                    id = issueOfIssue.MagazineIssueId

                });
            }
            MagazineIssue magazineIssue = await _context.MagazineIssues.FirstOrDefaultAsync(a => a.Issueid == issueOfIssue.MagazineIssueId);
            ViewBag.Issuenumber = magazineIssue.Issuenumber;
            ViewBag.Issueid = issueOfIssue.MagazineIssueId; 
            return View(issueOfIssue);
        }

        // GET: IssueOfIssues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueOfIssue = await _context.IssueOfIssues
                .Include(i => i.MagazineIssue)
                .FirstOrDefaultAsync(m => m.IssueOfIssueid == id);
            if (issueOfIssue == null)
            {
                return NotFound();
            }

            return View(issueOfIssue);
        }

        // POST: IssueOfIssues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueOfIssue = await _context.IssueOfIssues.FindAsync(id);
            _context.IssueOfIssues.Remove(issueOfIssue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public JsonResult CheckIssueNum(string IssuenumberOfIssue, int MagazineIssueId, int? IssueOfIssueid)
        {
            if (IssueOfIssueid == null)
            {
                string n = IssuenumberOfIssue.Trim();
                return Json(!_context.IssueOfIssues.Any(e => e.IssuenumberOfIssue == n && e.MagazineIssueId == MagazineIssueId));

            }
            else
            {
                string n = IssuenumberOfIssue.Trim();
                if(_context.IssueOfIssues.Any(e => e.IssuenumberOfIssue == n && e.MagazineIssueId == MagazineIssueId && e.IssueOfIssueid == IssueOfIssueid))
                {
                    return Json(true);
                }
                else if(_context.IssueOfIssues.Any(e => e.IssuenumberOfIssue == n && e.MagazineIssueId == MagazineIssueId))
                {
                    return Json(false);
                }
                return Json(true);
               

            }

        }
        private bool IssueOfIssueExists(int id)
        {
            return _context.IssueOfIssues.Any(e => e.IssueOfIssueid == id);
        }
    }
}
