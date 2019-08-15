using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Onion.Data
{
	public class BookMap
	{
		public BookMap(EntityTypeBuilder<Book> entityTypeBuilder)
		{
			// Sets the primary Key for the table to be Id
			entityTypeBuilder.HasKey(b => b.Id);
			// Sets the Relational database table to be Books
			entityTypeBuilder.ToTable("Books");
		}
	}
}