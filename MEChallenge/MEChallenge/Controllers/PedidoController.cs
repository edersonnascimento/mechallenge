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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PedidoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PedidoController>
        [HttpPost]
        public void Post([FromBody] InsertOrderViewModel value)
        {

        }

        // PUT api/<PedidoController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] UpdateOrderViewModel value)
        {
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
