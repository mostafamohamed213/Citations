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
    public class AuthorResearchFieldsController : Controller
    {
        private readonly CitationContext _context;

        public AuthorResearchFieldsController(CitationContext context)
        {
            _context = context;
        }

        // GET: AuthorResearchFields
        public async Task<IActionResult> Index(int? id)
        {
            var citationContext = _context.AuthorResearchFields.Where(autres=>autres.Authorid==id).Include(a => a.Author).Include(a => a.Field);
            return View(await citationContext.ToListAsync());
        }

        // GET: AuthorResearchFields/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorResearchField = await _context.AuthorResearchFields
                .Include(a => a.Author)
                .Include(a => a.Field)
                .FirstOrDefaultAsync(m => m.Authorid == id);
            if (authorResearchField == null)
            {
                return NotFound();
            }

            return View(authorResearchField);
        }

        // GET: AuthorResearchFields/Create
        public IActionResult Create()
        {
            ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name");
            ViewData["Fieldid"] = new SelectList(_context.ResearchFields, "Fieldid", "Name");
            return View();
        }

        // POST: AuthorResearchFields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Authorid,Fieldid")] string Authorid,string[] Fieldid)
        {
            try
            {
                foreach (string item in Fieldid)
                {
                    _context.Add(new AuthorResearchField { Authorid = int.Parse(Authorid), Fieldid = int.Parse(item) });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
               string s= e.Message;
                ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name", Authorid);
                ViewData["Fieldid"] = new SelectList(_context.ResearchFields, "Fieldid", "Name", Fieldid);
                return View();
         
            }
          
                
              
                
            
         

           
        }
        public JsonResult AutherChanged(string autherid)
        {
            var selectedauthor = _context.AuthorResearchFields.Where(arf => arf.Authorid == int.Parse(autherid));
            var authorresfeild = _context.AuthorResearchFields.Except(selectedauthor);
            var resfields = authorresfeild.Select(rf => rf.Field).Distinct();
            return Json(resfields);
        }
        

        // GET: AuthorResearchFields/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorResearchField = await _context.AuthorResearchFields.FindAsync(id);
            if (authorResearchField == null)
            {
                return NotFound();
            }
            ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name", authorResearchField.Authorid);
            ViewData["Fieldid"] = new SelectList(_context.ResearchFields, "Fieldid", "Name", authorResearchField.Fieldid);
            return View(authorResearchField);
        }

        // POST: AuthorResearchFields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Authorid,Fieldid")] AuthorResearchField authorResearchField)
        {
            if (id != authorResearchField.Authorid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorResearchField);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorResearchFieldExists(authorResearchField.Authorid))
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
            ViewData["Authorid"] = new SelectList(_context.Authors, "Authorid", "Name", authorResearchField.Authorid);
            ViewData["Fieldid"] = new SelectList(_context.ResearchFields, "Fieldid", "Name", authorResearchField.Fieldid);
            return View(authorResearchField);
        }

        // GET: AuthorResearchFields/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorResearchField = await _context.AuthorResearchFields
                .Include(a => a.Author)
                .Include(a => a.Field)
                .FirstOrDefaultAsync(m => m.Authorid == id);
            if (authorResearchField == null)
            {
                return NotFound();
            }

            return View(authorResearchField);
        }
      public  class AutherField  {
            public int AutherId { get; set; }
            public int FieldId { get; set; }
       
               

            }
    // POST: AuthorResearchFields/Delete/5
    [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed([FromBody] AutherField AutherField)
        {
            
            var authorResearchField = await _context.AuthorResearchFields.FirstOrDefaultAsync(authfield=>authfield.Fieldid==AutherField.FieldId&&authfield.Authorid==AutherField.AutherId);
            _context.AuthorResearchFields.Remove(authorResearchField);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorResearchFieldExists(int id)
        {
            return _context.AuthorResearchFields.Any(e => e.Authorid == id);
        }
    }
}
