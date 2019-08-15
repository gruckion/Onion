using Onion.Data;
using Onion.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onion.Services
{
	public class BookService : IBookService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBookRepository _bookRepository;

		public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			_bookRepository = unitOfWork.BookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
		}

		//TODO test db migration then remove this if it works fine
		/*public BookService(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));

		}*/

		#region Synchronous CRUD operations

		public void Delete(int id)
		{
			var book = _bookRepository.GetById(id);
			//_bookRepository.Delete(book);
			//_unitOfWork.Commit();
		}

		public Book Get(int id)
		{
			return _bookRepository.GetById(id);
		}

		public IEnumerable<Book> GetAll()
		{
			return _bookRepository.GetAll();
		}

		public void Insert(Book book)
		{
			_bookRepository.Insert(book);
			_unitOfWork.Commit();
		}

		public void Update(Book book)
		{
			_bookRepository.Update(book);
			_unitOfWork.Commit();
		}

		#endregion

		#region Asynchronous CRUD operations

		public async Task<List<Book>> GetAllAsync()
		{
			return await _bookRepository.GetAllAsync();
		}

		public async Task<Book> GetAsyncById(int id)
		{
			return await _bookRepository.GetAsyncById(id);
		}

		#endregion
	}
}