using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMMS.Models;
using DMMS.WebMVC.App.Data;

namespace DMMS.WebMVC.App.Controllers
{
    public class ContentLinksSocialsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentLinksSocialsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentLinksSocials
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.ContentLinksSocial.Include(c => c.Content).Include(c => c.SocialMediaType);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: ContentLinksSocials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksSocial = await _context.ContentLinksSocial
                .Include(c => c.Content)
                .Include(c => c.SocialMediaType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentLinksSocial == null)
            {
                return NotFound();
            }

            return View(contentLinksSocial);
        }

        // GET: ContentLinksSocials/Create
        public IActionResult Create()
        {
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name");
            ViewData["SocialMediaTypeId"] = new SelectList(_context.Set<SocialMediaType>(), "Id", "Id");
            return View();
        }

        // POST: ContentLinksSocials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,SocialMediaTypeId,Link,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentLinksSocial contentLinksSocial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentLinksSocial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksSocial.ContentId);
            ViewData["SocialMediaTypeId"] = new SelectList(_context.Set<SocialMediaType>(), "Id", "Id", contentLinksSocial.SocialMediaTypeId);
            return View(contentLinksSocial);
        }

        // GET: ContentLinksSocials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksSocial = await _context.ContentLinksSocial.FindAsync(id);
            if (contentLinksSocial == null)
            {
                return NotFound();
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksSocial.ContentId);
            ViewData["SocialMediaTypeId"] = new SelectList(_context.Set<SocialMediaType>(), "Id", "Id", contentLinksSocial.SocialMediaTypeId);
            return View(contentLinksSocial);
        }

        // POST: ContentLinksSocials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,SocialMediaTypeId,Link,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentLinksSocial contentLinksSocial)
        {
            if (id != contentLinksSocial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentLinksSocial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentLinksSocialExists(contentLinksSocial.Id))
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
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksSocial.ContentId);
            ViewData["SocialMediaTypeId"] = new SelectList(_context.Set<SocialMediaType>(), "Id", "Id", contentLinksSocial.SocialMediaTypeId);
            return View(contentLinksSocial);
        }

        // GET: ContentLinksSocials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksSocial = await _context.ContentLinksSocial
                .Include(c => c.Content)
                .Include(c => c.SocialMediaType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentLinksSocial == null)
            {
                return NotFound();
            }

            return View(contentLinksSocial);
        }

        // POST: ContentLinksSocials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentLinksSocial = await _context.ContentLinksSocial.FindAsync(id);
            if (contentLinksSocial != null)
            {
                _context.ContentLinksSocial.Remove(contentLinksSocial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentLinksSocialExists(int id)
        {
            return _context.ContentLinksSocial.Any(e => e.Id == id);
        }
    }
}
