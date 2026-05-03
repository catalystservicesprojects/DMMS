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
    public class ContentLinksAffiliatesController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public ContentLinksAffiliatesController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: ContentLinksAffiliates
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.ContentLinksAffiliate.Include(c => c.Content).Include(c => c.SellerEntity);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: ContentLinksAffiliates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksAffiliate = await _context.ContentLinksAffiliate
                .Include(c => c.Content)
                .Include(c => c.SellerEntity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentLinksAffiliate == null)
            {
                return NotFound();
            }

            return View(contentLinksAffiliate);
        }

        // GET: ContentLinksAffiliates/Create
        public IActionResult Create()
        {
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name");
            ViewData["SellerEntityId"] = new SelectList(_context.Set<SellerEntity>(), "Id", "BackgroundColor");
            return View();
        }

        // POST: ContentLinksAffiliates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,SellerEntityId,Price,Link,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentLinksAffiliate contentLinksAffiliate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentLinksAffiliate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksAffiliate.ContentId);
            ViewData["SellerEntityId"] = new SelectList(_context.Set<SellerEntity>(), "Id", "BackgroundColor", contentLinksAffiliate.SellerEntityId);
            return View(contentLinksAffiliate);
        }

        // GET: ContentLinksAffiliates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksAffiliate = await _context.ContentLinksAffiliate.FindAsync(id);
            if (contentLinksAffiliate == null)
            {
                return NotFound();
            }
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksAffiliate.ContentId);
            ViewData["SellerEntityId"] = new SelectList(_context.Set<SellerEntity>(), "Id", "BackgroundColor", contentLinksAffiliate.SellerEntityId);
            return View(contentLinksAffiliate);
        }

        // POST: ContentLinksAffiliates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,SellerEntityId,Price,Link,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] ContentLinksAffiliate contentLinksAffiliate)
        {
            if (id != contentLinksAffiliate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentLinksAffiliate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentLinksAffiliateExists(contentLinksAffiliate.Id))
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
            ViewData["ContentId"] = new SelectList(_context.Content, "Id", "Name", contentLinksAffiliate.ContentId);
            ViewData["SellerEntityId"] = new SelectList(_context.Set<SellerEntity>(), "Id", "BackgroundColor", contentLinksAffiliate.SellerEntityId);
            return View(contentLinksAffiliate);
        }

        // GET: ContentLinksAffiliates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentLinksAffiliate = await _context.ContentLinksAffiliate
                .Include(c => c.Content)
                .Include(c => c.SellerEntity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentLinksAffiliate == null)
            {
                return NotFound();
            }

            return View(contentLinksAffiliate);
        }

        // POST: ContentLinksAffiliates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentLinksAffiliate = await _context.ContentLinksAffiliate.FindAsync(id);
            if (contentLinksAffiliate != null)
            {
                _context.ContentLinksAffiliate.Remove(contentLinksAffiliate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentLinksAffiliateExists(int id)
        {
            return _context.ContentLinksAffiliate.Any(e => e.Id == id);
        }
    }
}
