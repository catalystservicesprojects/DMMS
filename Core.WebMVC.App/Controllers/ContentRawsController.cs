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
    public class ContentRawsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentRawsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentRaws
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.ContentRaw.Include(c => c.Content);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: ContentRaws/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentRaw = await _context.ContentRaw
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentRaw == null)
            {
                return NotFound();
            }

            return View(contentRaw);
        }

        // GET: ContentRaws/Create
        public IActionResult Create()
        {
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name");
            return View();
        }

        // POST: ContentRaws/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentRaw contentRaw)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentRaw);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentRaw.ContentId);
            return View(contentRaw);
        }

        // GET: ContentRaws/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentRaw = await _context.ContentRaw.FindAsync(id);
            if (contentRaw == null)
            {
                return NotFound();
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentRaw.ContentId);
            return View(contentRaw);
        }

        // POST: ContentRaws/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentRaw contentRaw)
        {
            if (id != contentRaw.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentRaw);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentRawExists(contentRaw.Id))
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
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentRaw.ContentId);
            return View(contentRaw);
        }

        // GET: ContentRaws/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentRaw = await _context.ContentRaw
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentRaw == null)
            {
                return NotFound();
            }

            return View(contentRaw);
        }

        // POST: ContentRaws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentRaw = await _context.ContentRaw.FindAsync(id);
            if (contentRaw != null)
            {
                _context.ContentRaw.Remove(contentRaw);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentRawExists(int id)
        {
            return _context.ContentRaw.Any(e => e.Id == id);
        }
    }
}
