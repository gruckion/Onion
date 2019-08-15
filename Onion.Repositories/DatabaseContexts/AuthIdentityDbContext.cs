using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Onion.Data.Identity;

namespace Onion.Repositories.DatabaseContexts
{
	public class AuthIdentityDbContext : IdentityDbContext<AppUser>
	{
		//these are not roles but rather tables containing additional information
		//about users assigned to specific roles

		public DbSet<Customer> Customers { get; set; }

		public AuthIdentityDbContext(DbContextOptions<AuthIdentityDbContext> options)
			: base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.EnableSensitiveDataLogging();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}
	}
}