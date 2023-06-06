using CargoManagement.BLL.Infrastructure;
using CargoManagement.DAL.DTO;
using CargoManagement.DAL.DTO.CreateDTO;
using CargoManagement.DAL.DTO.ReadDTO;
using CargoManagement.DAL.DTO.UpdateDTO;
using CargoManagement.DAL.Models;
using CargoManagement.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CargoManagement.API.Properties
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public ILogger<Order> _logger { get; set; }
        public IOrderService _orderService;

        public OrderController(ILogger<Order> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<ReadOrderDTO>>> GetOrders()
        {
            var response = await _orderService.GetOrders();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(response.Item1);
        }

        [HttpGet("{orderId:int}/")]
        public async Task<ActionResult<ReadOrderDTO>> GetOrder(int orderId)
        {
            var response = await _orderService.GetOrder(orderId);

            if (response.Item1 == null)
                return NotFound("Any Order couldn't be found by given OrderId!");

   
            return Ok(response.Item1);
        }

        [HttpPut("{orderId:int}")]
        public async Task<ActionResult<string>> PutOrder(int orderId, UpdateOrderDTO updateOrderDTO)
        {
            var response = await _orderService.PutOrder(orderId, updateOrderDTO);

            if (response.Item1 == null)
                return NotFound("Any Order couldn't be found by given OrderId!");

            return Ok(String.Format("The Order with the OrderId: {0} has been successfully updated!", orderId)); 
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostOrder(CreateOrderDTO createOrderDTO)
        {
            var response = await _orderService.PostOrder(createOrderDTO);

            if (response.Item1 == null)
                return NotFound("There is not any registered Carrier in the system!");

            return Ok("The new Order has been successfully added!");
        }

        [HttpDelete("{orderId:int}")]
        public async Task<ActionResult<string>> DeleteOrder(int orderId)
        {
            var response = await _orderService.DeleteOrder(orderId);

            if (response == null)
                return NotFound("Any Order couldn't be found by given OrderId!");

            return Ok(String.Format("The Order with OrderId: {0} has been successfully deleted!", orderId));
        }
    }
}
