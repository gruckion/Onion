using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Onion.Data.Identity;
using Onion.Repositories;
using Onion.Repositories.Configuration;
using Onion.Repositories.DatabaseContexts;
using Onion.Services;
using Onion.WebApi.Helpers;
using Onion.WebApi.Models;
using System;
using System.Text;

namespace Onion.WebApi
{
	public static class ConfigureContainerExtensions
	{
		private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
		private static readonly SymmetricSecurityKey SigningKey =
			new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));


		public static void AddDbContext(this IServiceCollection serviceCollection,
			string businessEntitiesConnectionString = null, string authConnectionString = null)
		{
			serviceCollection.AddDbContext<BusinessEntityDbContext>(options =>
				options.UseSqlServer(businessEntitiesConnectionString ?? GetBusinessEntitiesConnectionStringFromConfig()));

			serviceCollection.AddDbContext<AuthIdentityDbContext>(options =>
				options.UseSqlServer(authConnectionString ?? GetAuthConnectionStringFromConfig()));
		}

		public static void AddIdentity(this IServiceCollection serviceCollection)
		{
			//serviceCollection.AddIdentity<AppUser, IdentityRole>(options =>
			//	{
			//		options.Stores.MaxLengthForKeys = 128;
			//		options.Password.RequireNonAlphanumeric = false;
			//		//configure identity options
			//		options.Password.RequireDigit = false;
			//		options.Password.RequireLowercase = false;
			//		options.Password.RequireUppercase = false;
			//		options.Password.RequireNonAlphanumeric = false;
			//		options.Password.RequiredLength = 6;
			//	})
			//	.AddEntityFrameworkStores<AuthIdentityDbContext>()
			//	.AddDefaultTokenProviders();


			var builder = serviceCollection.AddIdentityCore<AppUser>(options =>
			{
				options.Stores.MaxLengthForKeys = 128;
				options.Password.RequireNonAlphanumeric = false;
				// configure identity options
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 6;
			});
			builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
			builder.AddEntityFrameworkStores<AuthIdentityDbContext>()
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

		public static void AddJwtConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			// jwt wire up
			// Get options from app settings
			var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

			// Configure JwtIssuerOptions
			serviceCollection.Configure<JwtIssuerOptions>(options =>
			{
				options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
				options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
				options.SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
			});

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

				ValidateAudience = true,
				ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

				ValidateIssuerSigningKey = true,
				IssuerSigningKey = SigningKey,

				RequireExpirationTime = false,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};

			serviceCollection.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(configureOptions =>
			{
				configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
				configureOptions.TokenValidationParameters = tokenValidationParameters;
				configureOptions.SaveToken = true;
			});

			// api user claim policy
			serviceCollection.AddAuthorization(options =>
			{
				options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
			});
		}
	}
}