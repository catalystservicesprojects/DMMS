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
    public class WebsiteConfigsController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public WebsiteConfigsController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: WebsiteConfigs
        public async Task<IActionResult> Index()
        {
            var dMMSWebMVCAppContext = _context.WebsiteConfig.Include(w => w.Website);
            return View(await dMMSWebMVCAppContext.ToListAsync());
        }

        // GET: WebsiteConfigs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteConfig = await _context.WebsiteConfig
                .Include(w => w.Website)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (websiteConfig == null)
            {
                return NotFound();
            }

            return View(websiteConfig);
        }

        // GET: WebsiteConfigs/Create
        public IActionResult Create()
        {
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "BackLinkColor");
            return View();
        }

        // POST: WebsiteConfigs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WebsiteId,ConnectionString,CompanyId,UserId,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] WebsiteConfig websiteConfig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(websiteConfig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "BackLinkColor", websiteConfig.WebsiteId);
            return View(websiteConfig);
        }

        // GET: WebsiteConfigs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteConfig = await _context.WebsiteConfig.FindAsync(id);
            if (websiteConfig == null)
            {
                return NotFound();
            }
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "BackLinkColor", websiteConfig.WebsiteId);
            return View(websiteConfig);
        }

        // POST: WebsiteConfigs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WebsiteId,ConnectionString,CompanyId,UserId,Id,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] WebsiteConfig websiteConfig)
        {
            if (id != websiteConfig.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(websiteConfig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebsiteConfigExists(websiteConfig.Id))
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
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "BackLinkColor", websiteConfig.WebsiteId);
            return View(websiteConfig);
        }

        // GET: WebsiteConfigs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteConfig = await _context.WebsiteConfig
                .Include(w => w.Website)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (websiteConfig == null)
            {
                return NotFound();
            }

            return View(websiteConfig);
        }

        // POST: WebsiteConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var websiteConfig = await _context.WebsiteConfig.FindAsync(id);
            if (websiteConfig != null)
            {
                _context.WebsiteConfig.Remove(websiteConfig);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebsiteConfigExists(int id)
        {
            return _context.WebsiteConfig.Any(e => e.Id == id);
        }
    }
}
