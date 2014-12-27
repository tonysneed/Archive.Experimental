using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HelloAspNet5.Web.Data;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloAspNet5.Web.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly NorthwindSlimContext _dbContext;

        public CustomersController(NorthwindSlimContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Get()
        {
            var products = await _dbContext.Customers
                .OrderBy(p => p.CustomerId)
                .ToListAsync();
            return Ok(products);
        }
    }
}
