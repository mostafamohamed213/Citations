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
    public class ArticlesController : Controller
    {
        private readonly CitationContext _context;
        public static List<articleauthors1> asd;
        public ArticlesController(CitationContext context)
        {
            _context = context;
        }


        // GET: Articles
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

            var students = from s in _context.Articles
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Articletittle.Contains(searchString));
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
            return View(await PaginatedList<Article>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult checkname(String Articletittle, int? Articleid)
        {
            if (Articletittle == null)
            {
                return Json(data: "الرجاء ادخال اسم صحيح");
            }

            if (Articleid == null)
            {
                var Articletittle1 = Articletittle.Trim();
                if (_context.Articles.FirstOrDefault(i => i.Articletittle == Articletittle1) == null)
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
                var Articletittle1 = Articletittle.Trim();
                if (_context.Articles.FirstOrDefault(i => i.Articleid != Articleid && i.Articletittle == Articletittle1) == null)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
        }

        public IActionResult checkenname(String ArticletittleEn, int? Articleid)
        {
            if (ArticletittleEn == null)
            {
                return Json(data: "الرجاء ادخال اسم صحيح");
            }

            if (Articleid == null)
            {
                var Articletittle1 = ArticletittleEn.Trim();
                if (_context.Articles.FirstOrDefault(i => i.ArticletittleEn == Articletittle1) == null)
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
                var Articletittle1 = ArticletittleEn.Trim();
                if (_context.Articles.FirstOrDefault(i => i.Articleid != Articleid && i.ArticletittleEn == Articletittle1) == null)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
        }

        // GET: Articles/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = _context.Articles.Include(p => p.ArticleReferenceArticles).ThenInclude(p=>p.Articleref).Include(p => p.ArticleReferenceArticles).ThenInclude(p => p.Book).Include(p => p.ArticleReferenceArticles).ThenInclude(p => p.Conference).Include(p => p.ArticleAuthores).ThenInclude(p=>p.Author).Include(p => p.ArticlesKeywords).Include(p => p.ArticleIssueNavigation).ThenInclude(p => p.MagazineIssue).ThenInclude(p => p.Magazine).Where(a => a.Articleid == id).Single();

            if (article == null)
            {
                return NotFound();
            }

            ViewData["Authors"] = new SelectList(_context.Authors, "Authorid", "Name");
            ViewData["institution"] = new SelectList(_context.Institutions, "Institutionid", "Name");
            ViewData["sources"] = new SelectList(_context.TypeOfSources, "TypeSourceid", "TypeName");
            ViewData["country"] = new SelectList(_context.Countries, "Countryid", "Name");
            ViewData["publisher"] = new SelectList(_context.Publishers, "Publisherid", "Name");
            ViewData["Article"] = new SelectList(_context.Articles, "Articleid", "Articletittle");
            ViewData["Book"] = new SelectList(_context.Books, "Bookid", "Booktittle");
            ViewData["Conference"] = new SelectList(_context.ConferenceProceedings, "Conferenceid", "Conferencetittle");
            IEnumerable<int> keywords = _context.ArticlesKeywords.Where(a => a.Articleid == id).Select(a => a.KeyWord.KeyWordid).ToList();

            article.KeyWords = keywords.ToArray();

            var Keywords = _context.KeyWords.Where(a => a.Active == true).Select(s => new
            {
                KeyWordid = s.KeyWordid,
                KeyWord = string.Format("{0} - {1}", s.KeyWord1, s.KeyWordEn)
            }).ToList();
            ViewData["key"] = new MultiSelectList(Keywords, "KeyWordid", "KeyWord");


            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["ArticleIssue"] = new SelectList(_context.IssueOfIssues, "IssueOfIssueid", "IssuenumberOfIssue");
            ViewData["Authors"] = new SelectList(_context.Authors, "Authorid", "Name");
            ViewData["institution"] = new SelectList(_context.Institutions, "Institutionid", "Name");
            var Keywords = _context.KeyWords.Where(a => a.Active == true).Select(s => new
            {
                KeyWordid = s.KeyWordid,
                KeyWord = string.Format("{0} - {1}", s.KeyWord1, s.KeyWordEn)
            }).ToList();
            ViewData["key"] = new MultiSelectList(Keywords, "KeyWordid", "KeyWord");

            

            return View();
        }

        [HttpPost]
        public IActionResult save([FromBody] articleauthors data)
        {

            asd = data.articleauthors1;

            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        public IActionResult SaveArticle(string[] ResarchFields, string[] newenResarchFields, string[] newarResarchFields, [Bind("Articleid,Articletittle,ArticletittleEn,ScannedArticlePdf,BriefQuote,BriefQuoteEn,NumberOfCitations,NumberOfReferences,ArticleIssue,Active,Page")] Article article, IFormFile afiles)
        {
            if (ModelState.IsValid)
            {
                if (afiles != null)
                {
                    try
                    {
                        var date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                        date = date.Replace("/", "-").Replace(":", "-");

                        var fileName = date + Path.GetFileName(afiles.FileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
                        string filepath = Path.Combine(path, fileName);
                        //  file.CopyTo(new FileStream(filepath, FileMode.Create));


                        //string path = ""; // From appsettings.json
                        //string filePath = path + $"\\Attachment {i}-" + attachment.FileName;
                        System.IO.StreamWriter files = new System.IO.StreamWriter(filepath);
                        afiles.OpenReadStream().CopyTo(files.BaseStream);
                        files.Flush();
                        files.Close();

                        article.ScannedArticlePdf = fileName;
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
                article.NumberOfCitations = 0;
                article.NumberOfReferences = 0;
                _context.Add(article);
                _context.SaveChanges();



                List<KeyWord> newresfields = new List<KeyWord>();
                List<ArticlesKeyword> autresfield = new List<ArticlesKeyword>();
                for (int i = 0; i < newarResarchFields.Length; i++)
                {
                    var exresfild = _context.KeyWords.FirstOrDefault(rf => rf.KeyWord1.Trim().ToLower() == newarResarchFields[i].Trim().ToLower());
                    if (exresfild == null)
                    {

                        newresfields.Add(new KeyWord { KeyWord1 = newarResarchFields[i], KeyWordEn = newenResarchFields[i], Active = true });


                    }
                    else
                    {
                        autresfield.Add(new ArticlesKeyword() { Articleid = article.Articleid,KeyWordid = exresfild.KeyWordid });
                    }
                }

                _context.KeyWords.AddRange(newresfields);
                _context.SaveChanges();

                var allresearchfields = _context.KeyWords;
                List<string> ResarchFieldslst = ResarchFields.ToList();
                for (int i = 0; i < newarResarchFields.Length; i++)
                {
                    string resfieldid = allresearchfields.FirstOrDefault(rf => rf.KeyWord1.Trim() == newarResarchFields[i].Trim()).KeyWordid.ToString();
                    ResarchFieldslst.Add(resfieldid);
                }

                foreach (var item in ResarchFieldslst)
                {
                    autresfield.Add(new ArticlesKeyword() { Articleid = article.Articleid, KeyWordid = int.Parse(item) });
                }

                autresfield = autresfield.GroupBy(i => i.KeyWordid).Select(i => i.FirstOrDefault()).ToList();




                _context.ArticlesKeywords.AddRange(autresfield);

                _context.SaveChanges();

                foreach (var item in asd)
                {
                    ArticleAuthore articleAuthore = new ArticleAuthore();
                    articleAuthore.Articleid = article.Articleid;
                    articleAuthore.Authorid = item.author;
                    articleAuthore.MainAuthor = item.main;
                    _context.Add(articleAuthore);
                    _context.SaveChanges();
                }
                return Json(new
                {
                    success = true,
                    id=article.Articleid
                });


            }
            else
            {
                return Json(new
                {
                    success = false
                });
            }

        }


        public JsonResult getmagazine(int Institutionid)
        {
            List<Magazine> data = new List<Magazine>();

            data = _context.Magazines.Where(a => a.Institutionid.Equals(Institutionid)).OrderBy(a => a.Name).ToList();

            return Json(data);
        }

        public JsonResult getissue(int Magazineid)
        {
            List<MagazineIssue> data = new List<MagazineIssue>();

            data = _context.MagazineIssues.Where(a => a.Magazineid.Equals(Magazineid)).OrderBy(a => a.Issuenumber).ToList();

            return Json(data);
        }

        public JsonResult getissueversion(int magazineissueid)
        {
            List<IssueOfIssue> data = new List<IssueOfIssue>();

            data = _context.IssueOfIssues.Where(a => a.MagazineIssueId.Equals(magazineissueid)).OrderBy(a => a.IssuenumberOfIssue).ToList();

            return Json(data);
        }


       
      public IActionResult SaveResourceNewArticle(int articleid,[Bind("Articletittle,ArticletittleEn,ScannedArticlePdf,BriefQuote,BriefQuoteEn,NumberOfCitations,NumberOfReferences,ArticleIssue,Active,Page")] Article article)
        {
          
           
            if (TryValidateModel(article))
            {
                var existarticle = _context.Articles.FirstOrDefault(w=>w.Articletittle==article.Articletittle.Trim());
                if(existarticle!=null)
                {
                    return RedirectToAction("SaveResourceExistArticle", new { articleid= articleid, articlerefid= existarticle.Articleid});
                }
                article.NumberOfReferences = 0;
                article.NumberOfCitations = 1;
                _context.Add(article);
                _context.SaveChanges();

                foreach (var item in asd)
                {
                    ArticleAuthore articleAuthore = new ArticleAuthore();
                    articleAuthore.Articleid = article.Articleid;
                    articleAuthore.Authorid = item.author;
                    articleAuthore.MainAuthor = item.main;
                    _context.Add(articleAuthore);
                    _context.SaveChanges();
                }

                ArticleReference articleReference = new ArticleReference();
                articleReference.Articleid = articleid;
                articleReference.Articlerefid = article.Articleid;
               
                articleReference.TypeSourceid = 1;

                _context.Add(articleReference);
                _context.SaveChanges();

                Article article1 = _context.Articles.Find(articleid);
                article1.NumberOfReferences = ++article1.NumberOfReferences;
                _context.Update(article1);
                _context.SaveChanges();

                return Json(new
                {
                    success = true
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    responseText = "there is an invalid input!.",
                    type = "error"

                });

            }


        }

        public IActionResult SaveResourceExistArticle(int articleid,int articlerefid)
        {
            var existarticleReference = _context.ArticleReferences.FirstOrDefault(a=>a.Articleid==articleid&&a.Articlerefid==articlerefid);
            if(existarticleReference!=null)
            {
                return Json(new
                {
                    success = false,
                    responseText = "هذا المرجع موجود من قبل",
                    type = "warning"

                });
            }
            ArticleReference articleReference = new ArticleReference();
            articleReference.Articleid = articleid;
            articleReference.Articlerefid = articlerefid;
           
            articleReference.TypeSourceid = 1;

            _context.Add(articleReference);
            _context.SaveChanges();

            Article article1 = _context.Articles.Find(articleid);
            article1.NumberOfReferences = ++article1.NumberOfReferences;
            _context.Update(article1);
            _context.SaveChanges();

            Article articleref = _context.Articles.Find(articlerefid);
            articleref.NumberOfCitations = ++articleref.NumberOfCitations;
            _context.Update(articleref);
            _context.SaveChanges();

            return Json(new
            {
                success = true
            });



        }


        public IActionResult SaveResourceNewBook(int articleid, [Bind("Bookid,Booktittle,Year,Country,Publisherid,Active,Page")] Book book)
        {


            if (TryValidateModel(book))
            {
                var existbook = _context.Books.FirstOrDefault(w => w.Booktittle == book.Booktittle.Trim());
                if (existbook != null)
                {
                 
                    return RedirectToAction("SaveResourceExistBook", new { articleid = articleid, bookrefid = existbook.Bookid });
                }

                _context.Add(book);
                _context.SaveChanges();


                foreach (var item in asd)
                {
                    BookAuthore bookAuthore = new BookAuthore();
                    bookAuthore.Bookid = book.Bookid;
                    bookAuthore.Authorid = item.author;
                    bookAuthore.MainAuthor= item.main;
                    _context.Add(bookAuthore);
                    _context.SaveChanges();
                }


                ArticleReference articleReference = new ArticleReference();
                articleReference.Articleid = articleid;
                articleReference.Bookid=book.Bookid;
             
                articleReference.TypeSourceid = 2;

                _context.Add(articleReference);
                _context.SaveChanges();

                Article article1 = _context.Articles.Find(articleid);
                article1.NumberOfReferences = ++article1.NumberOfReferences;
                _context.Update(article1);
                _context.SaveChanges();

                return Json(new
                {
                    success = true
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    responseText = "there is an invalid input!.",
                    type = "error"

                });

            }


        }

        public IActionResult SaveResourceExistBook(int articleid, int bookrefid)
        {
            var existarticleReference = _context.ArticleReferences.FirstOrDefault(a => a.Articleid == articleid && a.Bookid == bookrefid);
            if (existarticleReference != null)
            {
                return Json(new
                {
                    success = false,
                    responseText = "هذا المرجع موجود من قبل",
                    type = "warning"

                });
            }

            ArticleReference articleReference = new ArticleReference();
            articleReference.Articleid = articleid;
            articleReference.Bookid = bookrefid;
          
            articleReference.TypeSourceid = 2;

            _context.Add(articleReference);
            _context.SaveChanges();

            Article article1 = _context.Articles.Find(articleid);
            article1.NumberOfReferences = ++article1.NumberOfReferences;
            _context.Update(article1);
            _context.SaveChanges();

            return Json(new
            {
                success = true
            });



        }

        public IActionResult SaveResourceNewConference(int articleid, [Bind("Conferenceid,Conferencetittle,Year,ConferencePublicationName,Country,Publisherid,Active,Page")] ConferenceProceeding conferenceProceeding)
        {


            if (TryValidateModel(conferenceProceeding))
            {
                var existconference= _context.ConferenceProceedings.FirstOrDefault(w => w.Conferencetittle == conferenceProceeding.Conferencetittle.Trim());
                if (existconference != null)
                {
                
                    return RedirectToAction("SaveResourceExistConference", new { articleid = articleid, conferencerefid = existconference.Conferenceid });
                }

                _context.Add(conferenceProceeding);
                _context.SaveChanges();


                foreach (var item in asd)
                {
                    ConferenceProceedingsAuthor conferenceProceedingsAuthor = new ConferenceProceedingsAuthor();
                    conferenceProceedingsAuthor.Conferenceid = conferenceProceeding.Conferenceid;
                    conferenceProceedingsAuthor.Authorid= item.author;
                    conferenceProceedingsAuthor.MainAuthor= item.main;
                
                    _context.Add(conferenceProceedingsAuthor);
                    _context.SaveChanges();
                }


                ArticleReference articleReference = new ArticleReference();
                articleReference.Articleid = articleid;
                articleReference.Conferenceid = conferenceProceeding.Conferenceid;
            
                articleReference.TypeSourceid = 3;

                _context.Add(articleReference);
                _context.SaveChanges();

                Article article1 = _context.Articles.Find(articleid);
                article1.NumberOfReferences = ++article1.NumberOfReferences;
                _context.Update(article1);
                _context.SaveChanges();

                return Json(new
                {
                    success = true
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    responseText = "there is an invalid input!.",
                    type = "error"

                });

            }


        }

        public IActionResult SaveResourceExistConference(int articleid, int conferencerefid)
        {
            var existarticleReference = _context.ArticleReferences.FirstOrDefault(a => a.Articleid == articleid && a.Conferenceid == conferencerefid);
            if (existarticleReference != null)
            {
                return Json(new
                {
                    success = false,
                    responseText = "هذا المرجع موجود من قبل",
                    type = "warning"

                });
            }
            ArticleReference articleReference = new ArticleReference();
            articleReference.Articleid = articleid;
            articleReference.Conferenceid= conferencerefid;
          
            articleReference.TypeSourceid = 3;

            _context.Add(articleReference);
            _context.SaveChanges();

            Article article1 = _context.Articles.Find(articleid);
            article1.NumberOfReferences = ++article1.NumberOfReferences;
            _context.Update(article1);
            _context.SaveChanges();

            return Json(new
            {
                success = true
            });



        }
        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Articleid,Articletittle,ArticletittleEn,ScannedArticlePdf,BriefQuote,BriefQuoteEn,NumberOfCitations,NumberOfReferences,ArticleIssue,Active")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleIssue"] = new SelectList(_context.IssueOfIssues, "IssueOfIssueid", "IssuenumberOfIssue", article.ArticleIssue);
            return View(article);
        }

        // GET: Articles/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article =  _context.Articles.Include(p => p.ArticleAuthores).Include(p => p.ArticlesKeywords).Include(p => p.ArticleIssueNavigation).ThenInclude(p => p.MagazineIssue).ThenInclude(p => p.Magazine).Where(a => a.Articleid == id).Single();
            
            if (article == null)
            {
                return NotFound();
            }

            ViewData["Authors"] = new SelectList(_context.Authors, "Authorid", "Name");
            ViewData["institution"] = new SelectList(_context.Institutions, "Institutionid", "Name");
            IEnumerable<int> keywords =  _context.ArticlesKeywords.Where(a => a.Articleid == id).Select(a => a.KeyWord.KeyWordid).ToList();
       
            article.KeyWords = keywords.ToArray();

               var Keywords = _context.KeyWords.Where(a => a.Active == true).Select(s => new
            {
                KeyWordid = s.KeyWordid,
                KeyWord = string.Format("{0} - {1}", s.KeyWord1, s.KeyWordEn)
            }).ToList();
            ViewData["key"] = new MultiSelectList(Keywords, "KeyWordid", "KeyWord");

            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     
        public async Task<IActionResult> Edit(int id, string[] ResarchFields, string[] newenResarchFields, string[] newarResarchFields, [Bind("Articleid,Articletittle,ArticletittleEn,ScannedArticlePdf,BriefQuote,BriefQuoteEn,NumberOfCitations,NumberOfReferences,ArticleIssue,Active,Page")] Article article, IFormFile afiles)
        {
            if (id != article.Articleid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (afiles != null)
                    {
                        try
                        {
                            var date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                            date = date.Replace("/", "-").Replace(":", "-");

                            var fileName = date + Path.GetFileName(afiles.FileName);
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
                            string filepath = Path.Combine(path, fileName);
                            //  file.CopyTo(new FileStream(filepath, FileMode.Create));


                            //string path = ""; // From appsettings.json
                            //string filePath = path + $"\\Attachment {i}-" + attachment.FileName;
                            System.IO.StreamWriter files = new System.IO.StreamWriter(filepath);
                            afiles.OpenReadStream().CopyTo(files.BaseStream);
                            files.Flush();
                            files.Close();
                            if (article.ScannedArticlePdf != null)
                            {
                                System.IO.File.Delete(Path.Combine(path, article.ScannedArticlePdf));
                            }
                            article.ScannedArticlePdf = fileName;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    _context.Update(article);
                    await _context.SaveChangesAsync();

                    var articlekeywords = _context.ArticlesKeywords.Where(a => a.Articleid == article.Articleid);
                    _context.ArticlesKeywords.RemoveRange(articlekeywords);
                    _context.SaveChanges();



                    List<KeyWord> newresfields = new List<KeyWord>();
                    List<ArticlesKeyword> autresfield = new List<ArticlesKeyword>();
                    for (int i = 0; i < newarResarchFields.Length; i++)
                    {
                        var exresfild = _context.KeyWords.FirstOrDefault(rf => rf.KeyWord1.Trim().ToLower() == newarResarchFields[i].Trim().ToLower());
                        if (exresfild == null)
                        {

                            newresfields.Add(new KeyWord { KeyWord1 = newarResarchFields[i], KeyWordEn = newenResarchFields[i], Active = true });


                        }
                        else
                        {
                            autresfield.Add(new ArticlesKeyword() { Articleid = article.Articleid, KeyWordid = exresfild.KeyWordid });
                        }
                    }

                    _context.KeyWords.AddRange(newresfields);
                    _context.SaveChanges();

                    var allresearchfields = _context.KeyWords;
                    List<string> ResarchFieldslst = ResarchFields.ToList();
                    for (int i = 0; i < newarResarchFields.Length; i++)
                    {
                        string resfieldid = allresearchfields.FirstOrDefault(rf => rf.KeyWord1.Trim() == newarResarchFields[i].Trim()).KeyWordid.ToString();
                        ResarchFieldslst.Add(resfieldid);
                    }

                    foreach (var item in ResarchFieldslst)
                    {
                        autresfield.Add(new ArticlesKeyword() { Articleid = article.Articleid, KeyWordid = int.Parse(item) });
                    }

                    autresfield = autresfield.GroupBy(i => i.KeyWordid).Select(i => i.FirstOrDefault()).ToList();




                    _context.ArticlesKeywords.AddRange(autresfield);

                    _context.SaveChanges();



                    var articleauthor = _context.ArticleAuthores.Where(a => a.Articleid == article.Articleid);
                    _context.ArticleAuthores.RemoveRange(articleauthor);
                    _context.SaveChanges();
                    foreach (var item in asd)
                    {
                        ArticleAuthore articleAuthore = new ArticleAuthore();
                        articleAuthore.Articleid = article.Articleid;
                        articleAuthore.Authorid = item.author;
                        articleAuthore.MainAuthor = item.main;
                        _context.Add(articleAuthore);
                        _context.SaveChanges();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Articleid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new
                {
                    success = true,
                    id = article.Articleid
                });
            }
            ViewData["ArticleIssue"] = new SelectList(_context.IssueOfIssues, "IssueOfIssueid", "IssuenumberOfIssue", article.ArticleIssue);
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.ArticleIssueNavigation)
                .FirstOrDefaultAsync(m => m.Articleid == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Articleid == id);
        }
    }
}
