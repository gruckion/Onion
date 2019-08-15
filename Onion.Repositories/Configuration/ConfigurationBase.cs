using Microsoft.Extensions.Configuration;
using System;

namespace Onion.Repositories.Configuration
{
	public abstract class ConfigurationBase
	{
		protected IConfigurationRoot GetConfigurationRoot()
		{
			return new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				//.AddXmlFile("appsettings.xml")
				.AddJsonFile("appsettings.json")
				.Build();
		}

		protected void RaiseValueNotFoundException(string configurationKey)
		{
			//TODO move this ConfigurationBase Exception into it's own class
			throw new Exception($"appsettings key ({configurationKey}) could not be found.");
		}
	}
}