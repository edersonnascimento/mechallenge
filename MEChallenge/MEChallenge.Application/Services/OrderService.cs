using Flunt.Notifications;
using Mapster;
using MEChallenge.Application.Commands;
using MEChallenge.Application.Interfaces;
using MEChallenge.CrossCutting.ViewModels;
using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;

namespace MEChallenge.Application.Services
{
    public class OrderService : Notifiable
    {
        private readonly IRepository<Order, string> _orderRepository;
        private readonly IRepository<Item, string> _itemRepository;
        private readonly IValidator<Order> _orderValidator;
        private readonly IValidator<Item> _itemValidator;

        public OrderService(
            IRepository<Order, string> orderRepository,
            IRepository<Item, string> itemRepository,
            IValidator<Order> orderValidator,
            IValidator<Item> itemValidator
        )
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
            _orderValidator = orderValidator;
            _itemValidator = itemValidator;
        }

        public void Insert(InsertOrderViewModel model)
        {
            var order = model.Adapt<Order>();
            validateInsert(order);
            if (Valid) {
                var orderCmd = new SaveOrderCommand(_orderRepository, _orderValidator);
                orderCmd.Execute(order);

                AddNotifications(orderCmd);
                if (Valid) {
                    var itemCmd = new SaveItemCommand(_itemRepository, _itemValidator);
                    foreach (var item in order.Items) {
                        item.PedidoId = order.Id;
                        itemCmd.Execute(item);
                        AddNotifications(itemCmd);

                        if (Invalid) {
                            break;
                        }
                    }
                }
            }
        }

        private void validateInsert(Order order)
        {
            var entity = _orderRepository.GetById(order.Id).Result;
            if (entity != null) {
                AddNotification("Pedido", "Já existe um pedido com este código!");
                return;
            }

            if (order.Items == null || order.Items.Count == 0) {
                AddNotification("Itens", "Pedido inválido! Os pedidos devem conter ao meno um item.");
                return;
            }
        }
    }
}
