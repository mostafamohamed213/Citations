using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Citations.Models;
using System.Text.Json;

namespace Citations.Controllers
{
    public class MagazinesController : Controller
    {
        private readonly CitationContext _context;

        public MagazinesController(CitationContext context)
        {
            _context = context;
        }

        // GET: Magazines
        public async Task<IActionResult> Index()
        {
            var citationContext = _context.Magazines.Include(m => m.Institution).Include(a=>a.Publisher);
            return View(await citationContext.Where(a=>a.Active==true).ToListAsync());
        }

        // GET: Magazines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _context.Magazines
                .Include(m => m.Institution)
                .Include(m => m.Publisher)
                .FirstOrDefaultAsync(m => m.Magazineid == id);
            if (magazine == null)
            {
                return NotFound();
            }

            return View(magazine);
        }
        public IActionResult AddField()
        {
            ResearchField res = new ResearchField();
            return PartialView("_AddFieldPartialView", res);
        }
        [HttpPost]
        public async Task<IActionResult> AddField(ResearchField researchField)
         {
            if (ModelState.IsValid)
            {
                researchField.Active = true;
                _context.Add(researchField);
                await _context.SaveChangesAsync();
                return PartialView("_AddFieldPartialView", researchField);
            }
            return PartialView("_AddFieldPartialView", researchField);
        }
        public  JsonResult researchfields()
        {
            return Json(new MultiSelectList(_context.ResearchFields.Where(a => a.Active == true), "Fieldid", "Name"));
        }

        // GET: Magazines/Create
        public IActionResult Create()
        {
            //IEnumerable<ResearchField> Fields = await _context.ResearchFields.Where(a=>a.Active==true).ToListAsync();
            //ViewBag.ResearchFields = Fields;
            ViewBag.Fieldid = new MultiSelectList(_context.ResearchFields.Where(a => a.Active == true), "Fieldid", "Name");
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name");
            ViewData["Publisherid"] = new SelectList(_context.Publishers, "Publisherid", "Name");
            return View();
        }

        // POST: Magazines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Name,Isbn,ImpactFactor,ImmediateCoefficient,AppropriateValue,NumberOfCitations,Institutionid,ResearchFields")]*/ Magazine magazine/*, IEnumerable<int> Field*/)
        {
            if (ModelState.IsValid)
            {
                magazine.Active = true;
                _context.Add(magazine);
                await _context.SaveChangesAsync();
                if (magazine.ResearchFields != null)
                {
                    foreach (var item in magazine.ResearchFields)
                    {
                        var magazineResearchField = new MagazineResearchField() { Fieldid = item, Magazineid = magazine.Magazineid };
                        _context.Add(magazineResearchField);

                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", magazine.Institutionid);
            ViewData["Publisherid"] = new SelectList(_context.Publishers, "Publisherid", "Name", magazine.Publisherid);
            return View(magazine);
        }

        // GET: Magazines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _context.Magazines.FindAsync(id);
            if (magazine == null)
            {
                return NotFound();
            }
            IEnumerable<int> Fields = await _context.MagazineResearchFields.Where(a => a.Magazineid == id).Select(a => a.Field.Fieldid).ToListAsync();
            //ViewBag.ResearchFields = Fields;
            magazine.ResearchFields = Fields.ToArray();
            ViewBag.Fieldid = new MultiSelectList(_context.ResearchFields.Where(a => a.Active == true), "Fieldid", "Name");
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", magazine.Institutionid);
            ViewData["Publisherid"] = new SelectList(_context.Publishers, "Publisherid", "Name", magazine.Publisherid);
            return View(magazine);
        }

        // POST: Magazines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("Magazineid,Name,Isbn,ImpactFactor,ImmediateCoefficient,AppropriateValue,NumberOfCitations,Institutionid,Active,ResearchFields")] */Magazine magazine/*, IEnumerable<int> Field*/)
        {

            if (id != magazine.Magazineid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //IEnumerable<int> Fields = await _context.MagazineResearchFields.Where(a => a.Magazineid == id).Select(a => a.Fieldid).ToListAsync();
                //IEnumerable<int> UnCheckedField = Fields.Except(Field);
                try
                {
                    _context.Update(magazine);
                    IEnumerable<MagazineResearchField> fields = _context.MagazineResearchFields.Where(e => e.Magazineid == id).ToList();
                    foreach (var item in fields)
                    {
                        _context.Remove(item);
                    }
                    if(magazine.ResearchFields!=null)
                    {
                        foreach (var field in magazine.ResearchFields)
                        {
                            var magazineResearchField = new MagazineResearchField() { Fieldid = field, Magazineid = id };
                            _context.Add(magazineResearchField);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagazineExists(magazine.Magazineid))
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
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", magazine.Institutionid);
            ViewData["Publisherid"] = new SelectList(_context.Publishers, "Publisherid", "Name", magazine.Publisherid);
            return View(magazine);
        }

        // GET: Magazines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazine = await _context.Magazines
                .Include(m => m.Institution)
                .FirstOrDefaultAsync(m => m.Magazineid == id);
            if (magazine == null)
            {
                return NotFound();
            }

            return View(magazine);
        }

        // POST: Magazines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var magazine = await _context.Magazines.FindAsync(id);
            magazine.Active = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        public JsonResult CheckIsbn(string Isbn, int? Magazineid)
        {
            if (Magazineid == null)
            {
                return Json(!_context.Magazines.Any(e => e.Isbn == Isbn));

            }
            else
            {

                if (_context.Magazines.Any(e => e.Isbn == Isbn && e.Magazineid==Magazineid))
                {
                    return Json(true);
                }
                else if (_context.Magazines.Any(e => e.Isbn == Isbn))
                {
                    return Json(false);
                }
                return Json(true);


            }
        }
        public JsonResult CheckName(string Name,int? Magazineid)
        {
            var name = Name.Trim();
            if (Magazineid == null)
            {
                return Json(!_context.Magazines.Any(e => e.Name == name));

            }
            else
            {

                if (_context.Magazines.Any(e => e.Name == name && e.Magazineid == Magazineid))
                {
                    return Json(true);
                }
                else if (_context.Magazines.Any(e => e.Name == name))
                {
                    return Json(false);
                }
                return Json(true);


            }



        }
        private bool MagazineExists(int id)
        {
            return _context.Magazines.Any(e => e.Magazineid == id);
        }
    }
}
