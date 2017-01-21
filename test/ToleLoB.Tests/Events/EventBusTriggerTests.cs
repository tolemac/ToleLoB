using Xunit;
//using System;
using Moq;

namespace ToleLoB.Events.Tests
{
    public class EventBusTrigger
    {
        [Fact]
        public void HandlerIsCalled()
        {
            var eventBus = new EventBus();
            var handlerMock = new Mock<SimpleEventHandler>();
            var ev = new SimpleEventClass();

            eventBus.RegisterHandler(handlerMock.Object);
            eventBus.Trigger(ev);

            handlerMock.Verify(f => f.Run((EventBase)ev), Times.Once);
        }

        [Fact]
        public void HandlerIsCalledAllwaysThatEventTriggers()
        {
            var eventBus = new EventBus();
            var handlerMock = new Mock<SimpleEventHandler>();
            eventBus.RegisterHandler(handlerMock.Object);

            var ev = new SimpleEventClass();
            eventBus.Trigger(ev);
            eventBus.Trigger(ev);
            eventBus.Trigger(ev);

            handlerMock.Verify(f => f.Run((EventBase)ev), Times.Exactly(3));
        }

        [Fact]
        public void HandlersIsCalled_UsingDerivedEvent()
        {
            //Given
            var eventBus = new EventBus();
            var handlerMock = new Mock<SimpleEventHandler>();
            var ev = new DerivedSimpleEventClass();

            //When
            eventBus.RegisterHandler(handlerMock.Object);
            eventBus.Trigger(ev);

            //Then
            handlerMock.Verify(f => f.Run((EventBase)ev), Times.Once);
        }

    }
}