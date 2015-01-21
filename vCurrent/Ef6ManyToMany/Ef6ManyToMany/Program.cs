// NOTE: This code uses the NorthwindSlim database, which can be found here:
// http://bit.ly/northwindslim

namespace Ef6ManyToMany
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            const int EmployeeId = 1;
            const string TerritoryId = "02116";

            Console.WriteLine("Press Enter to get an employee with territories");
            Console.ReadLine();
            var employee = GetEmployee(EmployeeId);
            PrintEmployee(employee);

            Console.WriteLine("\nAdd a territory. Use OSM {Y/N}?");
            bool useObjectStateManager = Console.ReadLine().ToUpper() == "Y";
            var territory = GetTerritory(TerritoryId);
            AddTerritory(employee, territory, useObjectStateManager);
            employee = GetEmployee(EmployeeId);
            PrintEmployee(employee);

            Console.WriteLine("\nRemove a territory. Use OSM {Y/N}?");
            useObjectStateManager = Console.ReadLine().ToUpper() == "Y";
            RemoveTerritory(employee, TerritoryId, useObjectStateManager);
            employee = GetEmployee(EmployeeId);
            PrintEmployee(employee);

            Console.WriteLine("\nPress Enter to exit");
            Console.ReadLine();
        }

        static void AddTerritory(Employee employee, Territory territory, bool useObjectStateManager)
        {
            using (var context = new NorthwindSlim())
            {
                // Log sql
                context.Database.Log = Console.Write;

                // Attach employee
                context.Employees.Attach(employee);

                if (!useObjectStateManager)
                {
                    // Add territory to nav prop collection
                    employee.Territories.Add(territory);
                    Debug.Assert(context.Entry(territory).State == EntityState.Added);
                }
                else
                {
                    // Attach territory then set relationship state to added
                    context.Territories.Attach(territory);
                    var osm = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager;
                    osm.ChangeRelationshipState(employee, territory, "Territories", EntityState.Added);
                }

                try
                {
                    // Save changes
                    context.SaveChanges();
                }
                catch (DbUpdateException updateEx)
                {
                    Console.WriteLine(updateEx.InnerException.InnerException.Message);
                    employee.Territories.Remove(territory);
                }
            }
        }

        static void RemoveTerritory(Employee employee, string territoryId, bool useObjectStateManager)
        {
            using (var context = new NorthwindSlim())
            {
                // Log sql
                context.Database.Log = Console.Write;

                // Attach employee
                context.Employees.Attach(employee);
                var territory = employee.Territories.Single(t => t.TerritoryId == territoryId);

                if (!useObjectStateManager)
                {
                    // Add territory to nav prop collection
                    employee.Territories.Remove(territory);
                    Debug.Assert(context.Entry(territory).State == EntityState.Deleted);
                }
                else
                {
                    // Attach territory then set relationship state to deleted
                    var osm = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager;
                    osm.ChangeRelationshipState(employee, territory, "Territories", EntityState.Deleted);
                }

                try
                {
                    // Save changes
                    context.SaveChanges();
                }
                catch (DbUpdateException updateEx)
                {
                    Console.WriteLine(updateEx.InnerException.InnerException.Message);
                    employee.Territories.Remove(territory);
                }
            }
        }

        static Employee GetEmployee(int employeeId)
        {
            using (var context = new NorthwindSlim())
            {
                var employee = context.Employees
                    .Include(e => e.Territories)
                    .Single(e => e.EmployeeId == employeeId);
                return employee;
            }
        }

        static Territory GetTerritory(string territoryId)
        {
            using (var context = new NorthwindSlim())
            {
                var territory = context.Territories.Single(t => t.TerritoryId == territoryId);
                return territory;
            }
        }

        private static void PrintEmployee(Employee employee)
        {
            Console.WriteLine("\n{0} {1}", employee.FirstName, employee.LastName);
            foreach (var territory in employee.Territories)
            {
                Console.WriteLine("\t{0}", territory.TerritoryDescription);
            }
        }
    }
}
