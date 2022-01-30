using Application.PitchDeckExecutors;
using System;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<CommandResult> DispatcheAsync<T>(T command) where T : class
        {
            var type = typeof(ICommandExecutor<T>);

            var commandExecutor = _serviceProvider.GetService(type);

            if (commandExecutor == null)
                throw new Exception("CommandExecutor is not provided");

            var result = await ((ICommandExecutor<T>)commandExecutor).ExecuteAsync(command);

            return result;
        }
    }
}
