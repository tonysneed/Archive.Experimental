using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HelloAspNet5.Entities;
using HelloAspNet5.Web.Data;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity.Update;

namespace HelloAspNet5.Web.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly NorthwindSlimContext _dbContext;

        public OrdersController(NorthwindSlimContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Orders?customerId=ALFKI
        public async Task<IActionResult> Get(string customerId)
        {
            var orders = await _dbContext.Orders
                //.Include(o => o.Customer) // not supported
                //.Include(o => o.OrderDetails) // not supported
                // EF7: Multi-level Include not yet supported
                //.Include("OrderDetails.Product")
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
            return Ok(orders);
        }

        // GET: api/Orders/5
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _dbContext.Orders
                //.Include(o => o.Customer)
                // EF7: Multi-level Include not yet supported
                //.Include("OrderDetails.Product")
                .SingleOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // PUT: api/Orders/5
        public async Task<IActionResult> PutOrder(Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // EF7: Entry state API replaced by DbSet.Update
            //_dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.Orders.Update(order);

            // EF7: Add, Update and Remove DbSet methods accept arrays
            //_dbContext.OrderDetails.Add(addedDetails.ToArray());
            //_dbContext.OrderDetails.Update(updatedDetails.ToArray());
            //_dbContext.OrderDetails.Remove(deletedDetails.ToArray());

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.OrderId))
                    return NotFound();
                throw;
            }

            return Ok(order);
        }

        // POST: api/Orders
        public async Task<IActionResult> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _dbContext.Orders.Add(order);

            // EF7: Adding parent does not add children
            foreach (var detail in order.OrderDetails)
                _dbContext.OrderDetails.Add(detail);

            await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", 
                new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _dbContext.Orders
                //.Include(o => o.Customer)
                //.Include(o => o.OrderDetails)
                .SingleOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return Ok();

            var details = await _dbContext.OrderDetails
                .Where(o => o.OrderId == id).ToListAsync();
            for (int i = details.Count - 1; i > -1; i--)
            {
                var detail = order.OrderDetails.ElementAt(i);
                _dbContext.OrderDetails.Remove(detail);
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        // TODO: Replace DeleteOrder with DeleteOrder2,
        // when Include is implemented
        public async Task<IActionResult> DeleteOrder2(int id)
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
                _dbContext.Dispose();
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return _dbContext.Orders.Any(e => e.OrderId == id);
        }
    }
}