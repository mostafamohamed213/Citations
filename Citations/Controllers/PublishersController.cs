using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Citations.Models;
using System.Collections.ObjectModel;

namespace Citations.Controllers
{
    public class PublishersController : Controller
    {
        private readonly CitationContext _context;

        public PublishersController(CitationContext context)
        {
            _context = context;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            var citationContext = _context.Publishers.Include(p => p.CountryNavigation).Include(p=>p.Institution);
            return View(await citationContext.ToListAsync());
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
         
            var publisher = await _context.Publishers
                .Include(p => p.CountryNavigation)
                .Include( a => a.Institution).DefaultIfEmpty()
                .Include(c => c.TypeOfPublisherNavigation).DefaultIfEmpty()
                .FirstOrDefaultAsync(m => m.Publisherid == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name");
            ViewData["TypeOfPublisher"] = new SelectList(_context.TypeOfPublishers, "TypePublisherid", "TypeName", "--Select Type--");
            IQueryable<Institution> existedinst = _context.Publishers.Select(p => p.Institution).Distinct();
            ViewData["Institutions"] = new SelectList(_context.Institutions.Except(existedinst), "Institutionid", "Name");
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Publisher publisher)
        {
            try 
            {
                if (publisher.Name != null)
                {
                    publisher.Institutionid = null;
                }


                //if (publisher.Name != null)
                //{
                //    publisher.Institutionid = null;
                //}


                if (publisher.Name == null)
                {
                    publisher.Name = string.Empty;
                }


                //if (publisher.Institutionid.HasValue==false && publisher.TypeOfPublisher.HasValue==false)
                //{
                //    ViewData["TypeOfPublisher"] = new SelectList(_context.TypeOfPublishers, "TypePublisherid", "TypeName", "--Select Type--");

                //    IQueryable<Institution> existedinst = _context.Publishers.Select(p => p.Institution).Distinct();
                //    ViewData["Institutions"] = new SelectList(_context.Institutions.Except(existedinst), "Institutionid", "Name");
                //    ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", publisher.Country);
                //    return View(publisher);
                //}
                //if (publisher.TypeOfPublisher==null &&publisher.Institutionid.HasValue)
                //{
                //    publisher.Name = _context.Institutions.FirstOrDefault(ins => ins.Institutionid == publisher.Institutionid).Name;
                //}

                _context.Add(publisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e) {
                string s = e.Message;
            ViewData["TypeOfPublisher"] = new SelectList(_context.TypeOfPublishers, "TypePublisherid", "TypeName", "--Select Type--");
                IQueryable<Institution> existedinst = _context.Publishers.Select(p => p.Institution).Distinct();
                ViewData["Institutions"] = new SelectList(_context.Institutions.Except(existedinst), "Institutionid", "Name");
                ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", publisher.Country);
            return View(publisher);
            }
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            ViewData["TypeOfPublisher"] = new SelectList(_context.TypeOfPublishers, "TypePublisherid", "TypeName", publisher.TypeOfPublisher);
            var thisinst = _context.Institutions.Where(ins => ins.Institutionid == _context.Publishers.FirstOrDefault(p=>p.Publisherid==id).Institutionid);
            IQueryable<Institution> existedinst = _context.Publishers.Select(p => p.Institution).Except(thisinst).Distinct();
            ViewData["Institutions"] = new SelectList(_context.Institutions.Except(existedinst), "Institutionid", "Name");
            ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", publisher.Country);
       
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Publisherid,Name,Country,Address,Active,Institutionid,TypePublisherid")] Publisher publisher)
        {
            if (id != publisher.Publisherid)
            {
                return NotFound();
            }

           
                try
                {
                //if (publisher.Institutionid.HasValue == false && publisher.TypeOfPublisher.HasValue == false)
                //{
                //    ViewData["TypeOfPublisher"] = new SelectList(_context.TypeOfPublishers, "TypePublisherid", "TypeName", "--Select Type--");
                //    var thisinst = _context.Institutions.Where(ins => ins.Institutionid == id);
                //    IQueryable<Institution> existedinst = _context.Publishers.Select(p => p.Institution).Except(thisinst).Distinct();
                //    ViewData["Institutions"] = new SelectList(_context.Institutions.Except(existedinst), "Institutionid", "Name");
                //    ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", publisher.Country);
                //    return View(publisher);
                //}


                if (publisher.Name != null)
                    {
                        publisher.Institutionid = null;
                    }
                    

                    if (publisher.Name == null)
                    {
                        publisher.Name = publisher.Name = string.Empty;
                    }


                    _context.Update(publisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                return View(publisher);
                if (!PublisherExists(publisher.Publisherid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", publisher.Country);
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .Include(p => p.CountryNavigation)
                .FirstOrDefaultAsync(m => m.Publisherid == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
             publisher.Active = false;
            _context.Publishers.Update(publisher);
          
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.Publisherid == id);
        }
    }
}
