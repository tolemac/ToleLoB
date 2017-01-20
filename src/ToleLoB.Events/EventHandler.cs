namespace ToleLoB.Events
{
    public interface IEventHandler
    {

    }

    public interface IEventHandler<out TEvent> : IEventHandler
        where TEvent : EventBase
    {

    }

    public class EventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : EventBase
    {
    }
}