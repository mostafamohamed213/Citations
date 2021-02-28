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
    public class AuthorsproController : Controller
    {
        private readonly CitationContext _context;

        public AuthorsproController(CitationContext context)
        {
            _context = context;
        }

        // GET: Authorspro
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.ToListAsync());
        }

        // GET: Authorspro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Articleauthor =  _context.ArticleAuthores.Where(m => m.Authorid == id).Select(s => s.Article);

            var artauth = (from a in _context.ArticleAuthores
                            join b in Articleauthor
                            on a.Articleid equals b.Articleid
                            select a);
            var coauth = (from a in _context.Authors
                           join b in artauth
                           on a.Authorid equals b.Authorid
                           where a.Authorid != id 
                          select a
                           ).Distinct();
           

            ViewBag.co = coauth;
            var author = await _context.Authors.Include(p=>p.ArticleAuthores).ThenInclude(p=>p.Author).Include(p=>p.ArticleAuthores).ThenInclude(p=>p.Article)
                .FirstOrDefaultAsync(m => m.Authorid == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authorspro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authorspro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Authorid,Name,ScannedAuthorimage,AuthorBio,PointerH,Active")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authorspro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authorspro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Authorid,Name,ScannedAuthorimage,AuthorBio,PointerH,Active")] Author author)
        {
            if (id != author.Authorid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Authorid))
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
            return View(author);
        }

        // GET: Authorspro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Authorid == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authorspro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Authorid == id);
        }
    }
}
