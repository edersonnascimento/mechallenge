using MEChallenge.Application.Interfaces;
using MEChallenge.Domain.Interfaces;

namespace MEChallenge.Application.Queries
{
    public abstract class AbstractQuery<TResult, TParam, TKey> : IParamQuery<TResult, TParam>
    {
        protected IRepository<TResult, TKey> _repository;

        protected AbstractQuery(IRepository<TResult, TKey> repository) => _repository = repository;

        public abstract TResult Query(TParam param);
    }
}
