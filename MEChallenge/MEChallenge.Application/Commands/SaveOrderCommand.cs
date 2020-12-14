using MEChallenge.Application.Interfaces;
using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;

namespace MEChallenge.Application.Commands
{
    public class SaveOrderCommand : AbstractCommand<Order, string>
    {
        public SaveOrderCommand(IRepository<Order, string> repository, IValidator<Order> validator) : base(repository, validator) { }

        public override void Execute(Order entity)
        {
            base.Execute(entity);
            if (Valid) {
                _repository.Save(entity);
            }
        }
    }
}
