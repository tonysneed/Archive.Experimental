using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Data.Entity;

namespace NTierEf7.Web.Models
{
    public class NorthwindSlimContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptions options)
        {
            options.UseSqlServer(@"data source=(local)\sqlexpress;initial catalog=NorthwindSlim;integrated security=True;pooling=False;MultipleActiveResultSets=True;");
        }
    }
}