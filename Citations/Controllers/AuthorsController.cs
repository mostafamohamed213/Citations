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
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;


namespace Citations.Controllers
{
    public class AuthorsController : Controller
    {
     
        private readonly IHostingEnvironment _environment;
        
           

        
        private readonly CitationContext _context;

        public AuthorsController(CitationContext context, IHostingEnvironment IHostingEnvironment)
        {
            _context = context;
            _environment = IHostingEnvironment;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.Where(a=>a.Active==true||a.Active==false).ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author =  _context.AuthorsPositionInstitutions.Where(m => m.Authorid == id)
                .Include(api=>api.FacultyInstitutionDepartment)
                .ThenInclude(fid=>fid.FacultyInstitution.Institution)
                .Include(fi=>fi.FacultyInstitutionDepartment.Department)
                .Include(fi=>fi.FacultyInstitutionDepartment.FacultyInstitution.Faculty)
                
                .Include(a=>a.PositionJob).Include(a => a.Author)
                .ThenInclude(a=>a.AuthorResearchFields)
                .ThenInclude(arf=>arf.Field)
              ;
            if (author == null)
            {
                return NotFound();
            }    if (author.Count() == 0)
            {

                return NotFound();
            }

            return View(author);
        }


        //public System.Web.Mvc.JsonResult getInstitutions()
        //{
        //    List<Institution> Institution = new List<Institution>();
        //    using (CitationContext dc = new CitationContext())
        //    {
        //        Institution = dc.Institutions.OrderBy(a => a.Name).ToList();
        //    }
        //    return new System.Web.Mvc.JsonResult { Data = Institution, JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet };
        //}

