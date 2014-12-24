using System.Collections.Generic;

namespace NTierEf7.Web.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int? CategoryId { get; set; }

        public decimal? UnitPrice { get; set; }

        public bool Discontinued { get; set; }

        public byte[] RowVersion { get; set; }

        public Category Category { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
