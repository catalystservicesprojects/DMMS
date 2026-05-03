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
    public class KeywordProvidersController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public KeywordProvidersController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: KeywordProviders
        public async Task<IActionResult> Index()
        {
            return View(await _context.KeywordProviders.ToListAsync());
        }

        // GET: KeywordProviders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordProvider = await _context.KeywordProviders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keywordProvider == null)
            {
                return NotFound();
            }

            return View(keywordProvider);
        }

        // GET: KeywordProviders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KeywordProviders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] KeywordProvider keywordProvider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keywordProvider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(keywordProvider);
        }

        // GET: KeywordProviders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordProvider = await _context.KeywordProviders.FindAsync(id);
            if (keywordProvider == null)
            {
                return NotFound();
            }
            return View(keywordProvider);
        }

        // POST: KeywordProviders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] KeywordProvider keywordProvider)
        {
            if (id != keywordProvider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keywordProvider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordProviderExists(keywordProvider.Id))
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
            return View(keywordProvider);
        }

        // GET: KeywordProviders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordProvider = await _context.KeywordProviders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keywordProvider == null)
            {
                return NotFound();
            }

            return View(keywordProvider);
        }

        // POST: KeywordProviders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keywordProvider = await _context.KeywordProviders.FindAsync(id);
            if (keywordProvider != null)
            {
                _context.KeywordProviders.Remove(keywordProvider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeywordProviderExists(int id)
        {
            return _context.KeywordProviders.Any(e => e.Id == id);
        }
    }
}
