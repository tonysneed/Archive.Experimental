using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloAspNet5.Web
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        static readonly List<string> Items = new List<string>
        {
            "value1", "value2", "value3"
        };

        [HttpGet]
        public List<string> GetAll()
        {
            return Items;
        }
    }
}
