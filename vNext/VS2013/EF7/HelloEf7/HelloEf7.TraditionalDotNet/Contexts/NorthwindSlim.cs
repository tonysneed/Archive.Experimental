using System;
using HelloEf7.TraditionalDotNet.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Query;

namespace HelloEf7.TraditionalDotNet.Contexts
{
    public partial class NorthwindSlim : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptions options)
        {
            options.UseSqlServer(@"data source=.\sqlexpress;initial catalog=NorthwindSlim;integrated security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //foreach (var convention in modelBuilder.EntityTypeConventions)
            //    Console.WriteLine(convention);
        }
    }
}
