using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RightProject.Data;
using RightProject.Models;

namespace RightProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly finalContext _context;

        public AuthApiController(finalContext context)
        {
            _context = context;
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody] Customer customer)
        {
            string hash = AppHash.HashPassword(customer.Password);
            Customer CustomersObj = _context.Customers
                .Where(x => x.Username == customer.Username)
                .Where(x => x.Password == hash)
                .FirstOrDefault();
            if(CustomersObj != null)
            {
                return Ok(new { sucsses= true , data = CustomersObj });

            }
            else
            {
                return Ok(new { sucsses = false , data = CustomersObj });

            }
        }


        private bool ShopManagerExists(int id)
        {
            return _context.ShopManagers.Any(e => e.Shopmanagerid == id);
        }
    }
}
