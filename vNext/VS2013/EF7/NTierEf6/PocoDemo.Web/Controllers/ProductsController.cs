using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PocoDemo.Data;

namespace PocoDemo.Web.Controllers
{
    public class ProductsController : ApiController
    {
        // GET api/products
        [ResponseType(typeof(IEnumerable<Product>))]
        public async Task<IHttpActionResult> Get()
        {
            using (var dbContext = new NorthwindSlim())
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
