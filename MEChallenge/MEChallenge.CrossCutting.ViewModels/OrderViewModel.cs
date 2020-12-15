using System.Collections.Generic;

namespace MEChallenge.CrossCutting.ViewModels
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public int ApprovedItens { get; set; }
        public decimal ApprovedValue { get; set; }

        public List<ItemViewModel> Items { get; set; }
    }
}
