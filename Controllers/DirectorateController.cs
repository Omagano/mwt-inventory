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
    public class DirectorateController : Controller
    {
        private readonly MvcAppContext _context;

        public DirectorateController(MvcAppContext context)
        {
            _context = context;
        }

        // GET: Directorate
        public async Task<IActionResult> Index()
        {
            var mvcAppContext = _context.DirectorateEntity.Include(d => d.Department);
            return View(await mvcAppContext.ToListAsync());
        }

        // GET: Directorate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorateEntity = await _context.DirectorateEntity
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directorateEntity == null)
            {
                return NotFound();
            }

            return View(directorateEntity);
        }

        // GET: Directorate/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentEntity, "Id", "Name");
            return View(new DirectorateEntity { Name = string.Empty });
        }

        // POST: Directorate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DepartmentId,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] DirectorateEntity directorateEntity)
        {
            if (ModelState.IsValid)
            {
                _context.DirectorateEntity.Add(directorateEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentEntity, "Id", "Name", directorateEntity.DepartmentId);
            return View(directorateEntity);
        }

        // GET: Directorate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorateEntity = await _context.DirectorateEntity.FindAsync(id);
            if (directorateEntity == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentEntity, "Id", "Name", directorateEntity.DepartmentId);
            return View(directorateEntity);
        }

        // POST: Directorate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DepartmentId,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] DirectorateEntity directorateEntity)
        {
            if (id != directorateEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    directorateEntity.DateUpdated = DateTime.UtcNow;
                    directorateEntity.UpdatedBy = User.Identity?.Name ?? "System";
                    _context.Update(directorateEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorateEntityExists(directorateEntity.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentEntity, "Id", "Name", directorateEntity.DepartmentId);
            return View(directorateEntity);
        }

        // GET: Directorate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var directorateEntity = await _context.DirectorateEntity
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directorateEntity == null)
            {
                return NotFound();
            }

            return View(directorateEntity);
        }

        // POST: Directorate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var directorateEntity = await _context.DirectorateEntity.FindAsync(id);
            if (directorateEntity != null)
            {
                _context.DirectorateEntity.Remove(directorateEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectorateEntityExists(int id)
        {
            return _context.DirectorateEntity.Any(e => e.Id == id);
        }
    }
}
