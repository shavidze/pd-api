using System.Threading.Tasks;

namespace Application.PitchDeckExecutors
{
    public interface IQueryExecutor<TQuery,TResult>
    {
        Task<TResult> ExecuteAsync(TQuery query);
    }
}
