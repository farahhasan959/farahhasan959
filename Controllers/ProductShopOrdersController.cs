using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RightProject.Models;

namespace RightProject.Controllers
{
    public class ProductShopOrdersController : Controller
    {
        private readonly finalContext _context;

        public ProductShopOrdersController(finalContext context)
        {
            _context = context;
        }

        // GET: ProductShopOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductShopOrders.ToListAsync());
        }

        // GET: ProductShopOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productShopOrder = await _context.ProductShopOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productShopOrder == null)
            {
                return NotFound();
            }

            return View(productShopOrder);
        }

        // GET: ProductShopOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductShopOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdOrder,IdProduct,Amount")] ProductShopOrder productShopOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productShopOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productShopOrder);
        }

        // GET: ProductShopOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productShopOrder = await _context.ProductShopOrders.FindAsync(id);
            if (productShopOrder == null)
            {
                return NotFound();
            }
            return View(productShopOrder);
        }

        // POST: ProductShopOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdOrder,IdProduct,Amount")] ProductShopOrder productShopOrder)
        {
            if (id != productShopOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productShopOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductShopOrderExists(productShopOrder.Id))
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
            return View(productShopOrder);
        }

        // GET: ProductShopOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productShopOrder = await _context.ProductShopOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productShopOrder == null)
            {
                return NotFound();
            }

            return View(productShopOrder);
        }

        // POST: ProductShopOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productShopOrder = await _context.ProductShopOrders.FindAsync(id);
            _context.ProductShopOrders.Remove(productShopOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductShopOrderExists(int id)
        {
            return _context.ProductShopOrders.Any(e => e.Id == id);
        }
    }
}
