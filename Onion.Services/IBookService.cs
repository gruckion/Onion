using Onion.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onion.Services
{
	//A part of me wants this to be generic, yet I feel it is not necessary
	public interface IBookService
	{
		IEnumerable<Book> GetAll();

		Book Get(int id);

		void Insert(Book book);

		void Update(Book book);

		void Delete(int id);

		//public static Task<List<TSource>> ToListAsync<TSource>([NotNullAttribute] this IQueryable<TSource> source, CancellationToken cancellationToken = default(CancellationToken));

		Task<List<Book>> GetAllAsync();

		Task<Book> GetAsyncById(int id);
	}
}