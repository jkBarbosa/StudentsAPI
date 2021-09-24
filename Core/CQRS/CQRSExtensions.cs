using System;
using System.Threading.Tasks;

namespace Core.CQRS
{
    public static class CQRSExtensions
    {
        public static Task SendAsync<TCommand>(this ICommandSender commandSender, TCommand command, Action<TCommand> beforeSend)
            where TCommand : class, new()
        {
            beforeSend(command);
            return commandSender.SendAsync(command);
        }
    }
}
