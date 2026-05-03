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
    public class ContentLinksReferencesController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentLinksReferencesController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentLinksReferences
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.ContentLinksReference.Include(c => c.Content);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: ContentLinksReferences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksReference = await _context.ContentLinksReference
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentLinksReference == null)
            {
                return NotFound();
            }

            return View(contentLinksReference);
        }

        // GET: ContentLinksReferences/Create
        public IActionResult Create()
        {
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name");
            return View();
        }

        // POST: ContentLinksReferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,ReferenceType,ReferenceTypeId,Price,Link,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentLinksReference contentLinksReference)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentLinksReference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksReference.ContentId);
            return View(contentLinksReference);
        }

        // GET: ContentLinksReferences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksReference = await _context.ContentLinksReference.FindAsync(id);
            if (contentLinksReference == null)
            {
                return NotFound();
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksReference.ContentId);
            return View(contentLinksReference);
        }

        // POST: ContentLinksReferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,ReferenceType,ReferenceTypeId,Price,Link,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentLinksReference contentLinksReference)
        {
            if (id != contentLinksReference.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentLinksReference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentLinksReferenceExists(contentLinksReference.Id))
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
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksReference.ContentId);
            return View(contentLinksReference);
        }

        // GET: ContentLinksReferences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksReference = await _context.ContentLinksReference
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentLinksReference == null)
            {
                return NotFound();
            }

            return View(contentLinksReference);
        }

        // POST: ContentLinksReferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentLinksReference = await _context.ContentLinksReference.FindAsync(id);
            if (contentLinksReference != null)
            {
                _context.ContentLinksReference.Remove(contentLinksReference);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentLinksReferenceExists(int id)
        {
            return _context.ContentLinksReference.Any(e => e.Id == id);
        }
    }
}
