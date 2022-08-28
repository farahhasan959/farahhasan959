using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RightProject.Models;

namespace RightProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsApiController : ControllerBase
    {
        private readonly finalContext _context;

        public GiftsApiController(finalContext context)
        {
            _context = context;
        }

        // GET: api/GiftsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
        [Route("Gifts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gift>>> GetGiftsByPoints(int customerId)
        {
            System.Diagnostics.Debug.WriteLine("HELLOOOOOOOOOOO");
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return await _context.Gifts
                .Where(x => x.Points <= customer.Points)
                .ToListAsync();
        }

        // GET: api/GiftsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gift>> GetGift(int id)
        {
            var gift = await _context.Gifts.FindAsync(id);

            if (gift == null)
            {
                return NotFound();
            }

            return gift;
        }


        // POST: api/GiftsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        [Route("AddGift")]
        public async Task<ActionResult<Gift>> PostGift(Gift gift)
        {

            if(CustomerExists(gift.IdCustomer))
            {
                _context.Gifts.Add(gift);
                await _context.SaveChangesAsync();


                CustomerGiftShop customerGiftShop = new CustomerGiftShop();

                customerGiftShop.IdCustomer = gift.IdCustomer;
                customerGiftShop.IdGift = gift.Giftid;

                _context.CustomerGiftShops.Add(customerGiftShop);
                await _context.SaveChangesAsync();

                return Ok(new { sucsses = true, data = gift });
            }
            else {

                return Ok(new { sucsses = false, data = gift });
            }
          
        }*/

        private bool GiftExists(int id)
        {
            return _context.Gifts.Any(e => e.Giftid == id);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Customerid == id);
        }
    }
}
