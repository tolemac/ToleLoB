using System;
using ToleLoB.DependencyResolver;

namespace ToleLoB.Events.Tests
{
    public class SimpleEventClass : EventBase { }
    public class DerivedSimpleEventClass : SimpleEventClass { }
    public class SimpleEventHandler : EventHandler<SimpleEventClass>
    {
        public int RunCounter { get; set; } = 0;
        public override void Run(SimpleEventClass ev)
        {
            RunCounter++;
        }
    }
    public class DerivedSimpleEventHandler : EventHandler<DerivedSimpleEventClass>
    {
        public override void Run(DerivedSimpleEventClass ev)
        {
            throw new NotImplementedException();
        }
    }
    public class Entity { }
    public class CreateEntityEvent<TEntity> : EventBase where TEntity : Entity { }
    public class CreateEntityEventHandler : EventHandler<CreateEntityEvent<Entity>>
    {
        public override void Run(CreateEntityEvent<Entity> ev)
        {
            throw new NotImplementedException();
        }
    }
    public class Person : Entity { }
    public class CreateEntityPersonEventHandler : EventHandler<CreateEntityEvent<Person>>
    {
        public override void Run(CreateEntityEvent<Person> ev)
        {
            throw new NotImplementedException();
        }
    }
    public class CreatePersonEvent : CreateEntityEvent<Person> { }
    public class CreatePersonEventHandler : EventHandler<CreatePersonEvent>
    {
        public override void Run(CreatePersonEvent ev)
        {
            throw new NotImplementedException();
        }
    }

    public class ObjectCreatorResolver : IDependencyResolver
    {
        public object Resolve(Type type)
        {
            return Activator.CreateInstance(type);
        }
        public TType Resolve<TType>()
        {
            return Activator.CreateInstance<TType>();
        }
    }
}