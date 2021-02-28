using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Citations.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Citations.Controllers
{
    public class AuthorProfileController : Controller
    {
        private readonly CitationContext _context;
        
        public AuthorProfileController(CitationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.Where(a => a.Active == true || a.Active == false).ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _context.AuthorsPositionInstitutions.Where(m => m.Authorid == id)
                .Include(api => api.FacultyInstitutionDepartment)
                .ThenInclude(fid => fid.FacultyInstitution.Institution)
                .Include(fi => fi.FacultyInstitutionDepartment.Department)
                .Include(fi => fi.FacultyInstitutionDepartment.FacultyInstitution.Faculty)

                .Include(a => a.PositionJob).Include(a => a.Author)
                .ThenInclude(a => a.AuthorResearchFields)
                .ThenInclude(arf => arf.Field).Include(p=>p.Author).ThenInclude(p=>p.ArticleAuthores).ThenInclude(p=>p.Author)
              ;
            if (author == null)
            {
                return NotFound();
            }
            if (author.Count() == 0)
            {

                return NotFound();
            }

            return View(author);
        }
    }
}

