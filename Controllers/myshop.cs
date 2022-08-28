using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RightProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RightProject.Controllers
{
    [Authorize(Roles = "ShopManager", AuthenticationSchemes = "Cookies")]
    public class myshop : Controller
    {
        private readonly finalContext _context;

        public myshop(finalContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> shop()
        {
            string name = User.FindFirst(ClaimTypes.Name)?.Value;
            int id = _context.ShopManagers.Where(e => e.Username == name).Select(e => e.Shopmanagerid).SingleOrDefault();
            var finalContext = _context.Shops.Include(s => s.Manager)
                .Where(s => s.Managerid == id);
            return View(await finalContext.SingleOrDefaultAsync());
        }
    }
}
