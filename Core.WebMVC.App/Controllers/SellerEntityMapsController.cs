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
    public class SellerEntityMapsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public SellerEntityMapsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: SellerEntityMaps
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.SellerEntityMap.Include(s => s.SellerEntity).Include(s => s.Website);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: SellerEntityMaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerEntityMap = await _context.SellerEntityMap
                .Include(s => s.SellerEntity)
                .Include(s => s.Website)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellerEntityMap == null)
            {
                return NotFound();
            }

            return View(sellerEntityMap);
        }

        // GET: SellerEntityMaps/Create
        public IActionResult Create()
        {
            ViewData["SellerEntityId"] = new SelectList(_context.SellerEntity, "Id", "BackgroundColor");
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "BackLinkColor");
            return View();
        }

        // POST: SellerEntityMaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WebsiteId,SellerEntityId,CompanyId,UserId,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] SellerEntityMap sellerEntityMap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sellerEntityMap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SellerEntityId"] = new SelectList(_context.SellerEntity, "Id", "BackgroundColor", sellerEntityMap.SellerEntityId);
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "BackLinkColor", sellerEntityMap.WebsiteId);
            return View(sellerEntityMap);
        }

        // GET: SellerEntityMaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerEntityMap = await _context.SellerEntityMap.FindAsync(id);
            if (sellerEntityMap == null)
            {
                return NotFound();
            }
            ViewData["SellerEntityId"] = new SelectList(_context.SellerEntity, "Id", "BackgroundColor", sellerEntityMap.SellerEntityId);
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "BackLinkColor", sellerEntityMap.WebsiteId);
            return View(sellerEntityMap);
        }

        // POST: SellerEntityMaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WebsiteId,SellerEntityId,CompanyId,UserId,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] SellerEntityMap sellerEntityMap)
        {
            if (id != sellerEntityMap.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sellerEntityMap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerEntityMapExists(sellerEntityMap.Id))
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
            ViewData["SellerEntityId"] = new SelectList(_context.SellerEntity, "Id", "BackgroundColor", sellerEntityMap.SellerEntityId);
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "BackLinkColor", sellerEntityMap.WebsiteId);
            return View(sellerEntityMap);
        }

        // GET: SellerEntityMaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerEntityMap = await _context.SellerEntityMap
                .Include(s => s.SellerEntity)
                .Include(s => s.Website)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellerEntityMap == null)
            {
                return NotFound();
            }

            return View(sellerEntityMap);
        }

        // POST: SellerEntityMaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sellerEntityMap = await _context.SellerEntityMap.FindAsync(id);
            if (sellerEntityMap != null)
            {
                _context.SellerEntityMap.Remove(sellerEntityMap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerEntityMapExists(int id)
        {
            return _context.SellerEntityMap.Any(e => e.Id == id);
        }
    }
}
