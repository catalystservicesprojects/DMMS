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
    public class ContentLinksMediasController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentLinksMediasController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentLinksMedias
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.ContentLinksMedia.Include(c => c.Content);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: ContentLinksMedias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksMedia = await _context.ContentLinksMedia
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentLinksMedia == null)
            {
                return NotFound();
            }

            return View(contentLinksMedia);
        }

        // GET: ContentLinksMedias/Create
        public IActionResult Create()
        {
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name");
            return View();
        }

        // POST: ContentLinksMedias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,MediaType,MediaTypeId,Price,Link,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentLinksMedia contentLinksMedia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentLinksMedia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksMedia.ContentId);
            return View(contentLinksMedia);
        }

        // GET: ContentLinksMedias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksMedia = await _context.ContentLinksMedia.FindAsync(id);
            if (contentLinksMedia == null)
            {
                return NotFound();
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksMedia.ContentId);
            return View(contentLinksMedia);
        }

        // POST: ContentLinksMedias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,MediaType,MediaTypeId,Price,Link,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentLinksMedia contentLinksMedia)
        {
            if (id != contentLinksMedia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentLinksMedia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentLinksMediaExists(contentLinksMedia.Id))
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
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksMedia.ContentId);
            return View(contentLinksMedia);
        }

        // GET: ContentLinksMedias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksMedia = await _context.ContentLinksMedia
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentLinksMedia == null)
            {
                return NotFound();
            }

            return View(contentLinksMedia);
        }

        // POST: ContentLinksMedias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentLinksMedia = await _context.ContentLinksMedia.FindAsync(id);
            if (contentLinksMedia != null)
            {
                _context.ContentLinksMedia.Remove(contentLinksMedia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentLinksMediaExists(int id)
        {
            return _context.ContentLinksMedia.Any(e => e.Id == id);
        }
    }
}
