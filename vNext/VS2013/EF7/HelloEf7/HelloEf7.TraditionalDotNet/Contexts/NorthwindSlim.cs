using HelloEf7.TraditionalDotNet.Models;
using Microsoft.Data.Entity;

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
    }
}
