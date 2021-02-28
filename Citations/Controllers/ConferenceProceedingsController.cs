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
    public class ConferenceProceedingsController : Controller
    {
        private readonly CitationContext _context;

        public ConferenceProceedingsController(CitationContext context)
        {
            _context = context;
        }

        // GET: ConferenceProceedings
        public async Task<IActionResult> Index()
        {
            var citationContext = _context.ConferenceProceedings.Include(c => c.CountryNavigation).Include(c => c.Publisher);
            return View(await citationContext.ToListAsync());
        }

        // GET: ConferenceProceedings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceProceeding = await _context.ConferenceProceedings
                .Include(c => c.CountryNavigation)
                .Include(c => c.Publisher)
                .FirstOrDefaultAsync(m => m.Conferenceid == id);
            if (conferenceProceeding == null)
            {
                return NotFound();
            }

            return View(conferenceProceeding);
        }

        // GET: ConferenceProceedings/Create
        public IActionResult Create()
        {
            ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name");
            ViewData["Publisherid"] = new SelectList(_context.Publishers, "Publisherid", "Address");
            return View();
        }

        // POST: ConferenceProceedings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Conferenceid,Conferencetittle,Year,ConferencePublicationName,Country,Publisherid,Active")] ConferenceProceeding conferenceProceeding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conferenceProceeding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", conferenceProceeding.Country);
            ViewData["Publisherid"] = new SelectList(_context.Publishers, "Publisherid", "Address", conferenceProceeding.Publisherid);
            return View(conferenceProceeding);
        }

        // GET: ConferenceProceedings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceProceeding = await _context.ConferenceProceedings.FindAsync(id);
            if (conferenceProceeding == null)
            {
                return NotFound();
            }
            ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", conferenceProceeding.Country);
            ViewData["Publisherid"] = new SelectList(_context.Publishers, "Publisherid", "Address", conferenceProceeding.Publisherid);
            return View(conferenceProceeding);
        }

        // POST: ConferenceProceedings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Conferenceid,Conferencetittle,Year,ConferencePublicationName,Country,Publisherid,Active")] ConferenceProceeding conferenceProceeding)
        {
            if (id != conferenceProceeding.Conferenceid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conferenceProceeding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConferenceProceedingExists(conferenceProceeding.Conferenceid))
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
            ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", conferenceProceeding.Country);
            ViewData["Publisherid"] = new SelectList(_context.Publishers, "Publisherid", "Address", conferenceProceeding.Publisherid);
            return View(conferenceProceeding);
        }

        // GET: ConferenceProceedings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceProceeding = await _context.ConferenceProceedings
                .Include(c => c.CountryNavigation)
                .Include(c => c.Publisher)
                .FirstOrDefaultAsync(m => m.Conferenceid == id);
            if (conferenceProceeding == null)
            {
                return NotFound();
            }

            return View(conferenceProceeding);
        }

        // POST: ConferenceProceedings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conferenceProceeding = await _context.ConferenceProceedings.FindAsync(id);
            _context.ConferenceProceedings.Remove(conferenceProceeding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConferenceProceedingExists(int id)
        {
            return _context.ConferenceProceedings.Any(e => e.Conferenceid == id);
        }
    }
}
