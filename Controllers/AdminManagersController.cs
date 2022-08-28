using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RightProject.Models;

namespace RightProject.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
    public class AdminManagersController : Controller
    {
        private readonly finalContext _context;

        public AdminManagersController(finalContext context)
        {
            _context = context;
        }

        // GET: AdminManagers
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdminManagers.ToListAsync());
        }

        // GET: AdminManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminManager = await _context.AdminManagers
                .FirstOrDefaultAsync(m => m.Managerid == id);
            if (adminManager == null)
            {
                return NotFound();
            }

            return View(adminManager);
        }

        // GET: AdminManagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Managerid,Name,Username,Password,PasswordConfirm,Email,lockout,lockTime,ErrorLogCount")] AdminManager adminManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminManager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminManager);
        }

        // GET: AdminManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminManager = await _context.AdminManagers.FindAsync(id);
            if (adminManager == null)
            {
                return NotFound();
            }
            return View(adminManager);
        }

        // POST: AdminManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Managerid,Name,Username,Password,PasswordConfirm,Email,lockout,lockTime,ErrorLogCount")] AdminManager adminManager)
        {
            if (id != adminManager.Managerid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminManagerExists(adminManager.Managerid))
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
            return View(adminManager);
        }

        // GET: AdminManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminManager = await _context.AdminManagers
                .FirstOrDefaultAsync(m => m.Managerid == id);
            if (adminManager == null)
            {
                return NotFound();
            }

            return View(adminManager);
        }

        // POST: AdminManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminManager = await _context.AdminManagers.FindAsync(id);
            _context.AdminManagers.Remove(adminManager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminManagerExists(int id)
        {
            return _context.AdminManagers.Any(e => e.Managerid == id);
        }
    }
}
