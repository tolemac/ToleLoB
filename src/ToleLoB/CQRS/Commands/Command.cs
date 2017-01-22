using System.Collections.Generic;

namespace ToleLoB.CQRS.Commands
{
    public interface ICommand
    {
        void Execute(object parameters);
        object[] StateItemsAffecteds { get; }
    }
    public abstract class Command : ICommand
    {
        protected List<object> StateItemsAffectedsList = new List<object>();
        public object[] StateItemsAffecteds { get { return StateItemsAffectedsList.ToArray(); } }

        public abstract void Execute(object parameters);
    }

    public interface ICommand<TParameters> : ICommand
    {
        void Execute(TParameters parameters);
    }
    public abstract class Command<TParameters> : Command, ICommand<TParameters>
    {
        public override void Execute(object parameters)
        {
            Execute((TParameters)parameters);
        }

        public abstract void Execute(TParameters parameters);
    }
}