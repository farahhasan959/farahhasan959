using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RightProject.Models;
using RightProject.ViewModel;

namespace RightProject.Controllers
{
    [Authorize(Roles = "ShopManager", AuthenticationSchemes = "Cookies")]
    public class ProductsController : Controller
    {

        private readonly finalContext _context;
        private readonly IHostingEnvironment _host;
        public ProductsController(finalContext context, IHostingEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: Products
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            string name = User.FindFirst(ClaimTypes.Name)?.Value;
            int shopm = _context.ShopManagers.Where(a => a.Username == name).Select(a => a.Shopmanagerid).SingleOrDefault();
            int id = _context.Shops.Where(a => a.Managerid == shopm).Select(a => a.Shopid).SingleOrDefault();
            
           // int id = _context.ShopManagers.Where(e => e.Username == name).Select(e => e.Shopmanagerid).SingleOrDefault();
           // var finalContext = _context.Shops.Include(s => s.Manager)
                //.Where(s => s.Managerid == id);
           // return View(await finalContext.SingleOrDefaultAsync());
            var finalContext = _context.Products.Where(a => a.shopId == id);
            var appDb = new object();
            if (!string.IsNullOrEmpty(search))
            {
                //finalContext = _context.Products.Where(a => a.shopId == id).Where(x => x.Name.Contains(search));
                appDb = await _context.Products.Where(a => a.shopId == id).Where(x => x.Name.Contains(search)).ToListAsync();
            }
            else
            {
                appDb = await _context.Products.Where(a => a.shopId == id).ToListAsync();
            }

            return View(appDb);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var m = new ShopN();
            m.product = await _context.Products
                .Include(p => p.shopIdNavigation)
                .FirstOrDefaultAsync(m => m.Productid == id);
            m.shopname = await _context.Shops.Where(s => s.Shopid == m.product.shopId).Select(s => s.Name).SingleOrDefaultAsync();
            if (m.product == null)
            {
                return NotFound();
            }

            return View(m);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            //ViewData["shopId"] = new SelectList(_context.Shops, "Shopid", "Shopid");
            return View();
        }
        private bool IsImage(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            if (extension.Contains(".png"))
            {
                return true;
            }
            if (extension.Contains(".jpeg"))
            {
                return true;
            }
            if (extension.Contains(".jpg"))
            {
                return true;
            }
            if (extension.Contains(".gif"))
            {
                return true;
            }
            if (extension.Contains(".bmp"))
            {
                return true;
            }
            return false;
        }
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Productid,shopId,Name,Properties,Type,Manufacturer,Picture,Price,Points,PointsToBuy,Amount")] Product product, IFormFile img)
        {
            string name = User.FindFirst(ClaimTypes.Name)?.Value;
            int shopm = _context.ShopManagers.Where(a => a.Username == name).Select(a => a.Shopmanagerid).SingleOrDefault();
            int id = _context.Shops.Where(a => a.Managerid == shopm).Select(a => a.Shopid).SingleOrDefault();
            if (ModelState.IsValid)
            {
                string newFileName = string.Empty;

                if (img != null && img.Length > 0)
                {
                    string fn = img.FileName;
                    if (IsImage(fn))
                    {
                        string extension = Path.GetExtension(fn);
                        newFileName = Guid.NewGuid().ToString() + extension;
                        string fileName = Path.Combine(_host.WebRootPath + "/images/products", newFileName);
                        await img.CopyToAsync(new FileStream(fileName, FileMode.Create));
                    }
                    else
                    {
                        return View();
                    }


                }
                product.shopId=id;
                product.Picture = newFileName;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            
           // ViewData["shopId"] = new SelectList(_context.Shops, "Shopid", "Shopid", product.shopId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Productid,shopId,Name,Properties,Type,Manufacturer,Picture,Price,Points,PointsToBuy,Amount")] Product product)
        {
            if (id != product.Productid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int x = _context.Products.Where(a => a.Productid == product.Productid).Select(a => a.shopId).SingleOrDefault();
                    product.shopId =x;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Productid))
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
          //  ViewData["shopId"] = new SelectList(_context.Shops, "Shopid", "Shopid", product.shopId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.shopIdNavigation)
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Productid == id);
        }
    }
}
