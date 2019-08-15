using System.Security.Claims;

namespace Onion.Repositories
{
	public interface ICurrentPrincipalAccessor
	{
		ClaimsPrincipal CurrentPrincipal { get; }
	}
}