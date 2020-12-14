using MEChallenge.Application.Interfaces;
using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;

namespace MEChallenge.Application.Commands
{
    public class DeleteItemCommand : AbstractCommand<Item, string>
    {
        public DeleteItemCommand(IRepository<Item, string> repository, IValidator<Item> validator) : base(repository, validator) { }

        public override void Execute(Item entity)
        {
            base.Execute(entity);
            if (Valid) {
                _repository.Delete(entity.Description).ConfigureAwait(false);
            }
        }
    }
}
