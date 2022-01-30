using System.Threading.Tasks;

namespace Application.Shared
{
    public interface ICommandDispatcher
    {
        Task<CommandResult> DispatcheAsync<T>(T command) where T : class;
    }
}
