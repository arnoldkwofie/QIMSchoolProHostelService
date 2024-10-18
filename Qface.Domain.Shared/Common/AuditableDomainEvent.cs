using Qsmart.EventBus.Shared.common;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qface.Domain.Shared.Common
{
	public abstract class AuditableDomainEvent
	{
		[NotMapped]
		private readonly ConcurrentQueue<DomainEvent> _domainEvents = new ConcurrentQueue<DomainEvent>();
		public IProducerConsumerCollection<DomainEvent> DomainEvents => _domainEvents;

		protected void AddEvent(DomainEvent eventItem)
		{
			_domainEvents.Enqueue(eventItem);
		}

		protected void RemoveEvent(DomainEvent eventItem)
		{
			//_domainEvents.TryDequeue(eventItem);
		}

		public void ClearDomainEvents()
		{
			_domainEvents?.Clear();
		}
		//protected Guid NewIdGuid()
		//{
		//    return MassTransit.NewId.NextGuid();
		//}
	}




    public class DomainEventNotification<TDomainEvent> 
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;

        }

        public TDomainEvent DomainEvent { get; }
    }

}
