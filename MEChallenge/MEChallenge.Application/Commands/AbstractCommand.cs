using Flunt.Notifications;
using MEChallenge.Application.Interfaces;
using MEChallenge.Domain.Interfaces;

namespace MEChallenge.Application.Commands
{
    public abstract class AbstractCommand<T, U> : Notifiable, ICommand<T>
    {
        protected IRepository<T, U> _repository;
        protected IValidator<T> _validator;

        protected AbstractCommand(IRepository<T, U> repository) : this(repository, null) { }
        protected AbstractCommand(IRepository<T, U> repository, IValidator<T> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public virtual void Execute(T entity)
        {
            if(_validator != null && !_validator.Validate(entity)) {
                AddNotifications(_validator.Notifications);
                return;
            }
        }
    }
}
