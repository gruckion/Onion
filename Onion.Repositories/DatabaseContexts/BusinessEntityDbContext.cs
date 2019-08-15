using Microsoft.EntityFrameworkCore;
using Onion.Data;
using Onion.Repositories.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Onion.Repositories.DatabaseContexts
{
	public class BusinessEntityDbContext : DbContext
	{
		private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

		public BusinessEntityDbContext(DbContextOptions<BusinessEntityDbContext> options) : base(options)
		{
			//This constructor is for the factory IDesignTimeDbContextFactory<BusinessEntityDbContext>
			//which does not need access to the principle accessor
		}

		public BusinessEntityDbContext(DbContextOptions<BusinessEntityDbContext> options, ICurrentPrincipalAccessor currentPrincipalAccessor) : base(options)
		{
			_currentPrincipalAccessor = currentPrincipalAccessor;
		}

		//TODO split these sub DbSets into their own DbContext files which extend the BusinessEntity
		//Add more DbSets into the main BusinessentityDbContext
		public DbSet<Book> Books { get; set; }

		/// <summary>
		/// Initialize the books mapping
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var bookMap = new BookMap(modelBuilder.Entity<Book>());
			base.OnModelCreating(modelBuilder);
		}

		/// <summary>
		/// Ensure that audit information is applied to auditable entities when saving changes
		/// </summary>
		/// <param name="acceptAllChangesOnSuccess"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (_currentPrincipalAccessor != null)
				ChangeTracker.ApplyAuditInformation(_currentPrincipalAccessor);

			return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}