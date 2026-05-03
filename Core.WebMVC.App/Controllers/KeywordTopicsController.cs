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
    public class KeywordTopicsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public KeywordTopicsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: KeywordTopics
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.KeywordTopics.Include(k => k.Keyword);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: KeywordTopics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordTopic = await _context.KeywordTopics
                .Include(k => k.Keyword)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keywordTopic == null)
            {
                return NotFound();
            }

            return View(keywordTopic);
        }

        // GET: KeywordTopics/Create
        public IActionResult Create()
        {
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name");
            return View();
        }

        // POST: KeywordTopics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeywordId,Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] KeywordTopic keywordTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keywordTopic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name", keywordTopic.KeywordId);
            return View(keywordTopic);
        }

        // GET: KeywordTopics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordTopic = await _context.KeywordTopics.FindAsync(id);
            if (keywordTopic == null)
            {
                return NotFound();
            }
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name", keywordTopic.KeywordId);
            return View(keywordTopic);
        }

        // POST: KeywordTopics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KeywordId,Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] KeywordTopic keywordTopic)
        {
            if (id != keywordTopic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keywordTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordTopicExists(keywordTopic.Id))
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
            ViewData["KeywordId"] = new SelectList(_context.Keywords, "Id", "Name", keywordTopic.KeywordId);
            return View(keywordTopic);
        }

        // GET: KeywordTopics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordTopic = await _context.KeywordTopics
                .Include(k => k.Keyword)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keywordTopic == null)
            {
                return NotFound();
            }

            return View(keywordTopic);
        }

        // POST: KeywordTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keywordTopic = await _context.KeywordTopics.FindAsync(id);
            if (keywordTopic != null)
            {
                _context.KeywordTopics.Remove(keywordTopic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeywordTopicExists(int id)
        {
            return _context.KeywordTopics.Any(e => e.Id == id);
        }
    }
}
