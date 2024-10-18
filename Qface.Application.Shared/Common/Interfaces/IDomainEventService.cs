using Qsmart.EventBus.Shared.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qface.Application.Shared.Common.Interfaces
{
    public interface ICustomHostingEnvironment
    {
        public string WebRootPath { get; }
        public string ContentRootPath { get; }
        public string ContentVirtualRootPath { get; }
        public string BaseUrl { get; }
    }
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
