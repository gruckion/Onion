using Onion.Repositories.DatabaseContexts;
using System;

namespace Onion.Repositories
{
	/// <summary>
	/// The Entity Framework implementation of IUnitOfWork
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// The DbContext
		/// </summary>
		private readonly BusinessEntityDbContext _dbContext;

		/// <summary>
		/// The book repository instance
		/// </summary>
		private IBookRepository _bookRepository;

		public IBookRepository BookRepository
		{
			get
			{
				if (this._bookRepository == null)
				{
					this._bookRepository = new BookRepository(_dbContext);
				}
				return _bookRepository;
			}
		}


		/// <summary>
		/// Initializes a new instance of the UnitOfWork class.
		/// </summary>
		/// <param name="dbContext"></param>
		public UnitOfWork(BusinessEntityDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public int Commit()
		{
			return _dbContext.SaveChanges();
		}

		/// <summary>
		/// Disposes all external resources.
		/// </summary>
		/// <param name="disposing">The dispose indicator.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
				return;

			if (_dbContext == null)
				return;

			_dbContext.Dispose();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}