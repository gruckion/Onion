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
	public class AuthIdentityDbContextFactory : IDesignTimeDbContextFactory<AuthIdentityDbContext>
	{
		//TODO more research is required to how portable the AspNetAuth is to
		//get it to work with the Xamarin clients. Desired end result is code re-use
		//in addition to using the same database to store all our users.

		public static string DataConnectionString => new DatabaseConfiguration().GetAuthConnectionString();

		public AuthIdentityDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<AuthIdentityDbContext>();
			optionsBuilder.UseSqlServer(DataConnectionString);
			return new AuthIdentityDbContext(optionsBuilder.Options);
		}
	}
}