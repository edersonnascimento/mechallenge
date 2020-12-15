using System.Collections.Generic;

namespace MEChallenge.CrossCutting.ViewModels
{
    public class StatusResultViewModel
    {
        public string Pedido { get; set; }
        public List<string> Status { get; set; } = new List<string>();
    }
}
