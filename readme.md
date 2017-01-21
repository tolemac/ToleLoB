# ToleLoB project

ToleLob is a little .NET Core C# project about several exercises, these exercises are about implementing some patters and small pieces of code, witch are designed to be used in Line Of Bussiness Applications.

Each folder in `src` folder corresponds to an exercise. The same for `test` folder.

# Contributing
You can contribute with the project by creating new exercises, or creating an issue writing about some exercise aproach, or code correction, or new exercise proposal.

Here are some advices:

[*] The issues can be wrote in English or Spanish language.
[*] The tests are not needed, although is a good practice.
[*] The exercises can use classes from others exercises.

All contribution are welcome.

# Exercises

### EventBus

EventBus is a resource where you can register event handlers, these event handlers are designed to be executed when a concrete event type is triggered.

You can declared custom events inheriting from `EventBase` class this way:

```` csharp
public class SimpleEventClass : EventBase { }

```` 

From you have created your event class you can trigger this event:

```` csharp
eventBusObject.Trigger(new SimpleEventClass());
````

If you want to handle event when it's triggered you have to create a handle class inheriting from `EventHandler` class:

```` csharp
public class SimpleEventHandler : EventHandler<SimpleEventClass>
{
    public override void Run(SimpleEventClass ev) 
    { 
        // This method is called from EventBus when the event is triggered 
    }
}
````

And finally, you have to register your event handler:

```` csharp
eventBusObject.RegisterHandler(new SimpleEventHandler()); // One instance for all triggers
// OR
eventBusObject.RegisterHandler<SimpleEventHandler, SimpleEventClass>(); // New instance for each triggers
````
