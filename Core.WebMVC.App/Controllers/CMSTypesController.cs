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
    public class CMSTypesController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public CMSTypesController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: CMSTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CMSTypes.ToListAsync());
        }

        // GET: CMSTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMSType = await _context.CMSTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMSType == null)
            {
                return NotFound();
            }

            return View(cMSType);
        }

        // GET: CMSTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CMSTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] CMSType cMSType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cMSType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cMSType);
        }

        // GET: CMSTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMSType = await _context.CMSTypes.FindAsync(id);
            if (cMSType == null)
            {
                return NotFound();
            }
            return View(cMSType);
        }

        // POST: CMSTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] CMSType cMSType)
        {
            if (id != cMSType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cMSType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CMSTypeExists(cMSType.Id))
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
            return View(cMSType);
        }

        // GET: CMSTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cMSType = await _context.CMSTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cMSType == null)
            {
                return NotFound();
            }

            return View(cMSType);
        }

        // POST: CMSTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cMSType = await _context.CMSTypes.FindAsync(id);
            if (cMSType != null)
            {
                _context.CMSTypes.Remove(cMSType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CMSTypeExists(int id)
        {
            return _context.CMSTypes.Any(e => e.Id == id);
        }
    }
}
