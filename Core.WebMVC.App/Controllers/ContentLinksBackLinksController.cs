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
    public class ContentLinksBackLinksController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentLinksBackLinksController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentLinksBackLinks
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.ContentLinksExternalLinks.Include(c => c.Content);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: ContentLinksBackLinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksBackLinks = await _context.ContentLinksExternalLinks
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentLinksBackLinks == null)
            {
                return NotFound();
            }

            return View(contentLinksBackLinks);
        }

        // GET: ContentLinksBackLinks/Create
        public IActionResult Create()
        {
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name");
            return View();
        }

        // POST: ContentLinksBackLinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,WordOrSentense,Link,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentLinksExternalLink contentLinksBackLinks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentLinksBackLinks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksBackLinks.ContentId);
            return View(contentLinksBackLinks);
        }

        // GET: ContentLinksBackLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksBackLinks = await _context.ContentLinksExternalLinks.FindAsync(id);
            if (contentLinksBackLinks == null)
            {
                return NotFound();
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksBackLinks.ContentId);
            return View(contentLinksBackLinks);
        }

        // POST: ContentLinksBackLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,WordOrSentense,Link,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentLinksExternalLink contentLinksBackLinks)
        {
            if (id != contentLinksBackLinks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentLinksBackLinks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentLinksBackLinksExists(contentLinksBackLinks.Id))
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
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksBackLinks.ContentId);
            return View(contentLinksBackLinks);
        }

        // GET: ContentLinksBackLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksBackLinks = await _context.ContentLinksExternalLinks
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentLinksBackLinks == null)
            {
                return NotFound();
            }

            return View(contentLinksBackLinks);
        }

        // POST: ContentLinksBackLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentLinksBackLinks = await _context.ContentLinksExternalLinks.FindAsync(id);
            if (contentLinksBackLinks != null)
            {
                _context.ContentLinksExternalLinks.Remove(contentLinksBackLinks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentLinksBackLinksExists(int id)
        {
            return _context.ContentLinksExternalLinks.Any(e => e.Id == id);
        }
    }
}
