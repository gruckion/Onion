using Microsoft.Extensions.Configuration;

namespace Onion.Repositories.Configuration
{
	public class DatabaseConfiguration : ConfigurationBase
	{
		//TODO write a test to ensure this can be found in the json file
		//Or it causes a crash
		private readonly string _businessEntitiesConnectionKey = "onionBusinessEntitiesConnection";

		private readonly string _authConnectionKey = "onionAuthConnection";

		//TODO change this to have one function that takes in an overload parameter
		public string GetBusinessEntityDbConnectionString()
		{
			return GetConfigurationRoot().GetConnectionString(_businessEntitiesConnectionKey);
		}

		public string GetAuthConnectionString()
		{
			return GetConfigurationRoot().GetConnectionString(_authConnectionKey);
		}
	}
}