namespace PocoDemo.Data
{
    using System;
    using System.Collections.Generic;

    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public string CustomerId { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
