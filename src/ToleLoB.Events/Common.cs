using System;
using System.Linq;
using System.Reflection;

namespace ToleLoB.Events
{
    internal static class Common
    {
        public static Type GetEventTypeFromEventHandlerType(Type eventHandlerType)
        {
            var interf = eventHandlerType.GetTypeInfo().ImplementedInterfaces
                .FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IEventHandler<>));
            return interf.GenericTypeArguments[0];
        }
    }
}