        //public System.Web.Mvc.JsonResult getFaculties(int instid)
        //{
        //    List<Institution> Institution = new List<Institution>();
        //    using (CitationContext dc = new CitationContext())
        //    {
        //        Institution = dc.Institutions.OrderBy(a => a.Name).ToList();
        //    }
        //    return new System.Web.Mvc.JsonResult { Data = Institution, JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet };
        //}
        // GET: Authors/Create
        public IActionResult Create()
        {
            ViewData["Institutions"] = _context.Institutions;
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name");
            ViewData["PositionJobid"] = new SelectList(_context.PositionJobs, "PositionJobid", "PositionJob1");
            ViewData["Fieldid"] = new SelectList(_context.ResearchFields, "Fieldid", "Name");
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Authorid,Name,ScannedAuthorimage,AuthorBio,PointerH,Active")] Author author, IFormFile ScannedAuthorimage,string[] ResarchFields,string[] newenResarchFields, string[] newarResarchFields, string[] positionjob, string[] institutions, string[] Faculty, string[] Department, string MainInstitute)
        {
            //if (ResarchFields.Length==0)
            //{
            //    ViewBag.error = "Resarch Fields Is Required";
            //    return RedirectToAction("create");
            //}
            if (positionjob.Length == 0)
            {
                ViewBag.error = "Position  Is Required";
                return RedirectToAction("create");
            }

            if (!int.TryParse( MainInstitute,out int value) )
            {
                ViewBag.error = "MainInstitute  Is Required";
                return RedirectToAction("create");
            }


            if (ModelState.IsValid)
            {
               
                
                if (ScannedAuthorimage != null)
                {
                    var fileName = string.Empty;
                  
                    var newFileName = string.Empty;


                    //Getting FileName
                    fileName = ContentDispositionHeaderValue.Parse(ScannedAuthorimage.ContentDisposition).FileName.Trim('"');

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = $"{author.Name}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
                    //Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var FileExtension = Path.GetExtension(fileName).ToLower();

                    // concating  FileName + FileExtension
                    newFileName = myUniqueFileName + FileExtension;

                    // Combines two strings into a path.
                    fileName = Path.Combine(_environment.WebRootPath, "AutherImages") + $@"\{newFileName}";

                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        ScannedAuthorimage.CopyTo(fs);
                        fs.Flush();

                    }
                    author.ScannedAuthorimage = newFileName;

                }
                else if (ScannedAuthorimage == null)
                {
                    author.ScannedAuthorimage = "defaultuser.jpg";
                }
                _context.Add(author);
              await _context.SaveChangesAsync();
                List<ResearchField> newresfields = new List<ResearchField>();
                List<AuthorResearchField> autresfield = new List<AuthorResearchField>();
                for  (int i=0;i<newarResarchFields.Length;i++)
                {
                    var exresfild = _context.ResearchFields.FirstOrDefault(rf => rf.Name.Trim().ToLower() == newarResarchFields[i].Trim().ToLower());
                    if (exresfild == null)
                    {

                        newresfields.Add(new ResearchField { Name = newarResarchFields[i], NameEn = newenResarchFields[i], Active = true });


                    }
                    else
                    {
                        autresfield.Add(new AuthorResearchField() {Authorid=author.Authorid,Fieldid=exresfild.Fieldid });
                    }
                }
               
                _context.ResearchFields.AddRange(newresfields);
                _context.SaveChanges();

                var allresearchfields = _context.ResearchFields;
                List<string> ResarchFieldslst = ResarchFields.ToList();
                for (int i = 0; i < newarResarchFields.Length; i++)
                {
                    string resfieldid = allresearchfields.FirstOrDefault(rf => rf.Name.Trim() == newarResarchFields[i].Trim()).Fieldid.ToString();
                ResarchFieldslst.Add(resfieldid);
                }
             
                foreach (var item in ResarchFieldslst)
                {
                    autresfield.Add(new AuthorResearchField() { Authorid = author.Authorid, Fieldid = int.Parse(item) });
                }

                autresfield=autresfield.GroupBy(i => i.Fieldid).Select(i => i.FirstOrDefault()).ToList();

                List<AuthorsPositionInstitution> autherposistions = new List<AuthorsPositionInstitution>();

               
             
                for (int i=1;i< positionjob.Length;i++ )
                {
                    if (int.Parse(Faculty[i]) == 0 && int.Parse(institutions[i]) == 0 && int.Parse(Department[i]) == 0)
                    {
                        continue;
                    }
                    var facins=_context.FacultyInstitutions.FirstOrDefault(fi => fi.Facultyid==  int.Parse(Faculty[i]) && fi.Institutionid==int.Parse(institutions[i])) ;
                    int facinsid=facins.FacultyInstitutionid ;
                    int facinstdepid = _context.FacultyInstitutionDepartments.FirstOrDefault(fid => fid.FacultyInstitutionid == facinsid&&fid.Departmentid==int.Parse(Department[i])).FacultyInstitutionDepartmentid;

                    //     int inst_id = int.Parse(item.Split(" _inst_")[1]);
                    //int posjob_id = int.Parse(item.Split(" _inst_")[0]);
                    if (facins.Institutionid == int.Parse(MainInstitute))
                    {

                        autherposistions.Add(new AuthorsPositionInstitution() { Authorid = author.Authorid, FacultyInstitutionDepartmentid = facinstdepid, PositionJobid = int.Parse(positionjob[i]), MainIntitute = true });
                    }
                    else
                    {
                        autherposistions.Add(new AuthorsPositionInstitution() { Authorid = author.Authorid, FacultyInstitutionDepartmentid = facinstdepid, PositionJobid = int.Parse(positionjob[i]), MainIntitute = false });

                    }
                  
                }


                _context.AuthorsPositionInstitutions.AddRange(autherposistions.Distinct());
                _context.AuthorResearchFields.AddRange(autresfield);
                
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        
              public List<Department> GetDepartment(int facid, int instid)
        {
            List<Department> list = new List<Department>();
           var facinst= _context.FacultyInstitutions.Where(fi=>fi.Facultyid==facid&& fi.Institutionid==instid).FirstOrDefault();
            list = _context.FacultyInstitutionDepartments .Where(f => f.FacultyInstitution == facinst).Select(fi => fi.Department).Distinct().OrderBy(a => a.Name).ToList();
            return list;
            //  return Json(list) ;
        }
        public List<Faculty> GetFuclty(int instid)
        {
            List<Faculty> list = new List<Faculty>();
             list= _context.FacultyInstitutions.Where(f=>f.Institutionid ==instid).Select(fi=>fi.Faculty).OrderBy(a => a.Name).ToList();
            return  list;
          //  return Json(list) ;
        }
        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            var authresfields = _context.AuthorResearchFields.Where(authfild => authfild.Authorid == id);
         
            List<int> fieldid = authresfields.Select(arf => arf.Fieldid).ToList<int>();

            var autherposinst = _context.AuthorsPositionInstitutions.Where(authfild => authfild.Authorid == id);
            List<int> positions = autherposinst.Select(arf => arf.PositionJobid).ToList<int>();
            ViewData["Institutions"] = _context.Institutions;
            ViewData["Institutionid"] = new SelectList(_context.Institutions, "Institutionid", "Name", positions);
            ViewData["PositionJobid"] = new SelectList(_context.PositionJobs, "PositionJobid", "PositionJob1");
            ViewData["autherposithininstitute"] = _context.AuthorsPositionInstitutions.Where(m => m.Authorid == id)
                .Include(api => api.FacultyInstitutionDepartment)
                .ThenInclude(fid => fid.FacultyInstitution.Institution)
                .Include(fi => fi.FacultyInstitutionDepartment.Department)
                .Include(fi => fi.FacultyInstitutionDepartment.FacultyInstitution.Faculty)

                .Include(a => a.PositionJob).Include(a => a.Author)
                .ThenInclude(a => a.AuthorResearchFields)
                .ThenInclude(arf => arf.Field)
              ; ;

            //ViewData["Fieldid"] = new SelectList(_context.ResearchFields, "Fieldid", "Name");
           
            ViewData["Fieldid"] = new MultiSelectList(_context.ResearchFields, "Fieldid", "Name",fieldid);
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Authorid,Name,ScannedAuthorimage,AuthorBio,PointerH,Active")] Author author, IFormFile ScannedAuthorimage, string[] ResarchFields, string[] newenResarchFields, string[] newarResarchFields, string[] positionjob, string[] institutions, string[] Faculty, string[] Department, string MainInstitute)
        {
          
            
            if (ModelState.IsValid)
            {
                try
                {
                    var oldauthor = _context.Authors.AsNoTracking().FirstOrDefault(authr => authr.Authorid == author.Authorid);
                   
                    if (ScannedAuthorimage != null)
                    {
                        var fileName = string.Empty;
                        fileName = Path.Combine(_environment.WebRootPath, "userprofilesimg") + $@"\{oldauthor.ScannedAuthorimage}";
                        using (FileStream fs = System.IO.File.Open(fileName, FileMode.Open))
                        {

                            fs.Close();
                            System.IO.File.Delete(fileName);

                        }
                        var newFileName = string.Empty;


                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(ScannedAuthorimage.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = $"{author.Name}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
                        //Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName).ToLower();

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "AutherImages") + $@"\{newFileName}";

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            ScannedAuthorimage.CopyTo(fs);
                            fs.Flush();

                        }
                        author.ScannedAuthorimage = newFileName;

                    }
                    if (ScannedAuthorimage == null)
                    {

                        author.ScannedAuthorimage = oldauthor.ScannedAuthorimage;





                    }
                    //_context.Entry(author).State = EntityState.Detached;
                    //_context.Entry(author).State = EntityState.Modified;
                    _context.Update(author);
                    
                   var oldresf= _context.AuthorResearchFields.Where(autf => autf.Authorid == id);
                   var oldapi= _context.AuthorsPositionInstitutions.Where(autf => autf.Authorid == id);
                    _context.RemoveRange(oldresf);
                    _context.RemoveRange(oldapi);
                    
                    _context.SaveChanges();
                    //List<AuthorResearchField> autresfield = new List<AuthorResearchField>(); 
                    //var allresearchfields = _context.ResearchFields;
                    //List<string> ResarchFieldslst = ResarchFields.ToList();
                    //for (int i = 0; i < newarResarchFields.Length; i++)
                    //{
                    //    ResarchFieldslst.Add(allresearchfields.FirstOrDefault(rf => rf.Name == newarResarchFields[i] && rf.NameEn == newenResarchFields[i]).Fieldid.ToString());
                    //}

                    //foreach (var item in ResarchFieldslst)
                    //{
                        
                    //    autresfield.Add(new AuthorResearchField() { Authorid = author.Authorid, Fieldid = int.Parse(item) });
                    //}




                    List<ResearchField> newresfields = new List<ResearchField>();
                    List<AuthorResearchField> autresfield = new List<AuthorResearchField>();
                    for (int i = 0; i < newarResarchFields.Length; i++)
                    {
                        var exresfild = _context.ResearchFields.FirstOrDefault(rf => rf.Name.Trim().ToLower() == newarResarchFields[i].Trim().ToLower());
                        if (exresfild == null)
                        {

                            newresfields.Add(new ResearchField { Name = newarResarchFields[i], NameEn = newenResarchFields[i], Active = true });


                        }
                        else
                        {
                            autresfield.Add(new AuthorResearchField() { Authorid = author.Authorid, Fieldid = exresfild.Fieldid });
                        }
                    }

                    _context.ResearchFields.AddRange(newresfields);
                    _context.SaveChanges();

                    var allresearchfields = _context.ResearchFields;
                    List<string> ResarchFieldslst = ResarchFields.ToList();
                    for (int i = 0; i < newarResarchFields.Length; i++)
                    {
                        string resfieldid = allresearchfields.FirstOrDefault(rf => rf.Name.Trim() == newarResarchFields[i].Trim()).Fieldid.ToString();
                        ResarchFieldslst.Add(resfieldid);
                    }

                    foreach (var item in ResarchFieldslst)
                    {
                        autresfield.Add(new AuthorResearchField() { Authorid = author.Authorid, Fieldid = int.Parse(item) });
                    }

                    autresfield = autresfield.GroupBy(i => i.Fieldid).Select(i => i.FirstOrDefault()).ToList();


                    List<AuthorsPositionInstitution> autherposistions = new List<AuthorsPositionInstitution>();



                    for (int i = 1; i < positionjob.Length; i++)
                    {
                        if (int.Parse(Faculty[i]) == 0 && int.Parse(institutions[i]) == 0 && int.Parse(Department[i]) == 0)
                        {
                            continue;
                        }
                        var facins = _context.FacultyInstitutions.FirstOrDefault(fi => fi.Facultyid == int.Parse(Faculty[i]) && fi.Institutionid == int.Parse(institutions[i]));
                        int facinsid = facins.FacultyInstitutionid;
                        int facinstdepid = _context.FacultyInstitutionDepartments.FirstOrDefault(fid => fid.FacultyInstitutionid == facinsid && fid.Departmentid == int.Parse(Department[i])).FacultyInstitutionDepartmentid;

                        //     int inst_id = int.Parse(item.Split(" _inst_")[1]);
                        //int posjob_id = int.Parse(item.Split(" _inst_")[0]);
                        if (facins.Institutionid == int.Parse(MainInstitute))
                        {

                            autherposistions.Add(new AuthorsPositionInstitution() { Authorid = author.Authorid, FacultyInstitutionDepartmentid = facinstdepid, PositionJobid = int.Parse(positionjob[i]), MainIntitute = true });
                        }
                        else
                        {
                            autherposistions.Add(new AuthorsPositionInstitution() { Authorid = author.Authorid, FacultyInstitutionDepartmentid = facinstdepid, PositionJobid = int.Parse(positionjob[i]), MainIntitute = false });

                        }

                    }


                    _context.AuthorsPositionInstitutions.AddRange(autherposistions.Distinct());
                    _context.AuthorResearchFields.AddRange(autresfield);



             
                     await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Authorid))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Authorid == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            author.Active = false;
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Authorid == id);
        }
    }
}
