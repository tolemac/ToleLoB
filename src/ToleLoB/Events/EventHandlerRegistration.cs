using System;

namespace ToleLoB.Events
{
    public interface IEventHandlerRegistration
    {
        void Unregister();
    }

    internal class EventHandlerRegistration : IEventHandlerRegistration
    {
        private EventBus _eventBus;
        public Type HandlerType { get; private set; }
        public IEventHandler HandlerInstance { get; private set; }
        public Type EventType { get; private set; }

        public void Unregister()
        {
            _eventBus.Unregister(this);
        }

        protected EventHandlerRegistration(EventBus eventBus, Type handlerType, IEventHandler handlerInstance)
        {
            this._eventBus = eventBus;
            this.HandlerInstance = handlerInstance;
            this.HandlerType = handlerType;
            this.EventType = EventHandler.GetEventTypeFromEventHandlerType(handlerType);
        }
    }

    internal class EventHandlerRegistration<TEvent> : EventHandlerRegistration
        where TEvent : EventBase
    {
        public new IEventHandler<TEvent> HandlerInstance
        {
            get
            {
                return (IEventHandler<TEvent>)((EventHandlerRegistration)this).HandlerInstance;
            }
        }
        public EventHandlerRegistration(EventBus eventBus, IEventHandler<TEvent> handler)
            : base(eventBus, handler.GetType(), handler)
        {
        }
        public EventHandlerRegistration(EventBus eventBus, Type handlerType)
            : base(eventBus, handlerType, null)
        {
        }
    }
}