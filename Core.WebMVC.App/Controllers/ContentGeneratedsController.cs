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
    public class ContentGeneratedsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentGeneratedsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentGenerateds
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.ContentGenerated.Include(c => c.Content);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: ContentGenerateds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentGenerated = await _context.ContentGenerated
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentGenerated == null)
            {
                return NotFound();
            }

            return View(contentGenerated);
        }

        // GET: ContentGenerateds/Create
        public IActionResult Create()
        {
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name");
            return View();
        }

        // POST: ContentGenerateds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentGenerated contentGenerated)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentGenerated);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentGenerated.ContentId);
            return View(contentGenerated);
        }

        // GET: ContentGenerateds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentGenerated = await _context.ContentGenerated.FindAsync(id);
            if (contentGenerated == null)
            {
                return NotFound();
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentGenerated.ContentId);
            return View(contentGenerated);
        }

        // POST: ContentGenerateds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentGenerated contentGenerated)
        {
            if (id != contentGenerated.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentGenerated);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentGeneratedExists(contentGenerated.Id))
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
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentGenerated.ContentId);
            return View(contentGenerated);
        }

        // GET: ContentGenerateds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentGenerated = await _context.ContentGenerated
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentGenerated == null)
            {
                return NotFound();
            }

            return View(contentGenerated);
        }

        // POST: ContentGenerateds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentGenerated = await _context.ContentGenerated.FindAsync(id);
            if (contentGenerated != null)
            {
                _context.ContentGenerated.Remove(contentGenerated);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentGeneratedExists(int id)
        {
            return _context.ContentGenerated.Any(e => e.Id == id);
        }
    }
}
