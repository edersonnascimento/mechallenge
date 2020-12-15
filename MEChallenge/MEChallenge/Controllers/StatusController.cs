using MEChallenge.Application.Services;
using MEChallenge.CrossCutting.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MEChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly OrderService _orderService;

        public StatusController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] StatusOrderViewModel model)
        {
            return Ok(_orderService.SetOrderStatus(model));
        }
    }
}
