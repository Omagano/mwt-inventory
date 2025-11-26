using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventorySystem.Models;
using MvcMovie.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace MvcApp.Controllers
{
    public class EquipmentsController : Controller
    {
        private readonly MvcAppContext _context;
        private readonly ILogger<EquipmentsController> _logger;

        public EquipmentsController(MvcAppContext context, ILogger<EquipmentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Equipments
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var equipments = from e in _context.EquipmentsEntity
                             .Include(e => e.SupplierEntity)
                             .Include(e => e.BrandsEntity)
                             .Include(e => e.EquipmentTypes)
                             .Include(e => e.Model)
                             where !e.IsDeleted // Only show non-deleted items
                             select e;

            if (!string.IsNullOrEmpty(searchString))
            {
                equipments = equipments.Where(e => e.SerialNumber.Contains(searchString.ToUpper())
                                                || e.BrandsEntity.Name.Contains(searchString.ToUpper())
                                                || e.EquipmentTypes.Name.Contains(searchString.ToUpper())
                                                || e.Model.Name.Contains(searchString.ToUpper())
                                                || e.SupplierEntity.Name.Contains(searchString.ToUpper())
                                                || e.Status.Contains(searchString.ToUpper()));
            }

            return View(await equipments.ToListAsync());
        }
  

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.EquipmentsEntity
                .Include(e => e.SupplierEntity)
                .Include(e => e.BrandsEntity)
                .Include(e => e.EquipmentTypes)
                .Include(e => e.Model)
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);


            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipments/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        // POST: Equipments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SerialNumber,Status,VendorPrice,Value,ArrivalDate, Warantyfrom, WarantyEnd, ReceiverName, SuppliersId,TypeId,ModelId,BrandId")] EquipmentsEntity equipment)
        {
            try
            {
                // Check for duplicate serial number
                if (await _context.EquipmentsEntity.AnyAsync(e => 
                    e.SerialNumber == equipment.SerialNumber && !e.IsDeleted))
                {
                    ModelState.AddModelError("SerialNumber", "This serial number already exists.");
                }

                if (ModelState.IsValid)
                {
                    // Set audit fields
                    equipment.Id = 0;
                    equipment.DateCreated = DateTime.UtcNow;
                    equipment.CreatedBy = User.Identity?.Name ?? "System";
                    equipment.IsDeleted = false;
                    //equipment.EquipmentTypes = await _context.EquipmentTypes.FindAsync(equipment.TypeId);

                    _context.Add(equipment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var modelStateKey in ModelState.Keys)
                    {
                        var modelStateVal = ModelState[modelStateKey];
                        foreach (var error in modelStateVal.Errors)
                        {
                            _logger.LogWarning($"ModelState error on {modelStateKey}: {error.ErrorMessage}");
                        }
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating equipment");
                ModelState.AddModelError("AddModelError", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.EquipmentsEntity.FindAsync(id);
            if (equipment == null || equipment.IsDeleted)
            {
                return NotFound();
            }

            await PopulateDropdowns(equipment);
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("Id,SerialNumber,Status,VendorPrice,Value,ArrivalDate, Warantyfrom, WarantyEnd, ReceiverName, SuppliersId,TypeId,ModelId,BrandId")] EquipmentsEntity equipment)
        {
            if (id != equipment.Id)
            {
                return NotFound();
            }

            try
            {
                // Check for duplicate serial number (excluding current record)
                if (await _context.EquipmentsEntity.AnyAsync(e => 
                    e.SerialNumber == equipment.SerialNumber && 
                    e.Id != equipment.Id && 
                    !e.IsDeleted))
                {
                    ModelState.AddModelError("SerialNumber", "This serial number already exists.");
                }

                if (ModelState.IsValid)
                {
                    // Update audit fields
                    equipment.DateUpdated = DateTime.UtcNow;
                    equipment.UpdatedBy = User.Identity?.Name ?? "System";

                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EquipmentExists(equipment.Id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, "Concurrency error editing equipment");
                    ModelState.AddModelError("", "Unable to save changes. The record you attempted to edit was modified by another user.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing equipment");
                ModelState.AddModelError("", "An error occurred while updating the equipment.");
            }

            await PopulateDropdowns(equipment);
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.EquipmentsEntity
                .Include(e => e.BrandsEntity)
                .Include(e => e.EquipmentTypes)
                .Include(e => e.Model)
                .Include(e => e.SupplierEntity)
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);

            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.EquipmentsEntity.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            try
            {
                // Soft delete approach
                equipment.IsDeleted = true;
                equipment.DateDeleted = DateTime.UtcNow;
                equipment.DeletedBy = User.Identity?.Name ?? "System";

                _context.Update(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting equipment");
                ModelState.AddModelError("", "Unable to delete equipment. Try again, and if the problem persists see your system administrator.");
                return View("Delete", equipment);
            }
        }

        // AJAX validation endpoint
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifySerialNumber(string serialNumber, int id = 0)
        {
            var exists = await _context.EquipmentsEntity
                .AnyAsync(e => e.SerialNumber == serialNumber && 
                             e.Id != id && 
                             !e.IsDeleted);

            return Json(!exists);
        }

        private bool EquipmentExists(int id)
        {
            return _context.EquipmentsEntity.Any(e => e.Id == id && !e.IsDeleted);
        }

        private async Task PopulateDropdowns(EquipmentsEntity equipment = null)
        {
            ViewData["SuppliersId"] = new SelectList(
                await _context.SuppliersEntity.ToListAsync(), 
                "Id", "Name", 
                equipment?.SuppliersId);

            ViewData["BrandId"] = new SelectList(
                await _context.BrandsEntity.ToListAsync(), 
                "Id", "Name", 
                equipment?.BrandId);

            ViewData["TypeId"] = new SelectList(
                await _context.EquipmentTypes.ToListAsync(), 
                "Id", "Name", 
                equipment?.TypeId);

            ViewData["ModelId"] = new SelectList(
                await _context.Model.ToListAsync(), 
                "Id", "Name", 
                equipment?.ModelId);
        }
    }
}