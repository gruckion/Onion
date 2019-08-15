using System.ComponentModel.DataAnnotations;

namespace Onion.Data
{
	public class Book : AuditableEntity<int>
	{
		/// <summary>
		/// The order of the books in a series
		/// </summary>
		public int Ordinal { get; set; }

		/// <summary>
		/// The name of the book entry
		/// </summary>
		[Required]
		[MaxLength(100)]
		[Display(Name = "Book Name")]
		public string Name { get; set; }

		/// <summary>
		/// The description of the book entry
		/// </summary>
		[Required]
		[MaxLength(250)]
		[Display(Name = "Book Description")]
		public string Description { get; set; }

		/// <summary>
		/// The author of the book
		/// </summary>
		[Required]
		[MaxLength(250)]
		[Display(Name = "Author")]
		public string Author { get; set; }

	}
}