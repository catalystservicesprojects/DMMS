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
    public class SellerEntitiesController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public SellerEntitiesController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        // GET: SellerEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.SellerEntity.ToListAsync());
        }

        // GET: SellerEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerEntity = await _context.SellerEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellerEntity == null)
            {
                return NotFound();
            }

            return View(sellerEntity);
        }

        // GET: SellerEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SellerEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BackgroundColor,TextColor,BorderColor,Style,Class,OpenIn,Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] SellerEntity sellerEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sellerEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sellerEntity);
        }

        // GET: SellerEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerEntity = await _context.SellerEntity.FindAsync(id);
            if (sellerEntity == null)
            {
                return NotFound();
            }
            return View(sellerEntity);
        }

        // POST: SellerEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BackgroundColor,TextColor,BorderColor,Style,Class,OpenIn,Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] SellerEntity sellerEntity)
        {
            if (id != sellerEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sellerEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerEntityExists(sellerEntity.Id))
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
            return View(sellerEntity);
        }

        // GET: SellerEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellerEntity = await _context.SellerEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellerEntity == null)
            {
                return NotFound();
            }

            return View(sellerEntity);
        }

        // POST: SellerEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sellerEntity = await _context.SellerEntity.FindAsync(id);
            if (sellerEntity != null)
            {
                _context.SellerEntity.Remove(sellerEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerEntityExists(int id)
        {
            return _context.SellerEntity.Any(e => e.Id == id);
        }
    }
}
