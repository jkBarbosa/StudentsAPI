using System.Threading.Tasks;
using Autofac;

namespace Core.CQRS
{
    public interface IQuery<TResults>
    {
    }

    public interface IQueryHandler<in TQuery, TResults>
        where TQuery : class, IQuery<TResults>
    {
        Task<TResults> ExecuteAsync(TQuery query);
    }

    public interface IQueryExecutor
    {
        Task<TResults> ExecuteAsync<TResults>(IQuery<TResults> query);
    }

    public class QueryExecutor : IQueryExecutor
    {
        private readonly ILifetimeScope _container;

        public QueryExecutor(ILifetimeScope container)
        {
            _container = container;
        }

        public async Task<TResults> ExecuteAsync<TResults>(IQuery<TResults> query)
        {
            var queryType = query.GetType();
            var responseType = typeof(TResults);
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, responseType);

            var handler = (dynamic)_container.Resolve(handlerType);
            return await handler.ExecuteAsync((dynamic)query);
        }
    }
}
