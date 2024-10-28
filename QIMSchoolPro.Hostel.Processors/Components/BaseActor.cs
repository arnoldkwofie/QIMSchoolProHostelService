using Akka.Actor;
using Akka.DI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Components
{
    public class BaseActor : ReceiveActor
    {
        protected void CreateAndTellActor<TActor>(object message)
            where TActor : BaseActor
        {
            var actorRef = Context.ActorOf(Context.DI()
                    .Props<TActor>()
                    .WithSupervisorStrategy(TopLevelActors.GetChildDefaultSupervisorStrategy),
                $"{Guid.NewGuid()}-actor");
            actorRef.Tell(message);
            actorRef.Tell(PoisonPill.Instance);
        }
        protected void Publish(object @event)
        {
            Context.Dispatcher.EventStream.Publish(@event);
        }
    }
}
