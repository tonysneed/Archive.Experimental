using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HelloAspNet5.Web.Data;
using Microsoft.AspNet.Mvc;

namespace HelloAspNet5.Web.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly NorthwindSlimContext _dbContext;
        public ProductsController(NorthwindSlimContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/products
        public async Task<IActionResult> Get()
        {
            var products = await _dbContext.Products
                //.Include(p => p.Category) // not implemented in beta1
                .OrderBy(p => p.ProductName)
                .ToListAsync();
            return Ok(products);
        }
    }
}
