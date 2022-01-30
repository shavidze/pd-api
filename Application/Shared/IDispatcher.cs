using System.Threading.Tasks;

namespace Application.Shared
{
    public interface IDispatcher
    {
        Task<CommandResult> DispatcheCommandAsync<T>(T command) where T : class;
        Task<TResult> DispatcheQueryAsync<TQuery, TResult>(TQuery query);
    }
}
