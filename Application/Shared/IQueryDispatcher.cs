using System.Threading.Tasks;

namespace Application.Shared
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatcheAsyn<TQuery, TResult>(TQuery query);
    }
}
