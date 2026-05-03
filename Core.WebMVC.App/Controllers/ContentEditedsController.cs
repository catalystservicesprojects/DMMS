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
    public class ContentEditedsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentEditedsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentEditeds
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.ContentEditeds.Include(c => c.Content);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: ContentEditeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentEdited = await _context.ContentEditeds
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentEdited == null)
            {
                return NotFound();
            }

            return View(contentEdited);
        }

        // GET: ContentEditeds/Create
        public IActionResult Create()
        {
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name");
            return View();
        }

        // POST: ContentEditeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentEdited contentEdited)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentEdited);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentEdited.ContentId);
            return View(contentEdited);
        }

        // GET: ContentEditeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentEdited = await _context.ContentEditeds.FindAsync(id);
            if (contentEdited == null)
            {
                return NotFound();
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentEdited.ContentId);
            return View(contentEdited);
        }

        // POST: ContentEditeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentEdited contentEdited)
        {
            if (id != contentEdited.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentEdited);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentEditedExists(contentEdited.Id))
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
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentEdited.ContentId);
            return View(contentEdited);
        }

        // GET: ContentEditeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentEdited = await _context.ContentEditeds
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentEdited == null)
            {
                return NotFound();
            }

            return View(contentEdited);
        }

        // POST: ContentEditeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentEdited = await _context.ContentEditeds.FindAsync(id);
            if (contentEdited != null)
            {
                _context.ContentEditeds.Remove(contentEdited);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentEditedExists(int id)
        {
            return _context.ContentEditeds.Any(e => e.Id == id);
        }
    }
}
