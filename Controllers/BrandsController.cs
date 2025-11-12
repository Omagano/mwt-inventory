using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventorySystem.Models;
using MvcMovie.Data;

namespace MvcApp.Controllers
{
    public class BrandsController : Controller
    {
        private readonly MvcAppContext _context;

        public BrandsController(MvcAppContext context)
        {
            _context = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            return View(await _context.BrandsEntity.ToListAsync());
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandsEntity = await _context.BrandsEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brandsEntity == null)
            {
                return NotFound();
            }

            return View(brandsEntity);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            return View(new BrandsEntity { Name = string.Empty });
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] BrandsEntity brandsEntity)
        {
            if (ModelState.IsValid)
            {

                _context.BrandsEntity.Add(brandsEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brandsEntity);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandsEntity = await _context.BrandsEntity.FindAsync(id);
            if (brandsEntity == null)
            {
                return NotFound();
            }
            return View(brandsEntity);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] BrandsEntity brandsEntity)
        {
            if (id != brandsEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the entity's properties
                    brandsEntity.DateUpdated = DateTime.Now;
                    brandsEntity.UpdatedBy = User.Identity?.Name ?? "System";
                    _context.Update(brandsEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandsEntityExists(brandsEntity.Id))
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
            return View(brandsEntity);
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandsEntity = await _context.BrandsEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brandsEntity == null)
            {
                return NotFound();
            }

            return View(brandsEntity);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brandsEntity = await _context.BrandsEntity.FindAsync(id);
            if (brandsEntity != null)
            {
                // Optionally, you can set IsDeleted to true instead of removing it 
                brandsEntity.IsDeleted = true;
                brandsEntity.DateDeleted = DateTime.Now;
                brandsEntity.DeletedBy = User.Identity?.Name ?? "System";
                _context.BrandsEntity.Remove(brandsEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandsEntityExists(int id)
        {
            return _context.BrandsEntity.Any(e => e.Id == id);
        }
    }
}
