using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMMS.WebMVC.App.Data;
using DMMS.WebMVC.App.Models;
using DMMS.Models;

namespace DMMS.WebMVC.App.Controllers
{
    public class WebsitesController : Controller
    {
        private readonly DMMSWebMVCAppContext _context;

        public WebsitesController(DMMSWebMVCAppContext context)
        {
            _context = context;
        }

        //// GET: Websites
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Website.ToListAsync());
        //}

        // GET: Contents
        public async Task<IActionResult> Index(string companyIdentity)
        {
            Company? company = _context.Companies.Where(a => a.Identity == companyIdentity).FirstOrDefault();
            ViewBag.Company = companyIdentity;
            if (company != null)
            {
                HttpContext.Session.SetString("Name", "Ninad Gawankar");
                HttpContext.Session.SetInt32("CompanyId", company.Id);
                HttpContext.Session.SetString("CompanyIdentity", company.Identity);
                HttpContext.Session.SetInt32("UserId", 1);
                return View(await _context.Website.Where(x => x.Status == 1 && x.CompanyId == company.Id).OrderByDescending(x => x.Id).ToListAsync());
            }
            return View(await _context.Website.Where(x => x.CompanyId == 0).OrderByDescending(x => x.Id).ToListAsync());
        }

        // GET: Websites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
            {
                return NotFound();
            }

            var website = await _context.Website
                .FirstOrDefaultAsync(m => m.Id == id);
            if (website == null)
            {
                return NotFound();
            }

            return View(website);
        }

        // GET: Websites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Websites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Url,Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] Website website)
        {
            if (ModelState.IsValid)
            {
                _context.Add(website);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(website);
        }

        // GET: Websites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Website.FindAsync(id);
            if (website == null)
            {
                return NotFound();
            }
            return View(website);
        }

        // POST: Websites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Url,Id,Name,Alias,Guid,Identity,Status,Tenant,CreatedBy,CreationDate,ModifiedBy,ModifiedDate")] Website website)
        {
            if (id != website.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(website);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebsiteExists(website.Id))
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
            return View(website);
        }

        // GET: Websites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Website
                .FirstOrDefaultAsync(m => m.Id == id);
            if (website == null)
            {
                return NotFound();
            }

            return View(website);
        }

        // POST: Websites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var website = await _context.Website.FindAsync(id);
            if (website != null)
            {
                _context.Website.Remove(website);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebsiteExists(int id)
        {
            return _context.Website.Any(e => e.Id == id);
        }
    }
}
