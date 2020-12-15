using MEChallenge.Application.Interfaces;
using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;
using System.Collections.Generic;

namespace MEChallenge.Application.Queries
{
    public class ListOrderQuery : IQuery<IEnumerable<Order>>
    {
        protected readonly IRepository<Order, string> _repository;
        
        public ListOrderQuery(IRepository<Order, string> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Order> Query() => _repository.GetAll().Result;
    }
}
