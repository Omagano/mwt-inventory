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
    public class DivisionController : Controller
    {
        private readonly MvcAppContext _context;

        public DivisionController(MvcAppContext context)
        {
            _context = context;
        }

        // GET: Division
        public async Task<IActionResult> Index()
        {
            return View(await _context.DivisionEntity.ToListAsync());
        }

        // GET: Division/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var divisionEntity = await _context.DivisionEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (divisionEntity == null)
            {
                return NotFound();
            }

            return View(divisionEntity);
        }

// GET: Division/Create
public IActionResult Create()
{
    ViewData["DirectorateId"] = new SelectList(_context.DirectorateEntity, "Id", "Name");
    return View();// add new DivisionEntity { Name = string.Empty, DirectorateId = 0 });
}

        // POST: Division/Create2
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,Name,DirectorateId")] DivisionEntity divisionEntity)
{
    if (ModelState.IsValid)
    {
        // Set auditing fields
        

        _context.DivisionEntity.Add(divisionEntity);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    ViewData["DirectorateId"] = new SelectList(_context.DirectorateEntity, "Id", "Name", divisionEntity.DirectorateId);
    return View(divisionEntity);
}

        // GET: Division/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var divisionEntity = await _context.DivisionEntity.FindAsync(id);
            if (divisionEntity == null)
            {
                return NotFound();
            }
            return View(divisionEntity);
        }

        // POST: Division/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] DivisionEntity divisionEntity)
        {
            if (id != divisionEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Set auditing fields
                    divisionEntity.DateUpdated = DateTime.UtcNow;
                    divisionEntity.UpdatedBy = User.Identity?.Name ?? "System";

                    _context.Update(divisionEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DivisionEntityExists(divisionEntity.Id))
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
            return View(divisionEntity);
        }

        // GET: Division/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var divisionEntity = await _context.DivisionEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (divisionEntity == null)
            {
                return NotFound();
            }

            return View(divisionEntity);
        }

        // POST: Division/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var divisionEntity = await _context.DivisionEntity.FindAsync(id);
            if (divisionEntity != null)
            {
                _context.DivisionEntity.Remove(divisionEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DivisionEntityExists(int id)
        {
            return _context.DivisionEntity.Any(e => e.Id == id);
        }
    }
}
