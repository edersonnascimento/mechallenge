using MEChallenge.Application.Interfaces;
using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;

namespace MEChallenge.Application.Commands
{
    public class ChangeStatusOrderCommand : AbstractCommand<Order, string>
    {
        public ChangeStatusOrderCommand(IRepository<Order, string> repository, IValidator<Order> validator) : base(repository, validator) { }

        public override void Execute(Order entity)
        {
            base.Execute(entity);
            var order = _repository.GetById(entity.Id).Result;
            if(order == null) {
                AddNotification("Pedido", "Pedido não encontrado!");
            }

            if (Valid) {
                _repository.Save(entity);
            }
        }
    }
}
