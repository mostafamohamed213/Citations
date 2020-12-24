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
    public class ArticleAuthoresController : Controller
    {
        private readonly CitationContext _context;

        public ArticleAuthoresController(CitationContext context)
        {
            _context = context;
        }

        // GET: ArticleAuthores
        public async Task<IActionResult> Index()
        {
            var citationContext = _context.ArticleAuthores.Include(a => a.Article).Include(a => a.Author);
            return View(await citationContext.ToListAsync());
        }

        // GET: ArticleAuthores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleAuthore = await _context.ArticleAuthores
                .Include(a => a.Article)
                .Include(a => a.Author)
                .FirstOrDefaultAsync(m => m.Authorid == id);
            if (articleAuthore == null)
            {
                return NotFound();
            }

            return View(articleAuthore);
        }

        // GET: ArticleAuthores/Create
        public IActionResult Create()
        {
            ViewData["Articleid"] = new SelectList(_context.Articles, "Articleid", "Articletittle");
            ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name");
            return View();
        }

        // POST: ArticleAuthores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Authorid,Articleid,MainAuthor")] ArticleAuthore articleAuthore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleAuthore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Articleid"] = new SelectList(_context.Articles, "Articleid", "Articletittle", articleAuthore.Articleid);
            ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name", articleAuthore.Authorid);
            return View(articleAuthore);
        }

        // GET: ArticleAuthores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleAuthore = await _context.ArticleAuthores.FindAsync(id);
            if (articleAuthore == null)
            {
                return NotFound();
            }
            ViewData["Articleid"] = new SelectList(_context.Articles, "Articleid", "Articletittle", articleAuthore.Articleid);
            ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name", articleAuthore.Authorid);
            return View(articleAuthore);
        }

        // POST: ArticleAuthores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Authorid,Articleid,MainAuthor")] ArticleAuthore articleAuthore)
        {
            if (id != articleAuthore.Authorid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleAuthore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleAuthoreExists(articleAuthore.Authorid))
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
            ViewData["Articleid"] = new SelectList(_context.Articles, "Articleid", "Articletittle", articleAuthore.Articleid);
            ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name", articleAuthore.Authorid);
            return View(articleAuthore);
        }

        // GET: ArticleAuthores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleAuthore = await _context.ArticleAuthores
                .Include(a => a.Article)
                .Include(a => a.Author)
                .FirstOrDefaultAsync(m => m.Authorid == id);
            if (articleAuthore == null)
            {
                return NotFound();
            }

            return View(articleAuthore);
        }

        // POST: ArticleAuthores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articleAuthore = await _context.ArticleAuthores.FindAsync(id);
            _context.ArticleAuthores.Remove(articleAuthore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleAuthoreExists(int id)
        {
            return _context.ArticleAuthores.Any(e => e.Authorid == id);
        }
    }
}
