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
//    public class AuthorsPositionInstitutionsController : Controller
//    {
//        private readonly CitationContext _context;

//        public AuthorsPositionInstitutionsController(CitationContext context)
//        {
//            _context = context;
//        }

//        // GET: AuthorsPositionInstitutions
//        public async Task<IActionResult> Index()
//        {
//            var citationContext = _context.AuthorsPositionInstitutions.Include(a => a.Author).Include(a => a.Institution).Include(a => a.PositionJob);
//            return View(await citationContext.ToListAsync());
//        }

//        // GET: AuthorsPositionInstitutions/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var authorsPositionInstitution = await _context.AuthorsPositionInstitutions
//                .Include(a => a.Author)
//                .Include(a => a.Institution)
//                .Include(a => a.PositionJob)
//                .FirstOrDefaultAsync(m => m.AuthorsPositionInstitutionId == id);
//            if (authorsPositionInstitution == null)
//            {
//                return NotFound();
//            }

//            return View(authorsPositionInstitution);
//        }

//        // GET: AuthorsPositionInstitutions/Create
//        public IActionResult Create()
//        {
//            ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name");
//            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name");
//            ViewData["PositionJobid"] = new SelectList(_context.PositionJobs, "PositionJobid", "PositionJob1");
//            return View();
//        }

//        // POST: AuthorsPositionInstitutions/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("AuthorsPositionInstitutionId,Authorid,Institutionid,PositionJobid,MainIntitute")] AuthorsPositionInstitution authorsPositionInstitution)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(authorsPositionInstitution);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name", authorsPositionInstitution.Authorid);
//            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", authorsPositionInstitution.Institutionid);
//            ViewData["PositionJobid"] = new SelectList(_context.PositionJobs, "PositionJobid", "PositionJob1", authorsPositionInstitution.PositionJobid);
//            return View(authorsPositionInstitution);
//        }

//        // GET: AuthorsPositionInstitutions/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var authorsPositionInstitution = await _context.AuthorsPositionInstitutions.FindAsync(id);
//            if (authorsPositionInstitution == null)
//            {
//                return NotFound();
//            }
//            ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name", authorsPositionInstitution.Authorid);
//            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", authorsPositionInstitution.Institutionid);
//            ViewData["PositionJobid"] = new SelectList(_context.PositionJobs, "PositionJobid", "PositionJob1", authorsPositionInstitution.PositionJobid);
//            return View(authorsPositionInstitution);
//        }

//        // POST: AuthorsPositionInstitutions/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("AuthorsPositionInstitutionId,Authorid,Institutionid,PositionJobid,MainIntitute")] AuthorsPositionInstitution authorsPositionInstitution)
//        {
//            if (id != authorsPositionInstitution.AuthorsPositionInstitutionId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(authorsPositionInstitution);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!AuthorsPositionInstitutionExists(authorsPositionInstitution.AuthorsPositionInstitutionId))
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
//            ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name", authorsPositionInstitution.Authorid);
//            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", authorsPositionInstitution.Institutionid);
//            ViewData["PositionJobid"] = new SelectList(_context.PositionJobs, "PositionJobid", "PositionJob1", authorsPositionInstitution.PositionJobid);
//            return View(authorsPositionInstitution);
//        }

//        // GET: AuthorsPositionInstitutions/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var authorsPositionInstitution = await _context.AuthorsPositionInstitutions
//                .Include(a => a.Author)
//                .Include(a => a.Institution)
//                .Include(a => a.PositionJob)
//                .FirstOrDefaultAsync(m => m.AuthorsPositionInstitutionId == id);
//            if (authorsPositionInstitution == null)
//            {
//                return NotFound();
//            }

//            return View(authorsPositionInstitution);
//        }

//        // POST: AuthorsPositionInstitutions/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var authorsPositionInstitution = await _context.AuthorsPositionInstitutions.FindAsync(id);
//            _context.AuthorsPositionInstitutions.Remove(authorsPositionInstitution);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool AuthorsPositionInstitutionExists(int id)
//        {
//            return _context.AuthorsPositionInstitutions.Any(e => e.AuthorsPositionInstitutionId == id);
//        }
//    }
//}
