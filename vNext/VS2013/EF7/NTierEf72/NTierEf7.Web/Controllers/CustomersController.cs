using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NTierEf7.Web.Models;

namespace NTierEf7.Web.Controllers
{
    public class CustomersController : ApiController
    {
        // GET api/customers
        [ResponseType(typeof(IEnumerable<Customer>))]
        public async Task<IHttpActionResult> Get()
        {
            using (var dbContext = new NorthwindSlimContext())
            {
                var products = await dbContext.Customers
                    .OrderBy(p => p.CustomerId)
                    .ToListAsync();
                return Ok(products);
            }
        }
    }
}
