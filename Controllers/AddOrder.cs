using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using RightProject.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RightProject.Controllers
{
    [Authorize(Roles = "ShopManager", AuthenticationSchemes = "Cookies")]
    public class AddOrder : Controller
    {
        private readonly finalContext db;
        public static string Message { get; set; }
        public static string SuccessMsg { get; set; }
        //  public LoginModel Input { get => input; set => input = value; }

        public AddOrder(finalContext context)
        {
            db = context;
        }
        [HttpGet]
        public async Task<IActionResult> AddOrderProduct(string search)
        {
            string name = User.FindFirst(ClaimTypes.Name)?.Value;
            int shopm = db.ShopManagers.Where(a => a.Username == name).Select(a => a.Shopmanagerid).SingleOrDefault();
            int id = db.Shops.Where(a => a.Managerid == shopm).Select(a => a.Shopid).SingleOrDefault();

            // int id = _context.ShopManagers.Where(e => e.Username == name).Select(e => e.Shopmanagerid).SingleOrDefault();
            // var finalContext = _context.Shops.Include(s => s.Manager)
            //.Where(s => s.Managerid == id);
            // return View(await finalContext.SingleOrDefaultAsync());
            var finalContext = db.Products.Where(a => a.shopId == id);
            var appDb = new object();
            if (!string.IsNullOrEmpty(search))
            {
                //finalContext = _context.Products.Where(a => a.shopId == id).Where(x => x.Name.Contains(search));
                appDb = await db.Products.Where(a => a.shopId == id).Where(x => x.Name.Contains(search)).ToListAsync();
            }
            else
            {
                appDb = await db.Products.Where(a => a.shopId == id).ToListAsync();
            }

            return View(appDb);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrderProduct(IEnumerable<int> ID,string UserName, IEnumerable<string> amount)
        {
            List<int> products= ID.ToList();
            List<string> amounts = amount.ToList();
            List<int> x = new List<int>();
            List<int> y = new List<int>();
            Order o = new Order();
            var user =await db.Customers.Where(c => c.Username == UserName).Select(e => e.Customerid).SingleOrDefaultAsync();
            o.Custid = user;
            //unique
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=","");
            GuidString = GuidString.Replace("+", "");
            //generate barcode
         
            o.Parcode = GuidString;

            db.Orders.Add(o);
            ProductShopOrder pso  = null;
            await db.SaveChangesAsync();
            int order= await db.Orders.Where(o => o.Parcode == GuidString).Select(p => p.Orderid).SingleOrDefaultAsync();
            if (products.Count > 0)
            {
                for (var i = 0; i < products.Count; i++)
                {
                    pso = new ProductShopOrder();
                    // pso.Id = 9999;
                    // x[i] = db.Products.Where(p => p.Productid == products[i]).Select(p => p.Productid).SingleOrDefault();
                    pso.IdProduct = products[i];
                    pso.IdOrder = order;
                    pso.Amount = amounts[i];
                    //pso.IdProductNavigation =await db.Products.Where(p => p.Productid == products[i]).SingleOrDefaultAsync();
                    //pso.IdOrderNavigation =await db.Orders.Where(o => o.Parcode == bar).SingleOrDefaultAsync();
                    db.ProductShopOrders.Add(pso);


                    await db.SaveChangesAsync();

                }
             
                return View("viewBar",o);
            }
            else
            {

                return RedirectToAction(nameof(AddOrderProduct));
            }
        }
      

     public IActionResult viewBar(Order bar)
        {
           // var barcode = db.Orders.Where(a => a.Parcode == bar).Select(a => a.Parcode == bar).SingleOrDefault();
            //Message = bar;
            
            return View(bar);
        }
       
       

       

     
   
    }
}
