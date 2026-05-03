using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMMS.Models;
using DMMS.WebMVC.App.Data;
using ContentResult = DMMS.Models.ContentResult;

namespace DMMS.WebMVC.App.Controllers
{
    public class ContentResultsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentResultsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentResults
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.ContentResult.Include(c => c.Content);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: ContentResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentResult = await _context.ContentResult
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentResult == null)
            {
                return NotFound();
            }

            return View(contentResult);
        }

        // GET: ContentResults/Create
        public IActionResult Create()
        {
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name");
            return View();
        }

        // POST: ContentResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentResult contentResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentResult.ContentId);
            return View(contentResult);
        }

        // GET: ContentResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentResult = await _context.ContentResult.FindAsync(id);
            if (contentResult == null)
            {
                return NotFound();
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentResult.ContentId);
            return View(contentResult);
        }

        // POST: ContentResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,Value,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentResult contentResult)
        {
            if (id != contentResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentResultExists(contentResult.Id))
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
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentResult.ContentId);
            return View(contentResult);
        }

        // GET: ContentResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentResult = await _context.ContentResult
                .Include(c => c.Content)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentResult == null)
            {
                return NotFound();
            }

            return View(contentResult);
        }

        // POST: ContentResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentResult = await _context.ContentResult.FindAsync(id);
            if (contentResult != null)
            {
                _context.ContentResult.Remove(contentResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentResultExists(int id)
        {
            return _context.ContentResult.Any(e => e.Id == id);
        }
    }
}
