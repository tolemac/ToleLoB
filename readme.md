# ToleLoB project

ToleLob is a little .NET Core C# project about several exercises, these exercises are about implementing some patters and small pieces of code, witch are designed to be used in Line Of Bussiness Applications.

Each folder in `src` folder corresponds to an exercise. The same for `test` folder.

# Contributing
You can contribute with the project by creating new exercises, or creating an issue writing about some exercise aproach, or code correction, or new exercise proposal.

Here are some advices:

* The issues can be wrote in English or Spanish language.
* The tests are not needed, although is a good practice.
* The exercises can use classes from others exercises.

All contribution are welcome.

# Exercises

## DependencyResolver

DependencyResolver add an interface to implementing a service locator pattern. It's used from others exercises in order to create new objects from a given type.

## EventBus

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

EventBus use `IDependencyResolver` from DependencyResolver exercise in order to create news handlers.
EventBus tests use a simple implementation that create a new object for each `resolve` method call.

## CQRS

This exercise is about a particular implementation of CQRS. In this system you have commands that change certains state items (or state elements) and you have queries that query some elements of the state. `QuerySystem` saves each query result in a cache, the next time the same query is run with the same parameters QuerySystem doesn't run the query, it returns the last cached result. When you execute a command `CommandSystem` synchronize with `QuerySystem` passing the state elements affected by the command, then `QuerySystem` purge results from cache of queries that query the same state items.

Query example. It's a query that query suppliers from application state, in the constructor `Supplier` is added to `QueryStateItemList` in order to set `Supplier` as state element that this query consume:

```` csharp
public class GetSuppliersQuery : Query<GetSuppliersQueryInput, GetSuppliersQueryOutput>
{
    public GetSuppliersQuery()
    {
        QueryedStateItemsList.Add(typeof(Supplier));
    }
    public override GetSuppliersQueryOutput Run(GetSuppliersQueryInput parameters)
    {
        return ...;
    }
}
````

Command example. In this case we have a command that add an element to the state. `Supplier` is marked as a state item that that command affect.

```` csharp
public class CreateSupplierCommand : Command<CreateSupplierParameters>
{
    public CreateSupplierCommand()
    {
        StateItemsAffectedsList.Add(typeof(Supplier));
    }
    public override void Execute(CreateSupplierParameters parameters) { }
}
````

When `CommandSystem` executes `CreateSupplierCommand` it purges the query results of queries that work with `typeof(Supplier)`.

The affected state items list of a command or the queryed state items list of a query is a list of objects, they don't have to be a `Type` you can do:

```` csharp
// In query constructor 
QueryedStateItemsList.Add("ClientInvoices");

// In command constructor
StateItemsAffectedsList.Add("ClientInvoices");
````
When the command is executed `QuerySystem` will search by results of queries that have `"ClientInvoices"` in his `QueryedStateItemsList`.

Finally an example of command that change more than one state element:

```` csharp
public class CreateInvoiceCommand : Command<CreateInvoiceParameters>
{
    public CreateInvoiceCommand()
    {
        StateItemsAffectedsList.AddRange(new[] {
            typeof(Invoice),
            typeof(InvoiceDetail),
            typeof(Customer),
            typeof(Supplier),
            "Stock"
        });
    }
    public override void Execute(CreateInvoiceParameters parameters) { }
}
````

The execution of that command invalidate the cached results of all queries that have at least one of affected state items: `typeof(Invoice)`, `typeof(InvoiceDetail)`, `typeof(Customer)`, `typeof(Supplier)` or `"Stock"`.