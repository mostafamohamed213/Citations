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
    public class TypeOfPublishersController : Controller
    {
        private readonly CitationContext _context;

        public TypeOfPublishersController(CitationContext context)
        {
            _context = context;
        }

        // GET: TypeOfPublishers
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeOfPublishers.ToListAsync());
        }

        // GET: TypeOfPublishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfPublisher = await _context.TypeOfPublishers
                .FirstOrDefaultAsync(m => m.TypePublisherid == id);
            if (typeOfPublisher == null)
            {
                return NotFound();
            }

            return View(typeOfPublisher);
        }

        // GET: TypeOfPublishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfPublishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypePublisherid,TypeName")] TypeOfPublisher typeOfPublisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfPublisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfPublisher);
        }

        // GET: TypeOfPublishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfPublisher = await _context.TypeOfPublishers.FindAsync(id);
            if (typeOfPublisher == null)
            {
                return NotFound();
            }
            return View(typeOfPublisher);
        }

        // POST: TypeOfPublishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypePublisherid,TypeName")] TypeOfPublisher typeOfPublisher)
        {
            if (id != typeOfPublisher.TypePublisherid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfPublisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfPublisherExists(typeOfPublisher.TypePublisherid))
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
            return View(typeOfPublisher);
        }

        // GET: TypeOfPublishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfPublisher = await _context.TypeOfPublishers
                .FirstOrDefaultAsync(m => m.TypePublisherid == id);
            if (typeOfPublisher == null)
            {
                return NotFound();
            }

            return View(typeOfPublisher);
        }

        // POST: TypeOfPublishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfPublisher = await _context.TypeOfPublishers.FindAsync(id);
            _context.TypeOfPublishers.Remove(typeOfPublisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfPublisherExists(int id)
        {
            return _context.TypeOfPublishers.Any(e => e.TypePublisherid == id);
        }
    }
}
