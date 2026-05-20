using DMMS.Models;
using DMMS.Models.ViewModels.Contents;
using DMMS.WebMVC.App.Data;
using DMMS.WebMVC.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMMS.WebMVC.App.Controllers
{
    public class ContentsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: Contents
        public async Task<IActionResult> Index(string websiteIdentity)
        {
            Website? website = _context.Website.Where(a => a.Identity == websiteIdentity).FirstOrDefault();
            ViewBag.Website = websiteIdentity;
            if (website != null)
            {
                ViewBag.WebsiteId = website.Id;
                HttpContext.Session.SetString("Name", "Ninad Gawankar");
                HttpContext.Session.SetInt32("CompanyId", website.CompanyId);
                HttpContext.Session.SetInt32("UserId", website.UserId);

                //ContentLinksMedia contentLinksMedia;

                //return View(await _context.Content.Where(x => x.Status == 1 && x.WebsiteId == website.Id).OrderByDescending(x => x.Id).ToListAsync());

                var data = await _context.ContentMetas
                            .Where(x => x.Status == 1 && x.WebsiteId == website.Id)
                            .OrderByDescending(x => x.Id)
                            .Select(contentMeta => new ContentListSEO
                            {
                                ContentMeta = contentMeta,
                                ContentLinksMedia = _context.ContentLinksMedia
                                            .Where(m => m.ContentId == contentMeta.ContentId)
                                            .FirstOrDefault()
                            })
                            .ToListAsync();
                return View(data);
            }
            return View(await _context.ContentMetas
                            .OrderByDescending(x => x.Id)
                            .Select(contentMeta => new ContentListSEO
                            {
                                ContentMeta = contentMeta,
                                ContentLinksMedia = _context.ContentLinksMedia
                                            .Where(m => m.ContentId == contentMeta.ContentId)
                                            .FirstOrDefault()
                            })
                            .ToListAsync());
        }

        // GET: Contents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Content
                .FirstOrDefaultAsync(m => m.Id == id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // GET: Contents/Create
        public async Task<IActionResult> Create(string websiteIdentity)
        {
            Website? website = _context.Website.Where(a => a.Identity == websiteIdentity).FirstOrDefault();
            ViewBag.Website = websiteIdentity;
            if (website != null)
            {
                ViewBag.WebsiteId = website.Id;
                HttpContext.Session.SetString("Name", "Ninad Gawankar");
                HttpContext.Session.SetInt32("CompanyId", website.CompanyId);
                HttpContext.Session.SetInt32("UserId", website.UserId);
                return View(await _context.Content.Where(x => x.Status == 1 && x.WebsiteId == website.Id).OrderByDescending(x => x.Id).ToListAsync());
            }
            return View(await _context.Content.Where(x => x.CompanyId == 0).OrderByDescending(x => x.Id).ToListAsync());
        }

        // POST: Contents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContentMeta contentMeta)
        {
            Content content = new Content()
            {
                Name = contentMeta.Title,
                Alias = contentMeta.PrimaryKeyword,
                ShortName = contentMeta.ShortName,
                ShortDescription = contentMeta.Description == null? "" : contentMeta.Description,
                WebsiteId = contentMeta.WebsiteId,
                UserId = contentMeta.UserId,
                CompanyId = contentMeta.CompanyId,
                Guid = contentMeta.Guid,
                Identity = contentMeta.Identity,
                Status = contentMeta.Status,
                Tenant = contentMeta.Tenant,
                CreatedBy = contentMeta.CreatedBy,
                CreationDate = contentMeta.CreationDate,
                ModifiedBy = contentMeta.ModifiedBy,
                ModifiedDate = contentMeta.ModifiedDate,
            };

            _context.Add(content);
            await _context.SaveChangesAsync(); // saves to DB and populates ID property
            contentMeta.Content = await _context.Content.FirstOrDefaultAsync(c => c.Id == content.Id);

            contentMeta.ContentId = content.Id;
            //var raw = Request.Form["WebsiteId"].ToString();
            if (ModelState.IsValid)
            {
                _context.Add(contentMeta);
                await _context.SaveChangesAsync(); // saves to DB and populates ID property

                int ContentId = contentMeta.ContentId; // <-- this now has the new record's ID
                ViewBag.ContentId = ContentId;
                ViewBag.Name = contentMeta.Title;
                ViewBag.Title = contentMeta.Title;
                ViewBag.Link = contentMeta.Link;
                ViewBag.ImageLink = contentMeta.ImageLink;
                ViewBag.Slug = contentMeta.Slug;
                ViewBag.ShortName = contentMeta.ShortName;
                ViewBag.PrimaryKeyword = contentMeta.PrimaryKeyword;
                ViewBag.SecondaryKeywords = contentMeta.SecondaryKeywords;
                ViewBag.TertiaryKeywords = contentMeta.TertiaryKeywords;
                ViewBag.Hook = contentMeta.Hook;
                ViewBag.Description = contentMeta.Description;
                ViewBag.Author = contentMeta.Author;
                ViewBag.Robots = contentMeta.Robots;
                ViewBag.Language = contentMeta.Language;
                ViewBag.Revisit = contentMeta.Revisit;
                return RedirectToAction("PostBuilder", "Posting", new { ContentId, Mode = "Create" });
            }
            return View(contentMeta);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ContentMeta contentMeta)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Validation failed"
                });
            }

            var content = await _context.Content
                .FirstOrDefaultAsync(c => c.Id == contentMeta.ContentId);

            if (content == null)
            {
                return Json(new { success = false, message = "Content not found" });
            }

            // Update Content
            content.Name = contentMeta.Title;
            content.Alias = contentMeta.PrimaryKeyword;
            content.ShortName = contentMeta.ShortName;
            content.ShortDescription = contentMeta.Description ?? "";
            content.ModifiedDate = DateTime.UtcNow;

            // Update ContentMeta
            var existingMeta = await _context.ContentMetas
                .FirstOrDefaultAsync(cm => cm.Id == contentMeta.Id);

            if (existingMeta == null)
            {
                return Json(new { success = false, message = "Meta not found" });
            }

            existingMeta.Title = contentMeta.Title;
            existingMeta.Link = contentMeta.Link;
            existingMeta.ImageLink = contentMeta.ImageLink;
            existingMeta.Slug = contentMeta.Slug;
            existingMeta.PrimaryKeyword = contentMeta.PrimaryKeyword;
            existingMeta.SecondaryKeywords = contentMeta.SecondaryKeywords;
            existingMeta.TertiaryKeywords = contentMeta.TertiaryKeywords;
            existingMeta.ShortName = contentMeta.ShortName;
            existingMeta.Hook = contentMeta.Hook;
            existingMeta.HookSlug = contentMeta.HookSlug;
            existingMeta.Description = contentMeta.Description;
            existingMeta.Author = contentMeta.Author;
            existingMeta.Robots = contentMeta.Robots;
            existingMeta.Language = contentMeta.Language;
            existingMeta.Revisit = contentMeta.Revisit;
            existingMeta.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("PostBuilder", "Posting", new
                {
                    ContentId = existingMeta.ContentId,
                    Mode = "Create"
                })
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRawContent([Bind("ShortName,ShortDescription,Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] Content content)
        {
            if (ModelState.IsValid)
            {
                _context.Add(content);
                await _context.SaveChangesAsync(); // saves to DB and populates ID property

                int ContentId = content.Id; // <-- this now has the new record's ID
                return RedirectToAction("PostBuilder", "Posting", new { ContentId, Mode = "Create" });
            }
            return View(content);
        }

        // GET: Contents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Content.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShortName,ShortDescription,Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(content);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(content.Id))
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
            return View(content);
        }

        // GET: Contents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Content
                .FirstOrDefaultAsync(m => m.Id == id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var content = await _context.Content.FindAsync(id);
            if (content != null)
            {
                _context.Content.Remove(content);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentExists(int id)
        {
            return _context.Content.Any(e => e.Id == id);
        }
    }
}
