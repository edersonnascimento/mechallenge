using System;
using System.Collections.Generic;
using System.Text;

namespace MEChallenge.CrossCutting.ViewModels
{
    public class InsertOrderViewModel
    {
        public string Id { get; set; }

        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
    }
}
