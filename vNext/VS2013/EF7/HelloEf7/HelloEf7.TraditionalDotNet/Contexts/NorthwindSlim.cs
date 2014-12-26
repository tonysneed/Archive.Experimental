using System;
using System.Diagnostics;
using HelloEf7.TraditionalDotNet.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Query;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.Logging;

namespace HelloEf7.TraditionalDotNet.Contexts
{
    public partial class NorthwindSlim : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }

        public NorthwindSlim() { }

        public NorthwindSlim(IServiceProvider serviceProvider) : base(serviceProvider) { }

        protected override void OnConfiguring(DbContextOptions options)
        {
            options.UseSqlServer(@"data source=.\sqlexpress;initial catalog=NorthwindSlim;integrated security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //foreach (var entityType in modelBuilder.Metadata.EntityTypes)
            //    Console.WriteLine(entityType.SimpleName);
            
            //foreach (var convention in modelBuilder.EntityTypeConventions)
            //    Console.WriteLine(convention);
        }
    }
}
