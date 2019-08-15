using System.ComponentModel.DataAnnotations;

namespace Onion.Website.Models.AccountViewModels
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}