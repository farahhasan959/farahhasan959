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
    public class CustomersApiController : ControllerBase
    {
        private readonly finalContext _context;

        public CustomersApiController(finalContext context)
        {
            _context = context;
        }

        [Route("AddPoints")]
        [HttpPost]
        public async Task<IActionResult> AddPoints([FromQuery] int id, [FromQuery] int orderId)
            
        {
             
            if (CustomerExists(id))
            {

                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }

                var order = await _context.Orders.Include(o=>o.ProductShopOrders).FirstOrDefaultAsync(o=>o.Orderid==orderId);

                if (order == null)
                {
                    return NotFound();
                }
                var valTemp = customer.Points;
                Product product  ;
                String amount;

                List<int> IdProducts  = _context.ProductShopOrders.Where(x=>x.IdOrder==orderId).Select(x => x.IdProduct).ToList();
               
               foreach (var idVal in IdProducts)

               {
                 amount = _context.ProductShopOrders.Where(x => x.IdOrder == orderId).Where(a => a.IdProduct == idVal).Select(a => a.Amount).SingleOrDefault();
                  product = await _context.Products.FindAsync(idVal);
                 valTemp = valTemp + (product.Points.Value*Int16.Parse(amount));
               }

                customer.Points = valTemp;

                try
                {
                    _context.Entry(customer).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    return Ok(new { sucsses = true, points = valTemp });

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return NoContent();
        }

        // GET: api/CustomersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }


        [Route("updatePoint")]
        [HttpPut("{id}")]
        public async Task<IActionResult> updatePoint([FromBody] int id, int points)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var customer = await _context.Customers.FindAsync(id);
            customer.Points = customer.Points - points;



            try
            {
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new { sucsses = true, points = customer.Points });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }


        [Route("GetPoints")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gift>>> GetPointsByCustomer(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(new { sucsses = true, points = customer.Points });
        }
        // GET: api/CustomersApi/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Customer>> GetCustomer(int id)
        //{
        //    var customer = await _context.Customers.FindAsync(id);

        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return customer;
        //}

        // PUT: api/CustomersApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCustomer(int id, Customer customer)
        //{
        //    if (id != customer.Customerid)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(customer).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CustomerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}



        // POST: api/CustomersApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //[Route("AddCustomer")]
        //public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        //{
        //    _context.Customers.Add(customer);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCustomer", new { id = customer.Customerid }, customer);
        //}

        // DELETE: api/CustomersApi/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCustomer(int id)
        //{
        //    var customer = await _context.Customers.FindAsync(id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Customers.Remove(customer);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Customerid == id);
        }
    }
}
