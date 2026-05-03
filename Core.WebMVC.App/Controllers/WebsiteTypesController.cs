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
    public class WebsiteTypesController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public WebsiteTypesController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: WebsiteTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebsiteType.ToListAsync());
        }

        // GET: WebsiteTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteType = await _context.WebsiteType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (websiteType == null)
            {
                return NotFound();
            }

            return View(websiteType);
        }

        // GET: WebsiteTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebsiteTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Alias,Value,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] WebsiteType websiteType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(websiteType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(websiteType);
        }

        // GET: WebsiteTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteType = await _context.WebsiteType.FindAsync(id);
            if (websiteType == null)
            {
                return NotFound();
            }
            return View(websiteType);
        }

        // POST: WebsiteTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,Value,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] WebsiteType websiteType)
        {
            if (id != websiteType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(websiteType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebsiteTypeExists(websiteType.Id))
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
            return View(websiteType);
        }

        // GET: WebsiteTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteType = await _context.WebsiteType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (websiteType == null)
            {
                return NotFound();
            }

            return View(websiteType);
        }

        // POST: WebsiteTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var websiteType = await _context.WebsiteType.FindAsync(id);
            if (websiteType != null)
            {
                _context.WebsiteType.Remove(websiteType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebsiteTypeExists(int id)
        {
            return _context.WebsiteType.Any(e => e.Id == id);
        }
    }
}
