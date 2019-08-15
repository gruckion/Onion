using Microsoft.EntityFrameworkCore;
using Onion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.Repositories
{
	public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
		where TEntity : BaseEntity
	{
		protected readonly DbContext context;
		protected readonly DbSet<TEntity> dbSet;
		private bool _disposed = false;

		protected GenericRepository(DbContext context)
		{
			this.context = context;
			dbSet = context.Set<TEntity>();
		}

		public IEnumerable<TEntity> FindBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
		{
			IEnumerable<TEntity> query = dbSet.Where(predicate).AsEnumerable();
			return query;
		}

		public void Insert(TEntity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("Cannot insert Null Entity");
			}

			dbSet.Add(entity);
		}

		public IEnumerable<TEntity> GetAll()
		{
			//TODO might want as IQueryable as we can do more processing on it?
			//Is it the correct place to use IQueryable?
			return dbSet.AsEnumerable();
		}

		public async Task<List<TEntity>> GetAllAsync()
		{
			return await dbSet.ToListAsync();
		}

		public void Update(TEntity entity)
		{
			//TODO check if this is right.
			if (entity == null)
			{
				throw new ArgumentNullException("Cannot insert Null Entity");
			}

			dbSet.Update(entity);
		}

		public void Delete(TEntity entity)
		{
			dbSet.Remove(entity);
		}

		//TODO figure out when this should be called.
		//Since we are not calling it in the implemented CRUD operations
		public void SaveChanges()
		{
			context.SaveChanges();
		}

		//TODO figure out if the Generic repository should be in charge of disposing its self
		//https://stackoverflow.com/questions/8703726/why-would-i-want-to-use-unitofwork-with-repository-pattern
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					context?.Dispose();
				}
			}
			this._disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}