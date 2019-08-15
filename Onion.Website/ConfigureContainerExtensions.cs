using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Onion.Data.Identity;
using Onion.Repositories;
using Onion.Repositories.Configuration;
using Onion.Repositories.DatabaseContexts;
using Onion.Services;

namespace Onion.Website
{
	public static class ConfigureContainerExtensions
	{
		public static void AddDbContext(this IServiceCollection serviceCollection,
			string businessEntitiesConnectionString = null, string authConnectionString = null)
		{
			serviceCollection.AddDbContext<BusinessEntityDbContext>(options =>
				options.UseSqlServer(businessEntitiesConnectionString ?? GetBusinessEntitiesConnectionStringFromConfig()));

			serviceCollection.AddDbContext<AuthIdentityDbContext>(options =>
				options.UseSqlServer(authConnectionString ?? GetAuthConnectionStringFromConfig()));

		}

		public static void AddCookiePolicy(this IServiceCollection serviceCollection)
		{
			serviceCollection.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
		}

		public static void AddIdentity(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddIdentity<AppUser, IdentityRole>(options =>
				{
					options.Stores.MaxLengthForKeys = 128;
					options.Password.RequireNonAlphanumeric = false;
				})
				.AddEntityFrameworkStores<AuthIdentityDbContext>()
				.AddDefaultUI()
				.AddDefaultTokenProviders();
		}

		public static void AddRepository(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IBookRepository, BookRepository>();
		}

		public static void AddTransientServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IBookService, BookService>();
			serviceCollection.AddTransient<IEmailSender, EmailSender>();
		}

		public static void AddUnitOfWork(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
		}

		private static string GetBusinessEntitiesConnectionStringFromConfig()
		{
			return new DatabaseConfiguration().GetBusinessEntityDbConnectionString();
		}

		private static string GetAuthConnectionStringFromConfig()
		{
			return new DatabaseConfiguration().GetAuthConnectionString();
		}
	}
}