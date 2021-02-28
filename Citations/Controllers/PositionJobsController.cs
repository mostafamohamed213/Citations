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
    public class PositionJobsController : Controller
    {
        private readonly CitationContext _context;

        public PositionJobsController(CitationContext context)
        {
            _context = context;
        }

        // GET: PositionJobs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PositionJobs.ToListAsync());
        }

        // GET: PositionJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positionJob = await _context.PositionJobs
                .FirstOrDefaultAsync(m => m.PositionJobid == id);
            if (positionJob == null)
            {
                return NotFound();
            }

            return View(positionJob);
        }

        // GET: PositionJobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PositionJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PositionJobid,PositionJob1,Active")] PositionJob positionJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(positionJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(positionJob);
        }

        // GET: PositionJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positionJob = await _context.PositionJobs.FindAsync(id);
            if (positionJob == null)
            {
                return NotFound();
            }
            return View(positionJob);
        }

        // POST: PositionJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PositionJobid,PositionJob1,Active")] PositionJob positionJob)
        {
            if (id != positionJob.PositionJobid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(positionJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionJobExists(positionJob.PositionJobid))
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
            return View(positionJob);
        }

        // GET: PositionJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positionJob = await _context.PositionJobs
                .FirstOrDefaultAsync(m => m.PositionJobid == id);
            if (positionJob == null)
            {
                return NotFound();
            }

            return View(positionJob);
        }

        // POST: PositionJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var positionJob = await _context.PositionJobs.FindAsync(id);
            _context.PositionJobs.Remove(positionJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionJobExists(int id)
        {
            return _context.PositionJobs.Any(e => e.PositionJobid == id);
        }
    }
}
