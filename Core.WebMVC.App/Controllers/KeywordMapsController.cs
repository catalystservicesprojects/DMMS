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
    public class KeywordMapsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public KeywordMapsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: KeywordMaps
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.KeywordMap.Include(k => k.Category).Include(k => k.Keyword).Include(k => k.Niche).Include(k => k.Tag);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: KeywordMaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordMap = await _context.KeywordMap
                .Include(k => k.Category)
                .Include(k => k.Keyword)
                .Include(k => k.Niche)
                .Include(k => k.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keywordMap == null)
            {
                return NotFound();
            }

            return View(keywordMap);
        }

        // GET: KeywordMaps/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name");
            ViewData["NicheId"] = new SelectList(_context.Niches, "Id", "Id");
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id");
            return View();
        }

        // POST: KeywordMaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeywordId,NicheId,TagId,CategoryId,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] KeywordMap keywordMap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keywordMap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", keywordMap.CategoryId);
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name", keywordMap.KeywordId);
            ViewData["NicheId"] = new SelectList(_context.Niches, "Id", "Id", keywordMap.NicheId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", keywordMap.TagId);
            return View(keywordMap);
        }

        // GET: KeywordMaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordMap = await _context.KeywordMap.FindAsync(id);
            if (keywordMap == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", keywordMap.CategoryId);
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name", keywordMap.KeywordId);
            ViewData["NicheId"] = new SelectList(_context.Niches, "Id", "Id", keywordMap.NicheId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", keywordMap.TagId);
            return View(keywordMap);
        }

        // POST: KeywordMaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KeywordId,NicheId,TagId,CategoryId,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] KeywordMap keywordMap)
        {
            if (id != keywordMap.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keywordMap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordMapExists(keywordMap.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", keywordMap.CategoryId);
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name", keywordMap.KeywordId);
            ViewData["NicheId"] = new SelectList(_context.Niches, "Id", "Id", keywordMap.NicheId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", keywordMap.TagId);
            return View(keywordMap);
        }

        // GET: KeywordMaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordMap = await _context.KeywordMap
                .Include(k => k.Category)
                .Include(k => k.Keyword)
                .Include(k => k.Niche)
                .Include(k => k.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keywordMap == null)
            {
                return NotFound();
            }

            return View(keywordMap);
        }

        // POST: KeywordMaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keywordMap = await _context.KeywordMap.FindAsync(id);
            if (keywordMap != null)
            {
                _context.KeywordMap.Remove(keywordMap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeywordMapExists(int id)
        {
            return _context.KeywordMap.Any(e => e.Id == id);
        }
    }
}
