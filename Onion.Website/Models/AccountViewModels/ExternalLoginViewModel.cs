using System.ComponentModel.DataAnnotations;

namespace Onion.Website.Models.AccountViewModels
{
	public class ExternalLoginViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}