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
    public class ContentSectionsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentSectionsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContentSections.ToListAsync());
        }

        // GET: ContentSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentSection = await _context.ContentSections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentSection == null)
            {
                return NotFound();
            }

            return View(contentSection);
        }

        // GET: ContentSections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContentSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentSection contentSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contentSection);
        }

        // GET: ContentSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentSection = await _context.ContentSections.FindAsync(id);
            if (contentSection == null)
            {
                return NotFound();
            }
            return View(contentSection);
        }

        // POST: ContentSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentSection contentSection)
        {
            if (id != contentSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentSectionExists(contentSection.Id))
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
            return View(contentSection);
        }

        // GET: ContentSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentSection = await _context.ContentSections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentSection == null)
            {
                return NotFound();
            }

            return View(contentSection);
        }

        // POST: ContentSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentSection = await _context.ContentSections.FindAsync(id);
            if (contentSection != null)
            {
                _context.ContentSections.Remove(contentSection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentSectionExists(int id)
        {
            return _context.ContentSections.Any(e => e.Id == id);
        }
    }
}
