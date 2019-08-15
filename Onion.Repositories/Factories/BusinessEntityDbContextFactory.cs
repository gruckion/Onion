using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Onion.Repositories.Configuration;
using Onion.Repositories.DatabaseContexts;

namespace Onion.Repositories.Factories
{
	/// <summary>
	/// This factory is provided so that the EF Core tools can build a full context
	///without having to have access to where the DbContext is being created(i.e. in the UI layer).
	/// </summary>
	/// <remarks>
	/// Please see the following URL for more information:
	/// https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext
	/// </remarks>
	public class BusinessEntityDbContextFactory : IDesignTimeDbContextFactory<BusinessEntityDbContext>
	{
		public static string DataConnectionString => new DatabaseConfiguration().GetBusinessEntityDbConnectionString();

		public BusinessEntityDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<BusinessEntityDbContext>();
			optionsBuilder.UseSqlServer(DataConnectionString);
			return new BusinessEntityDbContext(optionsBuilder.Options);
		}
	}
}