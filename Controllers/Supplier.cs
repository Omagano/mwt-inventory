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
    public class Supplier : Controller
    {
        private readonly MvcAppContext _context;

        public Supplier(MvcAppContext context)
        {
            _context = context;
        }

        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            return View(await _context.SuppliersEntity.ToListAsync());
        }

        // GET: Supplier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suppliersEntity = await _context.SuppliersEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suppliersEntity == null)
            {
                return NotFound();
            }

            return View(suppliersEntity);
        }

        // GET: Supplier/Create
        public IActionResult Create()
        {
            return View(new SuppliersEntity { Name = string.Empty, ContactPerson = string.Empty, ContactNumber = string.Empty, Email = string.Empty, Website = string.Empty, StreetAddress = string.Empty, City = string.Empty, Country = string.Empty, PostalCode = string.Empty });
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ContactPerson,ContactNumber,Email,Website,StreetAddress,City,Country,PostalCode,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] SuppliersEntity suppliersEntity)
        {
            if (ModelState.IsValid)
            {
                suppliersEntity.DateCreated = DateTime.Now;
                suppliersEntity.CreatedBy = User.Identity?.Name ?? "System";
                _context.Add(suppliersEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suppliersEntity);
        }

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suppliersEntity = await _context.SuppliersEntity.FindAsync(id);
            if (suppliersEntity == null)
            {
                return NotFound();
            }
            return View(suppliersEntity);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ContactPerson,ContactNumber,Email,Website,StreetAddress,City,Country,PostalCode,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] SuppliersEntity suppliersEntity)
        {
            if (id != suppliersEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    suppliersEntity.DateUpdated = DateTime.Now;
                    suppliersEntity.UpdatedBy = User.Identity?.Name ?? "System";
                    _context.Update(suppliersEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuppliersEntityExists(suppliersEntity.Id))
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
            return View(suppliersEntity);
        }

        // GET: Supplier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suppliersEntity = await _context.SuppliersEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suppliersEntity == null)
            {
                return NotFound();
            }

            return View(suppliersEntity);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suppliersEntity = await _context.SuppliersEntity.FindAsync(id);
            if (suppliersEntity != null)
            {
                // Optionally, you can set IsDeleted to true instead of removing it
                suppliersEntity.IsDeleted = true;
                suppliersEntity.DateDeleted = DateTime.Now;
                suppliersEntity.DeletedBy = User.Identity?.Name ?? "System";
                //_context.SuppliersEntity.Remove(suppliersEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuppliersEntityExists(int id)
        {
            return _context.SuppliersEntity.Any(e => e.Id == id);
        }
    }
}
