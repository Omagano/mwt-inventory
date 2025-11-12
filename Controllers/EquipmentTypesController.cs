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
    public class EquipmentTypesController : Controller
    {
        private readonly MvcAppContext _context;

        public EquipmentTypesController(MvcAppContext context)
        {
            _context = context;
        }

        // GET: EquipmentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EquipmentTypes.ToListAsync());
        }

        // GET: EquipmentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentTypes = await _context.EquipmentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentTypes == null)
            {
                return NotFound();
            }

            return View(equipmentTypes);
        }

        // GET: EquipmentTypes/Create
        public IActionResult Create()
        {
            return View(new EquipmentTypes { Name = String.Empty});
        }

        // POST: EquipmentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] EquipmentTypes equipmentTypes)
        {
            if (ModelState.IsValid)
            {
                _context.EquipmentTypes.Add(equipmentTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipmentTypes);
        }

        // GET: EquipmentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentTypes = await _context.EquipmentTypes.FindAsync(id);
            if (equipmentTypes == null)
            {
                return NotFound();
            }
            return View(equipmentTypes);
        }

        // POST: EquipmentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] EquipmentTypes equipmentTypes)
        {
            if (id != equipmentTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the audit fields
                    equipmentTypes.DateUpdated = DateTime.Now;
                    equipmentTypes.UpdatedBy = User.Identity?.Name ?? "System"; // Assuming you have a way to get the current user
                    _context.Update(equipmentTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentTypesExists(equipmentTypes.Id))
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
            return View(equipmentTypes);
        }

        // GET: EquipmentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentTypes = await _context.EquipmentTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentTypes == null)
            {
                return NotFound();
            }

            return View(equipmentTypes);
        }

        // POST: EquipmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipmentTypes = await _context.EquipmentTypes.FindAsync(id);
            if (equipmentTypes != null)
            {
                _context.EquipmentTypes.Remove(equipmentTypes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentTypesExists(int id)
        {
            return _context.EquipmentTypes.Any(e => e.Id == id);
        }
    }
}
