using Application.PitchDeckExecutors;
using System;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> DispatcheAsyn<TQuery, TResult>(TQuery query)
        {
            var type = typeof(IQueryExecutor<TQuery, TResult>);

            var queryExecutor = _serviceProvider.GetService(type);

            if (queryExecutor == null)
                throw new Exception("QueryExecutor is not provided");

            var result = await ((IQueryExecutor<TQuery, TResult>)queryExecutor).ExecuteAsync(query);

            return result;
        }
    }
}
