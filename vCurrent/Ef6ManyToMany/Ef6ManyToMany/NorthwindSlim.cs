namespace Ef6ManyToMany
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NorthwindSlim : DbContext
    {
        public NorthwindSlim()
            : base("name=NorthwindSlim")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Territories)
                .WithMany(e => e.Employees)
                .Map(m => m.ToTable("EmployeeTerritories").MapLeftKey("EmployeeId").MapRightKey("TerritoryId"));
        }
    }
}
