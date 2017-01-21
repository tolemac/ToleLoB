using System;

namespace ToleLoB.DependencyResolver
{
    public interface IDependencyResolver
    {
        object Resolve(Type type);
        TType Resolve<TType>();
    }
}
