namespace ToleLoB.CQRS.Commands
{
    public interface ICommand
    {
        void Execute(object parameters);
    }
    public abstract class Command : ICommand
    {
        public abstract void Execute(object parameters);
    }

    public interface ICommand<TParameters> : ICommand
    {
        void Execute(TParameters parameters);
    }
    public abstract class Command<TParameters> : ICommand<TParameters>
    {
        public void Execute(object parameters)
        {
            Execute((TParameters)parameters);
        }

        public abstract void Execute(TParameters parameters);
    }
}