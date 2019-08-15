using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Onion.Repositories
{
	public class HttpContextCurrentPrincipalAccessor : ICurrentPrincipalAccessor
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HttpContextCurrentPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public ClaimsPrincipal CurrentPrincipal => _httpContextAccessor.HttpContext.User;
	}
}