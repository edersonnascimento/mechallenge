using MEChallenge.Application.Services;
using MEChallenge.CrossCutting.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MEChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {

        private readonly OrderService _orderService;

        public PedidoController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/<PedidoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_orderService.All());
        }

        // GET api/<PedidoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_orderService.GetById(id));
        }

        // POST api/<PedidoController>
        [HttpPost]
        public IActionResult Post([FromBody] InsertOrderViewModel value)
        {
            _orderService.Insert(value);
            if (_orderService.Valid) {
                return Ok();
            }
            return BadRequest(_orderService.Notifications);
        }

        // PUT api/<PedidoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] UpdateOrderViewModel value)
        {
            _orderService.Update(id, value);
            if (_orderService.Valid) {
                return Ok();
            }
            return BadRequest(_orderService.Notifications);
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _orderService.Delete(id);
            if (_orderService.Valid) {
                return Ok();
            }
            return BadRequest(_orderService.Notifications);
        }
    }
}
