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
    public class OfficeController : Controller
    {
        private readonly MvcAppContext _context;

        public OfficeController(MvcAppContext context)
        {
            _context = context;
        }

        // GET: Office
        public async Task<IActionResult> Index()
        {
            return View(await _context.OfficeEntity.ToListAsync());
        }

        // GET: Office/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeEntity = await _context.OfficeEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officeEntity == null)
            {
                return NotFound();
            }

            return View(officeEntity);
        }

        // GET: Office/Create
        public IActionResult Create()
        {
            return View(new OfficeEntity{ Name = string.Empty });
        }

        // POST: Office/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] OfficeEntity officeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.OfficeEntity.Add(officeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(officeEntity);
        }

        // GET: Office/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeEntity = await _context.OfficeEntity.FindAsync(id);
            if (officeEntity == null)
            {
                return NotFound();
            }
            return View(officeEntity);
        }

        // POST: Office/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] OfficeEntity officeEntity)
        {
            if (id != officeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Set auditing fields
                    officeEntity.DateUpdated = DateTime.Now;
                    officeEntity.UpdatedBy = User.Identity?.Name ?? "System";
                    _context.Update(officeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficeEntityExists(officeEntity.Id))
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
            return View(officeEntity);
        }

        // GET: Office/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeEntity = await _context.OfficeEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officeEntity == null)
            {
                return NotFound();
            }

            return View(officeEntity);
        }

        // POST: Office/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var officeEntity = await _context.OfficeEntity.FindAsync(id);
            if (officeEntity != null)
            {
                //6OfficeEntity.IsDeleted = true;
                officeEntity.DateDeleted = DateTime.UtcNow;
                officeEntity.DeletedBy = User.Identity?.Name ?? "System";
                _context.OfficeEntity.Update(officeEntity);
                _context.OfficeEntity.Remove(officeEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficeEntityExists(int id)
        {
            return _context.OfficeEntity.Any(e => e.Id == id);
        }
    }
}
