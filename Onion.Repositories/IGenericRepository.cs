using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Onion.Repositories
{
	//TODO rename this to be IGenericRepository
	//TODO change the genetic entity to be of type BaseEntity
	public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
	{
		IEnumerable<TEntity> GetAll();

		IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

		//TODO determine if we wish to return the entity when Inserting
		void Insert(TEntity entity);

		void Update(TEntity entity);

		//TODO determine if we wish to return the entity when Deleting
		void Delete(TEntity entity);

		void SaveChanges();

		Task<List<TEntity>> GetAllAsync();
	}
}