using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace ToleLoB.Events
{
    public class EventBus
    {
        private IList<EventHandlerRegistration> _registrations = new List<EventHandlerRegistration>();
        public IEventHandlerRegistration RegisterHandler<TEvent>(EventHandler<TEvent> handler)
            where TEvent : EventBase
        {
            var reg = new EventHandlerRegistration(this, handler);
            _registrations.Add(reg);
            return reg;
        }
        public IEventHandlerRegistration RegisterHandler<TEventHandler>()
            where TEventHandler : IEventHandler
        {
            var reg = new EventHandlerRegistration(this, typeof(TEventHandler));
            _registrations.Add(reg);
            return reg;
        }

        public IEventHandlerRegistration RegisterHandler(Type handlerType)
        {
            var reg = new EventHandlerRegistration(this, handlerType);
            _registrations.Add(reg);
            return reg;
        }

        public void Unregister(IEventHandlerRegistration registration)
        {
            var reg = registration as EventHandlerRegistration;
            _registrations.Remove(reg);
        }

        public bool HasHandlerFor<TEvent>() where TEvent : EventBase
        {
            return GetHandlerRegistrations(typeof(TEvent)).Count() > 0;
        }

        public IList<IEventHandler<TEvent>> GetHandlersFor<TEvent>()
            where TEvent : EventBase
        {
            return GetHandlerRegistrations(typeof(TEvent))
                .Select(r => (r.HandlerInstance ?? this.CreateHandlerInstance(r.HandlerType)) as IEventHandler<TEvent>)
                .ToList();
        }

        private IEventHandler CreateHandlerInstance(Type handlerType)
        {
            return Activator.CreateInstance(handlerType) as IEventHandler;
        }

        private bool Match(EventHandlerRegistration reg, Type eventType)
        {
            // reg.EventType is assignable from EventType
            var result = reg.EventType.GetTypeInfo().IsAssignableFrom(eventType);
            if (!result)
            {
                // The two types are generics, reg.evenType generic type definition is assignable 
                // from eventType generic type definition, have the same arguments,
                // and each reg event type arguments are assignable from eventType arguments in the same index.
                if (eventType.GetTypeInfo().IsGenericType && reg.EventType.GetTypeInfo().IsGenericType &&
                    (reg.EventType.GetTypeInfo().GetGenericTypeDefinition()
                    .IsAssignableFrom(eventType.GetTypeInfo().GetGenericTypeDefinition())))
                {
                    var eventTypeArguments = eventType.GetTypeInfo().GetGenericArguments();
                    var regEvTypeArguments = reg.EventType.GetTypeInfo().GetGenericArguments();
                    if ((eventType.GetTypeInfo().GetGenericArguments().Length ==
                        reg.EventType.GetTypeInfo().GetGenericArguments().Length))
                    {
                        var l = reg.EventType.GetTypeInfo().GetGenericArguments().Length;
                        for (int i = 0; i < l; i++)
                        {
                            if (!regEvTypeArguments[i].GetTypeInfo().IsAssignableFrom(eventTypeArguments[i]))
                                return false;
                        }
                        result = true;
                    }
                }
            }

            return result;
        }

        private List<EventHandlerRegistration> GetHandlerRegistrations(Type eventType)
        {
            return _registrations.Where(r => Match(r, eventType)).ToList();
        }
    }
}