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
    public class ContentSocialsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentSocialsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentSocials
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.ContentSocial.Include(c => c.Content);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: ContentSocials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentSocial = await _context.ContentSocial
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentSocial == null)
            {
                return NotFound();
            }

            return View(contentSocial);
        }

        // GET: ContentSocials/Create
        public IActionResult Create()
        {
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name");
            return View();
        }

        // POST: ContentSocials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentSocial contentSocial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentSocial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentSocial.ContentId);
            return View(contentSocial);
        }

        // GET: ContentSocials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentSocial = await _context.ContentSocial.FindAsync(id);
            if (contentSocial == null)
            {
                return NotFound();
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentSocial.ContentId);
            return View(contentSocial);
        }

        // POST: ContentSocials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentSocial contentSocial)
        {
            if (id != contentSocial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentSocial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentSocialExists(contentSocial.Id))
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
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentSocial.ContentId);
            return View(contentSocial);
        }

        // GET: ContentSocials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentSocial = await _context.ContentSocial
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentSocial == null)
            {
                return NotFound();
            }

            return View(contentSocial);
        }

        // POST: ContentSocials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentSocial = await _context.ContentSocial.FindAsync(id);
            if (contentSocial != null)
            {
                _context.ContentSocial.Remove(contentSocial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentSocialExists(int id)
        {
            return _context.ContentSocial.Any(e => e.Id == id);
        }
    }
}
