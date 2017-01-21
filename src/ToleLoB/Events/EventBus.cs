using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using ToleLoB.DependencyResolver;

namespace ToleLoB.Events
{
    public class EventBus
    {
        public EventBus(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }
        private IList<EventHandlerRegistration> _registrations = new List<EventHandlerRegistration>();
        private IDependencyResolver _resolver;
        public IEventHandlerRegistration RegisterHandler<TEvent>(EventHandler<TEvent> handler)
            where TEvent : EventBase
        {
            var reg = new EventHandlerRegistration<TEvent>(this, handler);
            _registrations.Add(reg);
            return reg;
        }
        public IEventHandlerRegistration RegisterHandler<TEventHandler, TEvent>()
            where TEventHandler : IEventHandler<TEvent>
            where TEvent : EventBase
        {
            var reg = new EventHandlerRegistration<TEvent>(this, typeof(TEventHandler));
            _registrations.Add(reg);
            return reg;
        }

        public IEventHandlerRegistration RegisterHandler<TEvent>(Type handlerType)
            where TEvent : EventBase
        {
            var reg = new EventHandlerRegistration<TEvent>(this, handlerType);
            _registrations.Add(reg);
            return reg;
        }

        public void Unregister(IEventHandlerRegistration registration)
        {
            var reg = (EventHandlerRegistration)registration;
            _registrations.Remove(reg);
        }

        public bool HasHandlerFor<TEvent>() where TEvent : EventBase
        {
            return GetHandlerRegistrations<TEvent>().Count() > 0;
        }

        public IList<IEventHandler> GetHandlersFor<TEvent>()
            where TEvent : EventBase
        {
            return GetHandlerRegistrations<TEvent>()
                .Select(r =>
                {
                    if (r.HandlerInstance == null)
                        return this.CreateHandlerInstance(r.HandlerType);
                    return r.HandlerInstance;
                })
                .ToList();
        }

        public void Trigger<TEvent>(TEvent ev)
            where TEvent : EventBase
        {
            var handlers = GetHandlersFor<TEvent>();
            var exceptionList = new List<Exception>();
            foreach (var h in handlers)
            {
                try
                {
                    h.Run(ev);
                }
                catch (Exception ex)
                {
                    exceptionList.Add(ex);
                }
            }
            if (exceptionList.Count() > 0)
            {
                if (exceptionList.Count() == 1)
                    throw exceptionList[0];
                else
                    throw new AggregateException(exceptionList);
            }
        }

        private IEventHandler CreateHandlerInstance(Type handlerType)
        {
            return _resolver.Resolve(handlerType) as IEventHandler;
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

        private List<EventHandlerRegistration> GetHandlerRegistrations<TEvent>()
            where TEvent : EventBase
        {
            return _registrations.Where(r => Match(r, typeof(TEvent))).ToList();
        }
    }
}