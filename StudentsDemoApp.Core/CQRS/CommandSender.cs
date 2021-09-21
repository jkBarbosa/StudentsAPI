using System.Threading.Tasks;
using Autofac;

namespace Core.CQRS
{

    public interface ICommandExecutor<in TCommand> where TCommand : class
    {
        Task ExecuteAsync(TCommand command);
    }

    public interface ICommandSender
    {
        Task SendAsync(object command);
    }

    public class CommandSender : ICommandSender
    {
        private readonly ILifetimeScope _container;

        public CommandSender(ILifetimeScope container)
        {
            _container = container;
        }

        public async Task SendAsync(object command)
        {
            var commandType = command.GetType();
            var handlerType = typeof(ICommandExecutor<>).MakeGenericType(commandType);

            var handler = (dynamic)_container.Resolve(handlerType);
            await handler.ExecuteAsync((dynamic)command);
        }
    }
}
