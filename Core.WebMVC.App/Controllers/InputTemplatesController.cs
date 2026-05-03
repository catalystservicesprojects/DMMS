using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMMS.WebMVC.App.Data;
using DMMS.WebMVC.App.Models;

namespace DMMS.WebMVC.App.Controllers
{
    public class InputTemplatesController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public InputTemplatesController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: InputTemplates
        public async Task<IActionResult> Index()
        {
            return View(await _context.InputTemplates.ToListAsync());
        }

        // GET: InputTemplates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputTemplate = await _context.InputTemplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inputTemplate == null)
            {
                return NotFound();
            }

            return View(inputTemplate);
        }

        // GET: InputTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InputTemplates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] InputTemplate inputTemplate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inputTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inputTemplate);
        }

        // GET: InputTemplates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputTemplate = await _context.InputTemplates.FindAsync(id);
            if (inputTemplate == null)
            {
                return NotFound();
            }
            return View(inputTemplate);
        }

        // POST: InputTemplates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] InputTemplate inputTemplate)
        {
            if (id != inputTemplate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inputTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InputTemplateExists(inputTemplate.Id))
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
            return View(inputTemplate);
        }

        // GET: InputTemplates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputTemplate = await _context.InputTemplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inputTemplate == null)
            {
                return NotFound();
            }

            return View(inputTemplate);
        }

        // POST: InputTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inputTemplate = await _context.InputTemplates.FindAsync(id);
            if (inputTemplate != null)
            {
                _context.InputTemplates.Remove(inputTemplate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InputTemplateExists(int id)
        {
            return _context.InputTemplates.Any(e => e.Id == id);
        }
    }
}
