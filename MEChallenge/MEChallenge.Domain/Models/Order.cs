using System.Collections.Generic;

namespace MEChallenge.Domain.Models
{
    public class Order
    {
        public string Id { get; set; }
        public int ApprovedItens { get; set; }
        public decimal ApprovedValue { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
