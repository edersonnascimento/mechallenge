using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;

namespace MEChallenge.Application.Queries
{
    public class OrderQueryById : AbstractQuery<Order, string, string>
    {
        public OrderQueryById(IRepository<Order, string> repository) : base(repository) { }

        public override Order Query(string id) => _repository.GetById(id).Result;
    }
}
