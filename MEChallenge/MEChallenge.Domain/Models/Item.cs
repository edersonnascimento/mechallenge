﻿namespace MEChallenge.Domain.Models
{
    public class Item
    {
        public string PedidoId { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
    }
}
