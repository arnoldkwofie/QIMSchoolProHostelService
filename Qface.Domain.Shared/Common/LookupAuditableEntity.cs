namespace Qface.Domain.Shared.Common
{
	public abstract class LookupAuditableEntity : AuditableEntity
	{
		public string Name { get; set; }
		public bool IsSystemDefault { get; set; }
		public string Description { get; set; }

	}
	public abstract class LookupAuditableEntity<T> : AuditableEntity<T>
	{

		public string Name { get; set; }
		public bool IsSystemDefault { get; set; }
		public string? Description { get; set; }

	}

}
