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
    public class PrivilegeController : Controller
    {
        private readonly MvcAppContext _context;

        public PrivilegeController(MvcAppContext context)
        {
            _context = context;
        }

        // GET: Privilege
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrivilegeEntity.ToListAsync());
        }

        // GET: Privilege/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilegeEntity = await _context.PrivilegeEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (privilegeEntity == null)
            {
                return NotFound();
            }

            return View(privilegeEntity);
        }

        // GET: Privilege/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Privilege/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Privilege")] PrivilegeEntity privilegeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(privilegeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(privilegeEntity);
        }

        // GET: Privilege/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilegeEntity = await _context.PrivilegeEntity.FindAsync(id);
            if (privilegeEntity == null)
            {
                return NotFound();
            }
            return View(privilegeEntity);
        }

        // POST: Privilege/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Privilege")] PrivilegeEntity privilegeEntity)
        {
            if (id != privilegeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(privilegeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrivilegeEntityExists(privilegeEntity.Id))
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
            return View(privilegeEntity);
        }

        // GET: Privilege/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privilegeEntity = await _context.PrivilegeEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (privilegeEntity == null)
            {
                return NotFound();
            }

            return View(privilegeEntity);
        }

        // POST: Privilege/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var privilegeEntity = await _context.PrivilegeEntity.FindAsync(id);
            if (privilegeEntity != null)
            {
                _context.PrivilegeEntity.Remove(privilegeEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrivilegeEntityExists(int id)
        {
            return _context.PrivilegeEntity.Any(e => e.Id == id);
        }
    }
}
