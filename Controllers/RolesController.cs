using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventorySystem.Models;
using MvcMovie.Data;
//using MvcApp.Migrations;

namespace MvcApp.Controllers
{
    public class RolesController : Controller
    {
        private readonly MvcAppContext _context;

        public RolesController(MvcAppContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var mvcAppContext = _context.RolesEntity.Include(r => r.Privilege);
            return View(await mvcAppContext.ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolesEntity = await _context.RolesEntity
                .Include(r => r.Privilege)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolesEntity == null)
            {
                return NotFound();
            }

            return View(rolesEntity);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            ViewData["PrivilegeId"] = new SelectList(_context.PrivilegeEntity, "Id", "Id");
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleName,PrivilegeId,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] RolesEntity rolesEntity)
        {
            if (ModelState.IsValid)
            {
                _context.RolesEntity.Add(rolesEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrivilegeId"] = new SelectList(_context.PrivilegeEntity, "Id", "Id", rolesEntity.PrivilegeId);
            return View(rolesEntity);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolesEntity = await _context.RolesEntity.FindAsync(id);
            if (rolesEntity == null)
            {
                return NotFound();
            }
            ViewData["PrivilegeId"] = new SelectList(_context.PrivilegeEntity, "Id", "Id", rolesEntity.PrivilegeId);
            return View(rolesEntity);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleName,PrivilegeId,DateCreated,CreatedBy,DateUpdated,UpdatedBy,IsDeleted,DateDeleted,DeletedBy")] RolesEntity rolesEntity)
        {
            if (id != rolesEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //add updated by upadtes
                    rolesEntity.DateUpdated = DateTime.Now;
                    rolesEntity.UpdatedBy = User.Identity?.Name ?? "System"; 
                    _context.Update(rolesEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolesEntityExists(rolesEntity.Id))
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
            ViewData["PrivilegeId"] = new SelectList(_context.PrivilegeEntity, "Id", "Id", rolesEntity.PrivilegeId);
            return View(rolesEntity);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolesEntity = await _context.RolesEntity
                .Include(r => r.Privilege)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolesEntity == null)
            {
                return NotFound();
            }

            return View(rolesEntity);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rolesEntity = await _context.RolesEntity.FindAsync(id);
            if (rolesEntity != null)
            {
                //rolesEntity. isDeleted = true;
                rolesEntity.DateDeleted = DateTime.Now;
                _context.RolesEntity.Remove(rolesEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolesEntityExists(int id)
        {
            return _context.RolesEntity.Any(e => e.Id == id);
        }
    }
}
