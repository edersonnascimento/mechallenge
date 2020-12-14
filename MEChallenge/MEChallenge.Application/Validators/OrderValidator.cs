using Flunt.Notifications;
using MEChallenge.Application.Interfaces;
using MEChallenge.Domain.Models;

namespace MEChallenge.Application.Validators
{
    public class OrderValidator : Notifiable, IValidator<Order>
    {
        public bool Validate(Order entity) => true;
    }
}
