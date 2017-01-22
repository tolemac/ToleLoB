using ToleLoB.DependencyResolver;
using ToleLoB.CQRS.Queries;

namespace ToleLoB.CQRS.Commands
{
    public class CommandSystem
    {
        IDependencyResolver _resolver;
        QuerySystem _querySystem;
        public CommandSystem(IDependencyResolver resolver, QuerySystem querySystem)
        {
            _resolver = resolver;
            _querySystem = querySystem;
        }

        private void SynchronizeQuerySystem(ICommand command)
        {
            _querySystem.PurgeQueriesByStateItems(command.StateItemsAffecteds);
        }

        public void Execute<TCommand, TParameters>(TParameters parameters)
            where TCommand : ICommand<TParameters>
        {
            var command = _resolver.Resolve<TCommand>();
            command.Execute(parameters);

            SynchronizeQuerySystem(command);
        }
    }
}