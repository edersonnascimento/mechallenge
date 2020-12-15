using Flunt.Notifications;
using Mapster;
using MEChallenge.Application.Commands;
using MEChallenge.Application.Interfaces;
using MEChallenge.Application.Queries;
using MEChallenge.CrossCutting.ViewModels;
using MEChallenge.Data;
using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MEChallenge.Application.Services
{
    public class OrderService : Notifiable
    {
        private readonly ChallengeContext _context;

        private readonly IRepository<Order, string> _orderRepository;
        private readonly IRepository<Item, string> _itemRepository;
        private readonly IValidator<Order> _orderValidator;
        private readonly IValidator<Item> _itemValidator;

        public OrderService(
            ChallengeContext context,
            IRepository<Order, string> orderRepository,
            IRepository<Item, string> itemRepository,
            IValidator<Order> orderValidator,
            IValidator<Item> itemValidator
        )
        {
            _context = context;

            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
            _orderValidator = orderValidator;
            _itemValidator = itemValidator;
        }

        public IEnumerable<OrderViewModel> All()
        {
            var query = new ListOrderQuery(_orderRepository);
            return query.Query().Select(o => o.Adapt<OrderViewModel>());
        }
        public OrderViewModel GetById(string id) => getOrder(id).Adapt<OrderViewModel>();
        public void Insert(InsertOrderViewModel model)
        {
            var order = model.Adapt<Order>();
            validateInsert(order);
            if (Valid) {
                var orderCmd = new SaveOrderCommand(_orderRepository, _orderValidator);
                orderCmd.Execute(order);
                AddNotifications(orderCmd);
                addItems(order);
            }

            if (Valid) {
                _context.SaveChanges();
            }
        }
        public void Update(string id, UpdateOrderViewModel model)
        {
            validateUpdate(model);

            if (Valid) {
                Order order = getOrder(id);
                if (order == null) {
                    AddNotification("Pedido", "Nenhum pedido com este código pode ser encontrado!");
                    return;
                }

                if (Valid) {
                    clearItems(model, order);
                }

                if (Valid) {
                    addOrUpdateItems(model, order);
                }
            }

            if (Valid) {
                _context.SaveChanges();
            }
        }
        public void Delete(string id)
        {
            var order = _orderRepository.GetById(id).Result;
            if (order == null) {
                AddNotification("Pedido", "Nenhum pedido com este código pode ser encontrado!");
                return;
            }
            if (order.ApprovedItens > 0 || order.ApprovedValue > 0) {
                AddNotification("Pedido", "Exclusão cancelada! O pedido já foi aprovado.");
                return;
            }

            if (Valid) {
                var itemCmd = new DeleteItemCommand(_itemRepository, _itemValidator);
                foreach (var item in order.Items) {
                    itemCmd.Execute(item);
                    AddNotifications(itemCmd);
                }
            }

            if (Valid) {
                var orderCmd = new DeleteOrderCommand(_orderRepository);
                orderCmd.Execute(order);
                AddNotifications(orderCmd);
            }

            if (Valid) {
                _context.SaveChanges();
            }
        }

        public StatusResultViewModel SetOrderStatus(StatusOrderViewModel model)
        {
            var order = getOrder(model.Pedido);
            if (order == null) {
                return new StatusResultViewModel { Pedido = model.Pedido, Status = new List<string> { "CODIGO_PEDIDO_INVALIDO" } };
            }

            changeOrderStatus(model, order);

            if (model.Status.ToLower() == "REPROVADO") {
                return new StatusResultViewModel { Pedido = model.Pedido, Status = new List<string> { "REPROVADO" } };
            }

            var totalValue = order.Items.Sum(i => i.UnitPrice * i.Quantity);
            var totalQuantity = order.Items.Sum(i => i.Quantity);

            List<string> status = new List<string>();
            if (model.ItensAprovados == totalQuantity && model.ValorAprovado == totalValue) {
                status.Add("APROVADO");
            } else {
                if (model.ItensAprovados < totalQuantity) {
                    status.Add("APROVADO_QTD_A_MENOR");
                } else if (model.ItensAprovados > totalQuantity) {
                    status.Add("APROVADO_QTD_A_MAIOR");
                }

                if (model.ValorAprovado < totalValue) {
                    status.Add("APROVADO_VALOR_A_MENOR");
                } else if (model.ValorAprovado > totalValue) {
                    status.Add("APROVADO_VALOR_A_MAIOR");
                }
            }

            return new StatusResultViewModel { Pedido = model.Pedido, Status = status };
        }

        private void changeOrderStatus(StatusOrderViewModel model, Order order)
        {
            if (model.Status.ToLower() == "REPROVADO") {
                order.ApprovedItens = 0;
                order.ApprovedValue = 0;
            } else {
                order.ApprovedItens = model.ItensAprovados;
                order.ApprovedValue = model.ValorAprovado;
            }

            var orderCmd = new ChangeStatusOrderCommand(_orderRepository, _orderValidator);
            orderCmd.Execute(order);
            AddNotifications(orderCmd);

            if (Valid) {
                _context.SaveChanges();
            }
        }
        private Order getOrder(string id)
        {
            var query = new OrderQueryById(_orderRepository);
            return query.Query(id);
        }
        private void addItems(Order order)
        {
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
        private void addOrUpdateItems(UpdateOrderViewModel model, Order order)
        {
            var itemCmd = new SaveItemCommand(_itemRepository, _itemValidator);
            foreach (var item in model.Items) {
                var entity = item.Adapt<Item>();
                entity.PedidoId = order.Id;
                itemCmd.Execute(entity);
                AddNotifications(itemCmd);

                if (Invalid) {
                    break;
                }
            }
        }
        private void clearItems(UpdateOrderViewModel model, Order order)
        {
            var itemCmd = new DeleteItemCommand(_itemRepository, _itemValidator);
            foreach (var item in order.Items) {
                if (!model.Items.Any(i => i.Description == item.Description)) {
                    itemCmd.Execute(item);
                    AddNotifications(itemCmd);
                }
            }
        }
        private void validateUpdate(UpdateOrderViewModel order)
        {
            if (order.Items == null || order.Items.Count == 0) {
                AddNotification("Itens", "Pedido inválido! Os pedidos devem conter ao meno um item.");
                return;
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
