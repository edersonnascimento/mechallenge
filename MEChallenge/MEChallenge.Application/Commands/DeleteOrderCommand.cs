using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;

namespace MEChallenge.Application.Commands
{
    public class DeleteOrderCommand : AbstractCommand<Order, string>
    {
        public DeleteOrderCommand(IRepository<Order, string> repository) : base(repository) { }
        public override void Execute(Order entity)
        {
            base.Execute(entity);
            if (Valid) {
                _repository.Delete(entity.Id).ConfigureAwait(false);
            }
        }
    }
}
