using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onion.Data.Identity;
using Onion.Repositories.DatabaseContexts;
using Onion.WebApi.Helpers;
using Onion.WebApi.ViewModels;
using System;
using System.Threading.Tasks;

namespace Onion.WebApi.Controllers
{
	[Route("api/[controller]")]
	public class AccountsController : Controller
	{
		private readonly AuthIdentityDbContext _authIdentityDbContext;
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;

		public AccountsController(UserManager<AppUser> userManager, IMapper mapper, AuthIdentityDbContext appDbContext)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_authIdentityDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
		}

		// POST api/accounts
		[HttpPost]
		public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userIdentity = _mapper.Map<AppUser>(model);

			var result = await _userManager.CreateAsync(userIdentity, model.Password);

			if (!result.Succeeded)
				return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

			await _authIdentityDbContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
			await _authIdentityDbContext.SaveChangesAsync();

			return new OkObjectResult("Account created");
		}
	}
}