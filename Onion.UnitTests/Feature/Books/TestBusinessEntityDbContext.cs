using Microsoft.EntityFrameworkCore;
using Onion.Data;
using System.Collections.Generic;

namespace Onion.UnitTests.Feature.Books
{
	//TODO remove this or use it to test the repo
	public class TestBusinessEntityDbContext : DbContext
	{
		public DbSet<Book> Books { get; set; }

		public TestBusinessEntityDbContext()
		{

		}

		public void Seed(TestBusinessEntityDbContext testBusinessEntityDbContext)
		{
			var listBooks = new List<Book>()
			{
				new Book() {Ordinal = 1, Name = "Harry Potter and the Philosopher's Stone", Description = "Harry Potter and the Philosopher\'s Stone is a fantasy novel written by British author J. K. Rowling. It is the first novel in the Harry Potter series and Rowling\'s debut novel, first published in 1997 by Bloomsbury.", Author = "J. K. Rowling"},
				new Book() {Ordinal = 2, Name = "Harry Potter and the Chamber of Secrets", Description = "Harry Potter and the Chamber of Secrets is a fantasy novel written by British author J. K. Rowling and the second novel in the Harry Potter series.", Author = "J. K. Rowling"}
			};

			testBusinessEntityDbContext.Books.AddRange(listBooks);
			testBusinessEntityDbContext.SaveChanges();
		}


		//TODO
		public TestBusinessEntityDbContext(bool enableLazyLoading, bool enableProxyCreation)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var bookMap = new BookMap(modelBuilder.Entity<Book>());
			base.OnModelCreating(modelBuilder);
		}
	}
}
