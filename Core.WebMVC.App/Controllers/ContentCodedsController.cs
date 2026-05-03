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
    public class ContentCodedsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentCodedsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentCodeds
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.ContentCodeds.Include(c => c.Content);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: ContentCodeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentCoded = await _context.ContentCodeds
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentCoded == null)
            {
                return NotFound();
            }

            return View(contentCoded);
        }

        // GET: ContentCodeds/Create
        public IActionResult Create()
        {
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name");
            return View();
        }

        // POST: ContentCodeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentCoded contentCoded)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentCoded);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentCoded.ContentId);
            return View(contentCoded);
        }

        // GET: ContentCodeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentCoded = await _context.ContentCodeds.FindAsync(id);
            if (contentCoded == null)
            {
                return NotFound();
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentCoded.ContentId);
            return View(contentCoded);
        }

        // POST: ContentCodeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentCoded contentCoded)
        {
            if (id != contentCoded.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentCoded);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentCodedExists(contentCoded.Id))
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
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentCoded.ContentId);
            return View(contentCoded);
        }

        // GET: ContentCodeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentCoded = await _context.ContentCodeds
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentCoded == null)
            {
                return NotFound();
            }

            return View(contentCoded);
        }

        // POST: ContentCodeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentCoded = await _context.ContentCodeds.FindAsync(id);
            if (contentCoded != null)
            {
                _context.ContentCodeds.Remove(contentCoded);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentCodedExists(int id)
        {
            return _context.ContentCodeds.Any(e => e.Id == id);
        }
    }
}
