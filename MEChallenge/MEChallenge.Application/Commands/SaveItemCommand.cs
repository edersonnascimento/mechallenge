using MEChallenge.Application.Interfaces;
using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;

namespace MEChallenge.Application.Commands
{
    public class SaveItemCommand : AbstractCommand<Item, string>
    {
        public SaveItemCommand(IRepository<Item, string> repository, IValidator<Item> validator) : base(repository, validator) { }

        public override void Execute(Item entity)
        {
            base.Execute(entity);
            if (Valid) {
                _repository.Save(entity);
            }
        }
    }
}
