//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Citations.Models;

//namespace Citations.Controllers
//{
//    public class ArticleIssuesController : Controller
//    {
//        private readonly CitationContext _context;

//        public ArticleIssuesController(CitationContext context)
//        {
//            _context = context;
//        }

//        // GET: ArticleIssues
//        public async Task<IActionResult> Index()
//        {
//            var citationContext = _context.ArticleIssues.Include(a => a.Article).Include(a => a.MagazineIssue);
//            return View(await citationContext.ToListAsync());
//        }

//        // GET: ArticleIssues/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var articleIssue = await _context.ArticleIssues
//                .Include(a => a.Article)
//                .Include(a => a.MagazineIssue)
//                .FirstOrDefaultAsync(m => m.Articleid == id);
//            if (articleIssue == null)
//            {
//                return NotFound();
//            }

//            return View(articleIssue);
//        }

//        // GET: ArticleIssues/Create
//        public IActionResult Create()
//        {
//            ViewData["Articleid"] = new SelectList(_context.Articles, "Articleid", "Articletittle");
//            ViewData["MagazineIssueId"] = new SelectList(_context.MagazineIssues, "Issueid", "Issueid");
//            return View();
//        }

//        // POST: ArticleIssues/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Articleid,MagazineIssueId")] ArticleIssue articleIssue)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(articleIssue);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["Articleid"] = new SelectList(_context.Articles, "Articleid", "Articletittle", articleIssue.Articleid);
//            ViewData["MagazineIssueId"] = new SelectList(_context.MagazineIssues, "Issueid", "Issueid", articleIssue.MagazineIssueId);
//            return View(articleIssue);
//        }

//        // GET: ArticleIssues/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var articleIssue = await _context.ArticleIssues.FindAsync(id);
//            if (articleIssue == null)
//            {
//                return NotFound();
//            }
//            ViewData["Articleid"] = new SelectList(_context.Articles, "Articleid", "Articletittle", articleIssue.Articleid);
//            ViewData["MagazineIssueId"] = new SelectList(_context.MagazineIssues, "Issueid", "Issueid", articleIssue.MagazineIssueId);
//            return View(articleIssue);
//        }

//        // POST: ArticleIssues/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Articleid,MagazineIssueId")] ArticleIssue articleIssue)
//        {
//            if (id != articleIssue.Articleid)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(articleIssue);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ArticleIssueExists(articleIssue.Articleid))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["Articleid"] = new SelectList(_context.Articles, "Articleid", "Articletittle", articleIssue.Articleid);
//            ViewData["MagazineIssueId"] = new SelectList(_context.MagazineIssues, "Issueid", "Issueid", articleIssue.MagazineIssueId);
//            return View(articleIssue);
//        }

//        // GET: ArticleIssues/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var articleIssue = await _context.ArticleIssues
//                .Include(a => a.Article)
//                .Include(a => a.MagazineIssue)
//                .FirstOrDefaultAsync(m => m.Articleid == id);
//            if (articleIssue == null)
//            {
//                return NotFound();
//            }

//            return View(articleIssue);
//        }

//        // POST: ArticleIssues/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var articleIssue = await _context.ArticleIssue.FindAsync(id);
//            _context.ArticleIssues.Remove(articleIssue);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ArticleIssueExists(int id)
//        {
//            return _context.ArticleIssues.Any(e => e.Articleid == id);
//        }
//    }
//}
