Hello EF 7
============

Using EF7 in Traditional .NET Applications:
https://github.com/aspnet/EntityFramework/wiki/Using-EF7-in-Traditional-.NET-Applications

Observations:
- Logging isn't obvious
- Data annotations seem to work
- Can use existing database
- No need to db initializer to null

1. Configure Nuget
   - Add the following package source:
     AspNet vNext Nightly Builds: https://www.myget.org/F/aspnetvnext/
   - Select the package source and execute:
     Install-Package EntityFramework.SqlServer –Pre

2. Code First
   - Add model classes
   - Add DbContext class
     + Add DbSet properties
	 + Override OnConfiguring
	   Call options.UseSqlServer, pass connection string

