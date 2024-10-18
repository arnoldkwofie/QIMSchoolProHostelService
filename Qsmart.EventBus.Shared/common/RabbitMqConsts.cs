using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qsmart.EventBus.Shared.common
{
    public enum DomainEventAction
    {
        OnCreate, OnUpdate, OnDelete
    }
    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }
        public Guid Id { get; protected set; }
        public DomainEventAction Action { get; protected set; }

        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
    public static class RabbitMqConsts
    {
        public const string AlertNewAccountQueue = "alert-new-account-queue";
        public const string NotificationServiceQueue = "notification.service";

        public const string UserName = "guest";
        public const string Password = "guest";

        public static string HOST_URL => "rabbitmq://localhost";
        public static string HOST => $"amqp://{UserName}:{Password}@localhost:5672";

    }

}
