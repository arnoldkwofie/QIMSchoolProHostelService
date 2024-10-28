using Akka.Actor;
using Akka.DI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Components
{
    public class TopLevelActors
    {
        public static IActorRef RoomUpdateSubscriberActor = ActorRefs.Nobody;
      
        public static ActorSystem ActorSystem;

        public static IActorRef GetActorInstance<T>(string name) where T : ActorBase
        {
            return ActorSystem.ActorOf(ActorSystem.DI()
                    .Props<T>()
                    .WithSupervisorStrategy(GetDefaultSupervisorStrategy)
                , name);
        }
        public static SupervisorStrategy GetChildDefaultSupervisorStrategy => new OneForOneStrategy(3,
            TimeSpan.FromSeconds(3),
            ex =>
            {
                if (!(ex is ActorInitializationException)) return Directive.Resume;
                return Directive.Stop;
            });
        public static SupervisorStrategy GetDefaultSupervisorStrategy => new OneForOneStrategy(3,
            TimeSpan.FromSeconds(3),
            ex =>
            {
                if (!(ex is ActorInitializationException)) return Directive.Resume;
                Stop();
                return Directive.Stop;
            });

        public static SupervisorStrategy GetDefaultChildSupervisorStrategy => new OneForOneStrategy(3,
            TimeSpan.FromSeconds(3),
            ex =>
            {
                if (!(ex is ActorInitializationException)) return Directive.Resume;
                return Directive.Stop;
            });

        /// <summary>
        /// This method stops the actor system
        /// </summary>
        private static void Stop()
        {
            ActorSystem?.Terminate().Wait(1000);
        }
    }
}
