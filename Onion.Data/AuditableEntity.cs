using System;
using System.ComponentModel.DataAnnotations;

namespace Onion.Data
{
	public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
	{
		/// <summary>
		/// The creation date for the auditable entry
		/// </summary>
		[ScaffoldColumn(false)]
		public DateTimeOffset CreatedDate { get; set; }

		/// <summary>
		/// The modified date for the auditable entry
		/// </summary>
		[ScaffoldColumn(false)]
		public DateTimeOffset UpdatedDate { get; set; }

		/// <summary>
		/// The name of the user this entry was created by
		/// </summary>
		[ScaffoldColumn(false)]
		public string CreatedBy { get; set; }

		/// <summary>
		/// The name of the user this entry was updated by
		/// </summary>
		[ScaffoldColumn(false)]
		public string UpdatedBy { get; set; }

	}
}