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
    public class NichesController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public NichesController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: Niches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Niches.ToListAsync());
        }

        // GET: Niches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var niche = await _context.Niches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (niche == null)
            {
                return NotFound();
            }

            return View(niche);
        }

        // GET: Niches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Niches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Alias,Value,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] Niche niche)
        {
            if (ModelState.IsValid)
            {
                _context.Add(niche);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(niche);
        }

        // GET: Niches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var niche = await _context.Niches.FindAsync(id);
            if (niche == null)
            {
                return NotFound();
            }
            return View(niche);
        }

        // POST: Niches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,Value,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] Niche niche)
        {
            if (id != niche.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(niche);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NicheExists(niche.Id))
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
            return View(niche);
        }

        // GET: Niches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var niche = await _context.Niches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (niche == null)
            {
                return NotFound();
            }

            return View(niche);
        }

        // POST: Niches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var niche = await _context.Niches.FindAsync(id);
            if (niche != null)
            {
                _context.Niches.Remove(niche);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NicheExists(int id)
        {
            return _context.Niches.Any(e => e.Id == id);
        }
    }
}
