using Microsoft.EntityFrameworkCore;
using Onion.Data;
using Onion.Repositories.DatabaseContexts;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.Repositories
{
	//public class BookRepository<T> : IRepository<T> where T : BaseAuditClass
	public class BookRepository : GenericRepository<Book>, IBookRepository
	{
		//TODO move the errorMessage into the GenericRepoistory
		private string errorMessage = string.Empty;

		public BookRepository(BusinessEntityDbContext context) : base(context)
		{
		}

		public Book GetById(int id)
		{
			return FindBy(x => x.Id == id).FirstOrDefault();
		}

		public async Task<Book> GetAsyncById(int id)
		{
			return await dbSet.SingleOrDefaultAsync(m => m.Id == id);
		}

	}
}