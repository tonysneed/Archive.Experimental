using System.Collections.Generic;
using System.Linq;
using HelloAspNet5.Web.Data;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloAspNet5.Web
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly NorthwindSlimContext _dbContext;

        public CustomersController(NorthwindSlimContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _dbContext.Customers
                .OrderBy(p => p.CustomerId)
                .ToList();
            return new ObjectResult(products);
        }
    }
}
