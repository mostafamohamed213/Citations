using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Citations.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Citations.Controllers
{
    public class InstitutionsController : Controller
    {
        private readonly CitationContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public InstitutionsController(CitationContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult checkname(String Name, int? Institutionid)
        {
            if (Name == null)
            {
                return Json(data: "الرجاء ادخال اسم صحيح");
            }
            if (Institutionid == null)
            {
                var name = Name.Trim();
                if (_context.Institutions.FirstOrDefault(i => i.Name == name) == null)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                var name = Name.Trim();
                if (_context.Institutions.FirstOrDefault(i => i.Institutionid != Institutionid && i.Name == name) == null)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
        }


        // GET: Institutions
        //public async Task<IActionResult> Index()
        //{
        //    var citationContext = _context.Institutions.Include(i => i.CountryNavigation).Include(i => i.TypeOfInstitutionNavigation);
        //    return View(await citationContext.ToListAsync());
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

            var students = from s in _context.Institutions
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
            return View(await PaginatedList<Institution>.CreateAsync(students.AsNoTracking().Include(i => i.CountryNavigation).Include(i => i.TypeOfInstitutionNavigation), pageNumber ?? 1, pageSize));
        }

        // GET: Institutions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institution = await _context.Institutions
                .Include(i => i.CountryNavigation)
                .Include(i => i.TypeOfInstitutionNavigation)
                .FirstOrDefaultAsync(m => m.Institutionid == id);
            if (institution == null)
            {
                return NotFound();
            }

            return View(institution);
        }

        //get article in magazine
        //public async Task<IActionResult> testgetarticlemagazine(int? id)
        //{
           

        //    var articles = from a in _context.Articles
        //                   join isofis in _context.IssueOfIssues
        //                   on a.ArticleIssue equals isofis.IssueOfIssueid
        //                   join mis in _context.MagazineIssues
        //                   on isofis.MagazineIssueId equals mis.Issueid
        //                   join m in _context.Magazines
        //                   on mis.Magazineid equals m.Magazineid
        //                   where m.Magazineid==id
        //                   select a;

        //    return View(articles.ToList());
        //}

        // GET: Institutions/Create
        public IActionResult Create()
        {
            ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name");
            ViewData["TypeOfInstitution"] = new SelectList(_context.TypeOfInstitutions, "TypeInstitutionid", "TypeName");
            return View();
        }

        // POST: Institutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Institutionid,Name,Country,TypeOfInstitution,ImpactFactor,PointerH,NumberOfCitations,Active")] Institution institution, IFormFile cover, IFormFile logo)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (cover != null)
                    {
                        var date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                        date = date.Replace("/", "-").Replace(":", "-");

                        var fileName = date + Path.GetFileName(cover.FileName);
                        var path = Path.Combine(webHostEnvironment.WebRootPath, "images");
                        string filepath = Path.Combine(path, fileName);
                        //  file.CopyTo(new FileStream(filepath, FileMode.Create));


                        //string path = ""; // From appsettings.json
                        //string filePath = path + $"\\Attachment {i}-" + attachment.FileName;
                        System.IO.StreamWriter files = new System.IO.StreamWriter(filepath);
                        cover.OpenReadStream().CopyTo(files.BaseStream);
                        files.Flush();
                        files.Close();

                        institution.ScannedCoverImage = fileName;

                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }

                try
                {
                    if (logo != null)
                    {
                        var date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                        date = date.Replace("/", "-").Replace(":", "-");

                        var fileName = date + Path.GetFileName(logo.FileName);
                        var path = Path.Combine(webHostEnvironment.WebRootPath, "images");
                        string filepath = Path.Combine(path, fileName);
                        //  file.CopyTo(new FileStream(filepath, FileMode.Create));


                        //string path = ""; // From appsettings.json
                        //string filePath = path + $"\\Attachment {i}-" + attachment.FileName;
                        System.IO.StreamWriter files = new System.IO.StreamWriter(filepath);
                        logo.OpenReadStream().CopyTo(files.BaseStream);
                        files.Flush();
                        files.Close();

                        institution.ScannedLogoImage = fileName;

                    }


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                institution.ImpactFactor = 0;
                institution.NumberOfCitations = 0;
                institution.PointerH = 0;

                _context.Add(institution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", institution.Country);
            ViewData["TypeOfInstitution"] = new SelectList(_context.TypeOfInstitutions, "TypeInstitutionid", "TypeName", institution.TypeOfInstitution);
            return View(institution);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Institutionid,Name,Country,TypeOfInstitution,ScannedCoverImage,ScannedLogoImage,ImpactFactor,PointerH,NumberOfCitations,Active")] Institution institution)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(institution);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", institution.Country);
        //    ViewData["TypeOfInstitution"] = new SelectList(_context.TypeOfInstitutions, "TypeInstitutionid", "TypeName", institution.TypeOfInstitution);
        //    return View(institution);
        //}

        // GET: Institutions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institution = await _context.Institutions.FindAsync(id);
            if (institution == null)
            {
                return NotFound();
            }
            ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", institution.Country);
            ViewData["TypeOfInstitution"] = new SelectList(_context.TypeOfInstitutions, "TypeInstitutionid", "TypeName", institution.TypeOfInstitution);
            return View(institution);
        }

        // POST: Institutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Institutionid,Name,Country,TypeOfInstitution,ScannedCoverImage,ScannedLogoImage,ImpactFactor,PointerH,NumberOfCitations,Active")] Institution institution)
        {
            if (id != institution.Institutionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitutionExists(institution.Institutionid))
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
            ViewData["Country"] = new SelectList(_context.Countries, "Countryid", "Name", institution.Country);
            ViewData["TypeOfInstitution"] = new SelectList(_context.TypeOfInstitutions, "TypeInstitutionid", "TypeName", institution.TypeOfInstitution);
            return View(institution);
        }

        // GET: Institutions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institution = await _context.Institutions
                .Include(i => i.CountryNavigation)
                .Include(i => i.TypeOfInstitutionNavigation)
                .FirstOrDefaultAsync(m => m.Institutionid == id);
            if (institution == null)
            {
                return NotFound();
            }

            return View(institution);
        }

        // POST: Institutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institution = await _context.Institutions.FindAsync(id);
            _context.Institutions.Remove(institution);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitutionExists(int id)
        {
            return _context.Institutions.Any(e => e.Institutionid == id);
        }
    }
}
