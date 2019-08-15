using FluentValidation.Attributes;
using Onion.WebApi.ViewModels.Validations;

namespace Onion.WebApi.ViewModels
{
	[Validator(typeof(CredentialsViewModelValidator))]
	public class CredentialsViewModel
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
