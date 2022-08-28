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
    public class ShopManagersController : Controller
    {
        private readonly finalContext _context;

        public ShopManagersController(finalContext context)
        {
            _context = context;
        }

        // GET: ShopManagers
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShopManagers.ToListAsync());
        }

        // GET: ShopManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopManager = await _context.ShopManagers
                .FirstOrDefaultAsync(m => m.Shopmanagerid == id);
            if (shopManager == null)
            {
                return NotFound();
            }

            return View(shopManager);
        }

        // GET: ShopManagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShopManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Shopmanagerid,Salary,Name,Username,Password,PasswordConfirm,Email,lockout,lockTime,ErrorLogCount")] ShopManager shopManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopManager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shopManager);
        }

        // GET: ShopManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopManager = await _context.ShopManagers.FindAsync(id);
            if (shopManager == null)
            {
                return NotFound();
            }
            return View(shopManager);
        }

        // POST: ShopManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Shopmanagerid,Salary,Name,Username,Password,PasswordConfirm,Email,lockout,lockTime,ErrorLogCount")] ShopManager shopManager)
        {
            if (id != shopManager.Shopmanagerid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shopManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopManagerExists(shopManager.Shopmanagerid))
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
            return View(shopManager);
        }

        // GET: ShopManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopManager = await _context.ShopManagers
                .FirstOrDefaultAsync(m => m.Shopmanagerid == id);
            if (shopManager == null)
            {
                return NotFound();
            }

            return View(shopManager);
        }

        // POST: ShopManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shopManager = await _context.ShopManagers.FindAsync(id);
            _context.ShopManagers.Remove(shopManager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopManagerExists(int id)
        {
            return _context.ShopManagers.Any(e => e.Shopmanagerid == id);
        }
    }
}
