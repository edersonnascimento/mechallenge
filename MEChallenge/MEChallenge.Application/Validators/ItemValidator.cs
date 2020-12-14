using Flunt.Notifications;
using MEChallenge.Application.Interfaces;
using MEChallenge.Domain.Models;

namespace MEChallenge.Application.Validators
{
    public class ItemValidator : Notifiable, IValidator<Item>
    {
        public bool Validate(Item entity) => true;
    }
}
