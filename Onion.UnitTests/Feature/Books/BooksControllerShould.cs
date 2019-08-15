using Microsoft.AspNetCore.Mvc;
using Moq;
using Onion.Data;
using Onion.Services;
using Onion.Website.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Onion.UnitTests.Feature.Books
{
	public class BooksControllerShould
	{

		private readonly Mock<IBookService> _bookServiceMock;
		private BooksController _bookController;

		//Initialize
		public BooksControllerShould()
		{
			_bookServiceMock = new Mock<IBookService>();
		}


		[Fact]
		public async void VerifyAsyncIndexViewType()
		{
			// Arrange
			_bookController = new BooksController(_bookServiceMock.Object);
			// Act
			var result = await _bookController.Index();
			// Assert
			Assert.IsType<ViewResult>(result);
		}

		[Fact]
		public async void VerifyAsyncIndexBookListCount()
		{
			// Arrange
			_bookServiceMock.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(
				new List<Book>() { new Book() }
			));
			// Act
			_bookController = new BooksController(_bookServiceMock.Object);

			var result = await _bookController.Index();

			var viewResult = Assert.IsType<ViewResult>(result);
			// Assert
			var model = Assert.IsType<List<Book>>(viewResult.ViewData.Model);

			Assert.Single(model);
		}

		[Fact]
		public async void VerifyAsyncIndexBookPropertiesAreSet()
		{
			//Arrange
			Book book = new Book()
			{
				Ordinal = 1,
				Name = "name",
				Description = "description",
				Author = "author"
			};

			_bookServiceMock.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(
				new List<Book>() { book }
			));
			//Act
			_bookController = new BooksController(_bookServiceMock.Object);

			var result = await _bookController.Index();

			var viewResult = Assert.IsType<ViewResult>(result);
			//Assert
			var model = Assert.IsType<List<Book>>(viewResult.ViewData.Model);

			Assert.Equal(book, model[0]);
		}

		[Fact]
		public async void VerifyAsyncDetailByIndexBookObjectIsFound()
		{
			//Arrange
			Book book = new Book()
			{
				Ordinal = 1,
				Name = "name",
				Description = "description",
				Author = "author"
			};

			_bookServiceMock.Setup(x => x.GetAsyncById(0)).Returns(Task.FromResult(book));

			_bookController = new BooksController(_bookServiceMock.Object);
			//Act
			var result = await _bookController.Details(0);

			//Assert
			var viewResult = Assert.IsType<ViewResult>(result);

			var model = Assert.IsType<Book>(viewResult.ViewData.Model);

			Assert.Equal(book, model);
		}

		[Fact]
		public async void VerifyAsyncDetailByIndexBookObjectIsNotFound()
		{
			//Arrange
			Book book = new Book();

			_bookServiceMock.Setup(x => x.GetAsyncById(0)).Returns(Task.FromResult(book));

			_bookController = new BooksController(_bookServiceMock.Object);
			//Act
			var result = await _bookController.Details(1);

			//Assert
			Assert.IsType<NotFoundResult>(result);
		}

	}
}