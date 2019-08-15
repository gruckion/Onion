using Microsoft.AspNetCore.Identity;

namespace Onion.Data.Identity
{
	/// <summary>
	/// Adding additional properties to user
	/// </summary>
	public class AppUser : IdentityUser
	{
		// Extended Properties
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public long? FacebookId { get; set; }
		public string PictureUrl { get; set; }
	}
}
