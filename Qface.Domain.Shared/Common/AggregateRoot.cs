namespace Qface.Domain.Shared.Common
{
	public abstract class AggregateRoot : AuditableEntity { }
	public abstract class AggregateRoot<T> : AuditableEntity<T> { }

}
