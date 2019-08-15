namespace Onion.Data
{
	/// <summary>
	/// The BaseEntity for generic where statements for generics.
	/// In order of increasing speciality BaseEntity, Entity, AuditableEntity,
	/// </summary>
	public abstract class BaseEntity
	{
	}

	public abstract class Entity<T> : BaseEntity, IEntity<T>
	{
		/// <summary>
		/// The id for the entity
		/// </summary>
		public virtual T Id { get; set; }
	}
}