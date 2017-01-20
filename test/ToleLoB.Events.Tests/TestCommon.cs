namespace ToleLoB.Events.Tests
{
    public class SimpleEventClass : EventBase { }
    public class DerivedSimpleEventClass : SimpleEventClass { }
    public class SimpleEventHandler : EventHandler<SimpleEventClass> { }
    public class DerivedSimpleEventHandler : EventHandler<DerivedSimpleEventClass> { }
    public class Entity { }
    public class CreateEntityEvent<TEntity> : EventBase where TEntity : Entity { }
    public class CreateEntityEventHandler : EventHandler<CreateEntityEvent<Entity>> { }
    public class Person : Entity { }
    public class CreatePersonEvent : CreateEntityEvent<Person> { }
    public class CreatePersonEventHandler : EventHandler<CreatePersonEvent> { }
}