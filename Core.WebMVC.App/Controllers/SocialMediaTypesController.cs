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
    public class SocialMediaTypesController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public SocialMediaTypesController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: SocialMediaTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SocialMediaType.ToListAsync());
        }

        // GET: SocialMediaTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialMediaType = await _context.SocialMediaType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialMediaType == null)
            {
                return NotFound();
            }

            return View(socialMediaType);
        }

        // GET: SocialMediaTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SocialMediaTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Alias,Value,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] SocialMediaType socialMediaType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socialMediaType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socialMediaType);
        }

        // GET: SocialMediaTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialMediaType = await _context.SocialMediaType.FindAsync(id);
            if (socialMediaType == null)
            {
                return NotFound();
            }
            return View(socialMediaType);
        }

        // POST: SocialMediaTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,Value,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] SocialMediaType socialMediaType)
        {
            if (id != socialMediaType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socialMediaType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocialMediaTypeExists(socialMediaType.Id))
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
            return View(socialMediaType);
        }

        // GET: SocialMediaTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialMediaType = await _context.SocialMediaType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialMediaType == null)
            {
                return NotFound();
            }

            return View(socialMediaType);
        }

        // POST: SocialMediaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var socialMediaType = await _context.SocialMediaType.FindAsync(id);
            if (socialMediaType != null)
            {
                _context.SocialMediaType.Remove(socialMediaType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocialMediaTypeExists(int id)
        {
            return _context.SocialMediaType.Any(e => e.Id == id);
        }
    }
}
