using Flunt.Notifications;
using System.Collections.Generic;

namespace MEChallenge.Application.Interfaces
{
    public interface IValidator<T>
    {
        bool Validate(T entity);

        IReadOnlyCollection<Notification> Notifications { get; }
    }
}
