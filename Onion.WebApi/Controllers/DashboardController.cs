using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onion.Data.Identity;
using Onion.Repositories.DatabaseContexts;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Onion.WebApi.Controllers
{
	[Authorize(Policy = "ApiUser")]
	[Route("api/[controller]/[action]")]
	public class DashboardController : Controller
	{
		private readonly ClaimsPrincipal _caller;
		private readonly AuthIdentityDbContext _appDbContext;

		public DashboardController(UserManager<AppUser> userManager, AuthIdentityDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
		{

			_caller = httpContextAccessor.HttpContext.User ?? throw new ArgumentNullException(nameof(httpContextAccessor.HttpContext.User));
			_appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
		}

		// GET api/dashboard/home
		[HttpGet]
		public async Task<IActionResult> Home()
		{
			// retrieve the user info
			//HttpContext.User
			var userId = _caller.Claims.Single(c => c.Type == "id");
			var customer = await _appDbContext.Customers.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

			return new OkObjectResult(new
			{
				Message = "This is secure API and user data!",
				customer.Identity.FirstName,
				customer.Identity.LastName,
				customer.Identity.PictureUrl,
				customer.Identity.FacebookId,
				customer.Identity.PhoneNumber,
				customer.Identity.TwoFactorEnabled,
				customer.Identity.AccessFailedCount,
				customer.Identity.ConcurrencyStamp,
				customer.Identity.Email,
				customer.Identity.EmailConfirmed,
				customer.Identity.Id,
				customer.Identity.LockoutEnabled,
				customer.Identity.LockoutEnd,
				customer.Identity.NormalizedEmail,
				customer.Identity.NormalizedUserName,
				customer.Identity.PhoneNumberConfirmed,
				customer.Location,
				customer.Locale,
				customer.Gender
			});
		}
	}
}
