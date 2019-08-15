using System;

namespace Onion.Repositories
{
	public interface IUnitOfWork : IDisposable
	{

		IBookRepository BookRepository { get; }

		/// <summary>
		/// Saves all pending changes
		/// </summary>
		/// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
		int Commit();
	}
}