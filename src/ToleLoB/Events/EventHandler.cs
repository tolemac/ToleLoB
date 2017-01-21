using System;
using System.Reflection;
using System.Linq;

namespace ToleLoB.Events
{
    public interface IEventHandler
    {
        void Run(EventBase ev);
    }

    public abstract class EventHandler : IEventHandler
    {
        public static Type GetEventTypeFromEventHandlerType(Type eventHandlerType)
        {
            var interf = eventHandlerType.GetTypeInfo().ImplementedInterfaces
                .FirstOrDefault(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>));
            return interf.GenericTypeArguments[0];
        }

        public abstract void Run(EventBase ev);
    }

    public interface IEventHandler<out TEvent> : IEventHandler
        where TEvent : EventBase
    {
    }

    public abstract class EventHandler<TEvent> : EventHandler, IEventHandler<TEvent>
        where TEvent : EventBase
    {
        public abstract void Run(TEvent ev);
        public override void Run(EventBase ev)
        {
            this.Run(ev as TEvent);
        }
    }
}