using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Onion.Website.Controllers;
using Xunit;

namespace Onion.UnitTests.Feature.Home
{
	public class HomeControllerShould
	{
		[Fact]
		public void ReturnHomePage()
		{
			var controller = new HomeController();

			var result = controller.Index();

			result.Should().BeOfType<ViewResult>();
		}
	}
}
