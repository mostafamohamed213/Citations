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
        //public async Task<IActionResult> Index()
        //{
        //    var citationContext = _context.Magazines.Include(m => m.Institution).Include(a=>a.Publisher);
        //    return View(await citationContext.Where(a=>a.Active==true).ToListAsync());
        //}

        public async Task<IActionResult> Index(
    string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            //  ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.Magazines
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString));
            }
            //switch (sortOrder)
            //{
            //    //3kstha 
            //    case "name_desc":
            //        students = students.OrderBy(s => s.NumberOfCitations);
            //        break;
            //    //case "Date":
            //    //    students = students.OrderBy(s => s.EnrollmentDate);
            //    //    break;
            //    //case "date_desc":
            //    //    students = students.OrderByDescending(s => s.EnrollmentDate);
            //    //    break;
            //    default:
            //        students = students.OrderByDescending(s => s.NumberOfCitations);
            //        break;
            //}

            int pageSize = 5;
            return View(await PaginatedList<Magazine>.CreateAsync(students.AsNoTracking().Include(m => m.Institution), pageNumber ?? 1, pageSize));
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

            IEnumerable<int> Fields = await _context.MagazineResearchFields.Where(a => a.Magazineid == id).Select(a => a.Field.Fieldid).ToListAsync();
            //ViewBag.ResearchFields = Fields;
            magazine.ResearchFields = Fields.ToArray();
            var rFields = _context.ResearchFields.Where(a => a.Active == true).Select(s => new
            {
                Fieldid = s.Fieldid,
                Name = string.Format("{0} - {1}", s.Name, s.NameEn)
            }).ToList();
            ViewBag.Fieldid = new MultiSelectList(rFields, "Fieldid", "Name");

            var publisher = _context.Publishers.Include(a => a.Institution).Where(a => a.Active == true && a.Institutionid != null).Select(s => new
            {
                Publisherid = s.Publisherid,
                Name = s.Institution.Name
            }).ToList();
            publisher.AddRange(_context.Publishers.Include(a => a.Institution).Where(a => a.Active == true && a.Institutionid == null).Select(s => new
            {
                Publisherid = s.Publisherid,
                Name = s.Name
            }).ToList());
            ViewData["Publisherid"] = new SelectList(publisher, "Publisherid", "Name");

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
            var Fields = _context.ResearchFields.Where(a => a.Active == true).Select(s => new
            {
                Fieldid = s.Fieldid,
                Name = string.Format("{0} - {1}", s.Name, s.NameEn)
            }).ToList();
            var publisher = _context.Publishers.Include(a=>a.Institution).Where(a => a.Active == true && a.Institutionid!=null).Select(s => new
            {
                Publisherid = s.Publisherid,
                Name = s.Institution.Name
            }).ToList();
            publisher.AddRange(_context.Publishers.Include(a => a.Institution).Where(a => a.Active == true && a.Institutionid == null).Select(s => new
            {
                Publisherid = s.Publisherid,
                Name = s.Name
            }).ToList());
            ViewBag.Fieldid = new MultiSelectList(Fields, "Fieldid", "Name");
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name");
            ViewData["Publisherid"] = new SelectList(publisher, "Publisherid", "Name");
            return View();
        }

        // POST: Magazines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Name,Isbn,ImpactFactor,ImmediateCoefficient,AppropriateValue,NumberOfCitations,Institutionid,ResearchFields")]*/ Magazine magazine,/* string[] ResarchFields*/ string[] newenResarchFields, string[] newarResarchFields)
        {
            if (ModelState.IsValid)
            {
                magazine.Active = true;
                magazine.NumberOfCitations = 0;
                magazine.NumberOfCitations = 0;
                magazine.ImpactFactor = 0;
                magazine.AppropriateValue = 0;

                _context.Add(magazine);
                await _context.SaveChangesAsync();
                if (magazine.ResearchFields != null)
                {
                    foreach (var item in magazine.ResearchFields)
                    {
                        var magazineResearchField = new MagazineResearchField() { Fieldid = item, Magazineid = magazine.Magazineid };
                        _context.Add(magazineResearchField);
                    }
                    _context.SaveChanges();
                }
                if (newarResarchFields != null)
                {
                    if(newenResarchFields.Length==0&& newenResarchFields.Contains(""))
                    {
                        for (int i = 0; i < newarResarchFields.Length; i++)
                        {
                            if (!_context.ResearchFields.Any(e => e.Name.Trim().ToLower() == newarResarchFields[i].Trim().ToLower() /*&& e.NameEn==newenResarchFields[i]*/))
                            {
                                var researchField = new ResearchField() { Name = newarResarchFields[i].Trim().ToLower(), NameEn = newenResarchFields[i].Trim().ToLower(), Active = true };
                                _context.ResearchFields.Add(researchField);
                                _context.SaveChanges();
                                var magazineResearchField = new MagazineResearchField() { Fieldid = researchField.Fieldid, Magazineid = magazine.Magazineid };
                                _context.Add(magazineResearchField);
                                _context.SaveChanges();

                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < newarResarchFields.Length; i++)
                        {
                            if (!_context.ResearchFields.Any(e => e.Name.Trim().ToLower() == newarResarchFields[i].Trim().ToLower() /*&& e.NameEn==newenResarchFields[i]*/))
                            {
                                var researchField = new ResearchField() { Name = newarResarchFields[i].Trim().ToLower(), Active = true };
                                _context.ResearchFields.Add(researchField);
                                _context.SaveChanges();
                                var magazineResearchField = new MagazineResearchField() { Fieldid = researchField.Fieldid, Magazineid = magazine.Magazineid };
                                _context.Add(magazineResearchField);
                                _context.SaveChanges();

                            }
                        }

                    }
                }
                
                return RedirectToAction(nameof(Index));
            }
            var Fields = _context.ResearchFields.Where(a => a.Active == true).Select(s => new
            {
                Fieldid = s.Fieldid,
                Name = string.Format("{0} - {1}", s.Name, s.NameEn)
            }).ToList();
            var publisher = _context.Publishers.Include(a => a.Institution).Where(a => a.Active == true && a.Institutionid != null).Select(s => new
            {
                Publisherid = s.Publisherid,
                Name = s.Institution.Name
            }).ToList();
            publisher.AddRange(_context.Publishers.Include(a => a.Institution).Where(a => a.Active == true && a.Institutionid == null).Select(s => new
            {
                Publisherid = s.Publisherid,
                Name = s.Name
            }).ToList());
            ViewData["Publisherid"] = new SelectList(publisher, "Publisherid", "Name");
            ViewBag.Fieldid = new MultiSelectList(Fields, "Fieldid", "Name");
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name");
          
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
            var publisher = _context.Publishers.Include(a => a.Institution).Where(a => a.Active == true && a.Institutionid != null).Select(s => new
            {
                Publisherid = s.Publisherid,
                Name = s.Institution.Name
            }).ToList();
            publisher.AddRange(_context.Publishers.Include(a => a.Institution).Where(a => a.Active == true && a.Institutionid == null).Select(s => new
            {
                Publisherid = s.Publisherid,
                Name = s.Name
            }).ToList());
            ViewData["Publisherid"] = new SelectList(publisher, "Publisherid", "Name");

            var rFields = _context.ResearchFields.Where(a => a.Active == true).Select(s => new
            {
                Fieldid = s.Fieldid,
                Name = string.Format("{0} - {1}", s.Name, s.NameEn)
            }).ToList();
            ViewBag.Fieldid = new MultiSelectList(rFields, "Fieldid", "Name");
            //ViewBag.Fieldid = new MultiSelectList(_context.ResearchFields.Where(a => a.Active == true), "Fieldid", "Name");
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", magazine.Institutionid);
         
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
            IEnumerable<int> Fields = await _context.MagazineResearchFields.Where(a => a.Magazineid == id).Select(a => a.Field.Fieldid).ToListAsync();
            //ViewBag.ResearchFields = Fields;
            magazine.ResearchFields = Fields.ToArray();
            var publisher = _context.Publishers.Include(a => a.Institution).Where(a => a.Active == true && a.Institutionid != null).Select(s => new
            {
                Publisherid = s.Publisherid,
                Name = s.Institution.Name
            }).ToList();
            publisher.AddRange(_context.Publishers.Include(a => a.Institution).Where(a => a.Active == true && a.Institutionid == null).Select(s => new
            {
                Publisherid = s.Publisherid,
                Name = s.Name
            }).ToList());
            ViewData["Publisherid"] = new SelectList(publisher, "Publisherid", "Name");

            var rFields = _context.ResearchFields.Where(a => a.Active == true).Select(s => new
            {
                Fieldid = s.Fieldid,
                Name = string.Format("{0} - {1}", s.Name, s.NameEn)
            }).ToList();
            ViewBag.Fieldid = new MultiSelectList(rFields, "Fieldid", "Name");
            //ViewBag.Fieldid = new MultiSelectList(_context.ResearchFields.Where(a => a.Active == true), "Fieldid", "Name");
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", magazine.Institutionid);
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
            if (Name == null)
            {
                return Json(data: "الرجاء ادخال اسم صحيح");
            }
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
