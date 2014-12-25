using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Data.Entity.Update;
using NTierEf7.Web.Models;
using NTierEf72.Entities;

namespace NTierEf7.Web.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly NorthwindSlimContext _dbContext = new NorthwindSlimContext();

        // GET: api/Orders?customerId = 5
        [ResponseType(typeof(IEnumerable<Order>))]
        public async Task<IHttpActionResult> Get(string customerId)
        {
            var orders = await _dbContext.Orders
                .Include(o => o.Customer)
                //.Include("OrderDetails.Product")
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
            return Ok(orders);
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Customer)
                //.Include("OrderDetails.Product")
                .SingleOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> PutOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.Orders.Update(order);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.OrderId))
                {
                    return NotFound();
                }
                throw;
            }

            return Ok(order);
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", 
                new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteOrder(int id)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .SingleOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return Ok();
            for (int i = order.OrderDetails.Count - 1; i > -1; i--)
            {
                var detail = order.OrderDetails.ElementAt(i);
                _dbContext.OrderDetails.Remove(detail);
            }
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return _dbContext.Orders.Any(e => e.OrderId == id);
        }
    }
}