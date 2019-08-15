using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Onion.WebApi.Auth;
using Onion.WebApi.Auth.Auth;
using Onion.WebApi.Extensions;
using System.Net;
using Onion.Repositories;

namespace Onion.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext();

			services.AddUnitOfWork();

			services.AddRepository();

			services.AddTransientServices();

			services.AddSingleton<IJwtFactory, JwtFactory>();

			services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

			services.AddJwtConfiguration(Configuration);

			services.AddIdentity();

			services.AddAutoMapper();

			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

			//Gets current user for the auditable information
			services.AddSingleton<ICurrentPrincipalAccessor, HttpContextCurrentPrincipalAccessor>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseHsts();
			}
			app.UseExceptionHandler(
				builder =>
				{
					builder.Run(
						async context =>
						{
							context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
							context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

							var error = context.Features.Get<IExceptionHandlerFeature>();
							if (error != null)
							{
								context.Response.AddApplicationError(error.Error.Message);
								await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
							}
						});
				});

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseMvc();
		}
	}
}
