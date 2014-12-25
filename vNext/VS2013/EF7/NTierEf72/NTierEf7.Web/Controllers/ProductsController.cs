using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NTierEf7.Web.Models;
using NTierEf72.Entities;

namespace NTierEf7.Web.Controllers
{
    public class ProductsController : ApiController
    {
        // GET api/products
        [ResponseType(typeof(IEnumerable<Product>))]
        public async Task<IHttpActionResult> Get()
        {
            using (var dbContext = new NorthwindSlimContext())
            {
                var products = await dbContext.Products
                    .Include(p => p.Category)
                    .OrderBy(p => p.ProductName)
                    .ToListAsync();
                return Ok(products);
            }
        }
    }
}
