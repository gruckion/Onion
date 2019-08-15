using Onion.Data;
using System.Threading.Tasks;

namespace Onion.Repositories
{
	public interface IBookRepository : IGenericRepository<Book>
	{
		Book GetById(int id);

		Task<Book> GetAsyncById(int id);
	}
}