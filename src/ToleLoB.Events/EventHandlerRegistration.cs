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

        private EventHandlerRegistration(EventBus eventBus, Type handlerType, object handlerInstance)
        {
            this._eventBus = eventBus;
            this.HandlerInstance = handlerInstance;
            this.HandlerType = handlerType;
            this.EventType = Common.GetEventTypeFromEventHandlerType(handlerType);
        }
        public EventHandlerRegistration(EventBus eventBus, object handler)
            : this(eventBus, handler.GetType(), handler)
        {
        }
        public EventHandlerRegistration(EventBus eventBus, Type handlerType)
            : this(eventBus, handlerType, null)
        {
        }

        public Type HandlerType { get; private set; }
        public object HandlerInstance { get; private set; }
        public Type EventType { get; private set; }

        public void Unregister()
        {
            _eventBus.Unregister(this);
        }
    }
}