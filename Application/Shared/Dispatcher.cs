using Infrastructure.PitchDeckAppDbContext;
using System;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public IQueryDispatcher _queryDispatcher { get; }

        private readonly PitchDeckDbContext _dbContext;

        public Dispatcher(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, PitchDeckDbContext dbContext)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _dbContext = dbContext;
        }

        public async Task<CommandResult> DispatcheCommandAsync<T>(T command) where T : class
        {
            try
            {
                var result = await _commandDispatcher.DispatcheAsync(command);

                await _dbContext.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {

                return CommandResult.Error(ex.Message);
            }
        }

        public async Task<TResult> DispatcheQueryAsync<TQuery, TResult>(TQuery query)
        {
            return await _queryDispatcher.DispatcheAsyn<TQuery, TResult>(query);
        }
    }
}
