using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RightProject.Models;

namespace RightProject.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
    public class ShopsController : Controller
    {
        private readonly finalContext _context;

        public ShopsController(finalContext context)
        {
            _context = context;
        }
       
        // GET: Shops
        public async Task<IActionResult> Index()
        {
            // string name = User.FindFirst(ClaimTypes.Name)?.Value;
            // int id = _context.ShopManagers.Where(e => e.Username == name).Select(e => e.Shopmanagerid).SingleOrDefault();
            var finalContext = _context.Shops.Include(s => s.Manager);
            //    .Where(s => s.Managerid == id);
            return View(await finalContext.ToListAsync());
        }
      //  [Authorize(AuthenticationSchemes = "Cookies")]

        // GET: Shops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _context.Shops
                .Include(s => s.Manager)
                .FirstOrDefaultAsync(m => m.Shopid == id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
        // GET: Shops/Create
        public IActionResult Create()
        {
            ViewData["Managerid"] = new SelectList(_context.ShopManagers, "Shopmanagerid", "Username");
            return View();
        }

        // POST: Shops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Shopid,Managerid,Name,Locathion,Phonenumber")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Managerid"] = new SelectList(_context.ShopManagers, "Shopmanagerid", "Username", shop.Managerid);
            return View(shop);
        }
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
        // GET: Shops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }
            ViewData["Managerid"] = new SelectList(_context.ShopManagers, "Shopmanagerid", "Username", shop.Managerid);
            return View(shop);
        }

        // POST: Shops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Shopid,Managerid,Name,Locathion,Phonenumber")] Shop shop)
        {
            if (id != shop.Shopid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopExists(shop.Shopid))
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
            ViewData["Managerid"] = new SelectList(_context.ShopManagers, "Shopmanagerid", "Username", shop.Managerid);
            return View(shop);
        }

        // GET: Shops/Delete/5
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _context.Shops
                .Include(s => s.Manager)
                .FirstOrDefaultAsync(m => m.Shopid == id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.Shopid == id);
        }
    }
}
