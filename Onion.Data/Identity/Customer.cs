namespace Onion.Data.Identity
{
	public class Customer : Entity<int>
	{
		public string IdentityId { get; set; }
		public AppUser Identity { get; set; }  // navigation property
		public string Location { get; set; }
		public string Locale { get; set; }
		public string Gender { get; set; }
	}
}
