using Xunit;

namespace ToleLoB.Events.Tests
{
    public class EventBusRegistration
    {
        //private EventBus eventBus = new EventBus();

        [Fact]
        public void HaveToFindHandlerOfConcreteEventType_RegisteredByType()
        {
            var eventBus = new EventBus();
            eventBus.RegisterHandler<SimpleEventClass>(typeof(SimpleEventHandler));
            Assert.True(eventBus.HasHandlerFor<SimpleEventClass>());
        }

        [Fact]
        public void HaveToFindHandlerOfDerivedEventType_RegisteredByType()
        {
            var eventBus = new EventBus();
            eventBus.RegisterHandler<SimpleEventClass>(typeof(SimpleEventHandler));
            Assert.True(eventBus.HasHandlerFor<DerivedSimpleEventClass>());
        }

        [Fact]
        public void HaveToFindHandlerOfConcreteEventType_RegisteredByGenerics()
        {
            var eventBus = new EventBus();
            eventBus.RegisterHandler<SimpleEventHandler, SimpleEventClass>();
            Assert.True(eventBus.HasHandlerFor<SimpleEventClass>());
        }

        [Fact]
        public void HaveToFindHandlerOfDerivedEventType_RegisteredByGenerics()
        {
            var eventBus = new EventBus();
            eventBus.RegisterHandler<SimpleEventHandler, SimpleEventClass>();
            Assert.True(eventBus.HasHandlerFor<DerivedSimpleEventClass>());
        }

        [Fact]
        public void HaveToFindHandlerOfConcreteEventType_RegisteredByInstance()
        {
            var eventBus = new EventBus();
            var handler = new SimpleEventHandler();
            eventBus.RegisterHandler(handler);
            Assert.True(eventBus.HasHandlerFor<SimpleEventClass>());
        }

        [Fact]
        public void HaveToFindHandlerOfDerivedEventType_RegisteredByInstance()
        {
            var eventBus = new EventBus();
            var handler = new SimpleEventHandler();
            eventBus.RegisterHandler(handler);
            Assert.True(eventBus.HasHandlerFor<DerivedSimpleEventClass>());
        }

        [Fact]
        public void HaveToFailFindingUnregisterHandler()
        {
            var eventBus = new EventBus();
            var reg = eventBus.RegisterHandler<SimpleEventHandler, SimpleEventClass>();
            reg.Unregister();
            Assert.False(eventBus.HasHandlerFor<SimpleEventClass>());
        }

        [Fact]
        public void CanHaveMultipleHandlersForOneEvent()
        {
            var eventBus = new EventBus();
            eventBus.RegisterHandler<SimpleEventHandler, SimpleEventClass>();
            eventBus.RegisterHandler<SimpleEventHandler, SimpleEventClass>();
            eventBus.RegisterHandler<DerivedSimpleEventHandler, DerivedSimpleEventClass>();
            Assert.Equal(eventBus.GetHandlersFor<SimpleEventClass>().Count, 2);
            Assert.Equal(eventBus.GetHandlersFor<DerivedSimpleEventClass>().Count, 3);
        }

        [Fact]
        public void HaveToFindHandlerUsingGenericEventType()
        {
            var eventBus = new EventBus();
            eventBus.RegisterHandler<CreateEntityPersonEventHandler, CreateEntityEvent<Person>>();
            Assert.True(eventBus.HasHandlerFor<CreateEntityEvent<Person>>());
            Assert.True(eventBus.HasHandlerFor<CreatePersonEvent>());
        }

        [Fact]
        public void HaveToFindHandlerUsingGenericEventTypeWithDerivedTypeArgument()
        {
            var eventBus = new EventBus();
            eventBus.RegisterHandler<CreateEntityEventHandler, CreateEntityEvent<Entity>>();
            Assert.True(eventBus.HasHandlerFor<CreateEntityEvent<Person>>());
            // Assert.True(eventBus.HasHandlerFor<CreatePersonEvent>());
        }
    }
}
