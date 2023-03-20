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
        public IRepository<Carrier> _carrierRepository;
        public IRepository<CarrierConfiguration> _carrierConfigurationRepository;
        public IRepository<Order> _orderRepository;

        public OrderController(ILogger<Order> logger, IRepository<Carrier> carrierRepository, IRepository<CarrierConfiguration> carrierConfigurationRepository, IRepository<Order> orderRepository)
        {
            _logger = logger;
            _carrierRepository = carrierRepository;
            _carrierConfigurationRepository = carrierConfigurationRepository;
            _orderRepository = orderRepository;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<ReadOrderDTO>>> GetOrders()
        {
            var orders = await _orderRepository.GetAll();
            List<ReadOrderDTO> listReadOrderDTO = new List<ReadOrderDTO>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (Order order in orders)
            {
                ReadOrderDTO readOrderDTO = new ReadOrderDTO()
                {
                    OrderId = order.OrderId,
                    CarrierId = order.CarrierId,
                    OrderDesi = order.OrderDesi,
                    OrderCarrierCost = order.OrderCarrierCost
                };

                listReadOrderDTO.Add(readOrderDTO);
            }

            return Ok(listReadOrderDTO);
        }

        [HttpGet("{orderId:int}/")]
        public async Task<ActionResult<ReadOrderDTO>> GetOrder(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            ReadOrderDTO readOrderDTO = new ReadOrderDTO();

            if (order == null)
                return NotFound("Any Order couldn't be found by given OrderId!");

            readOrderDTO.OrderId = order.OrderId;
            readOrderDTO.CarrierId = order.CarrierId;
            readOrderDTO.OrderDesi = order.OrderDesi;
            readOrderDTO.OrderCarrierCost = order.OrderCarrierCost;

            return Ok(readOrderDTO);
        }

        [HttpPut("{orderId:int}")]
        public async Task<ActionResult<string>> PutOrder(int orderId, UpdateOrderDTO updateOrderDTO)
        {
            var order = await _orderRepository.GetById(orderId);
            
            if (order == null)
                return NotFound("Any Order couldn't be found by given OrderId!");

            order.CarrierId = updateOrderDTO.CarrierId;
            order.OrderDesi = updateOrderDTO.OrderDesi;
            order.OrderCarrierCost = updateOrderDTO.OrderCarrierCost;

            await _orderRepository.Update(order);
            await _orderRepository.CommitAsync();

            return Ok(String.Format("The Order with the OrderId: {0} has been successfully updated!", orderId)); 
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostOrder(CreateOrderDTO createOrderDTO)
        {
            var carrierConfigurations = await _carrierConfigurationRepository.GetAll();
            Decimal cheapestPrice = Decimal.MaxValue;
            Decimal orderPrice = Decimal.MaxValue;
            int cheapestCarrierId = -1;
            Dictionary<int, int> dataOfMaxDesiOfCarriers = new Dictionary<int, int>(); //The format is Dictionary<CarrierId, CarrierMaxDesi>
            int lowestDesiDifference = int.MaxValue;
            int carrierIdAtLowestDesiDifference = -1;
            Order newOrder = new Order();

            if (carrierConfigurations == null)
                return NotFound("There is not any registered Carrier in the system!");

            foreach (CarrierConfiguration carrierConfiguration in carrierConfigurations) //Determining the cheapestCarrier for the order. Subsequently, The order will be associated with that Carrier.
            {
                Decimal localOrderPrice = decimal.MaxValue;
                dataOfMaxDesiOfCarriers.Add(carrierConfiguration.CarrierId, carrierConfiguration.CarrierMaxDesi);

                if (createOrderDTO.OrderDesi >= carrierConfiguration.CarrierMinDesi && createOrderDTO.OrderDesi <= carrierConfiguration.CarrierMaxDesi)
                {
                    localOrderPrice = carrierConfiguration.CarrierCost;

                    if (localOrderPrice < cheapestPrice)
                    {
                        cheapestPrice = localOrderPrice;
                        cheapestCarrierId = carrierConfiguration.CarrierId; 
                    }
                }
            }

            orderPrice = cheapestPrice;
            carrierIdAtLowestDesiDifference = cheapestCarrierId;

            if (cheapestCarrierId == -1)
            {
                foreach (var dataOfmaxDesiOfCarrier in dataOfMaxDesiOfCarriers)
                {
                    int desiDifference = Math.Abs(dataOfmaxDesiOfCarrier.Value - createOrderDTO.OrderDesi);

                    if (desiDifference < lowestDesiDifference) {
                        lowestDesiDifference = desiDifference;
                        carrierIdAtLowestDesiDifference = dataOfmaxDesiOfCarrier.Key;
                    }
                }

                CarrierConfiguration carrierConfigurationAtLowestDesiDifference = await _carrierConfigurationRepository.GetById(carrierIdAtLowestDesiDifference);
                Carrier carrierAtLowestDesiDifference = await _carrierRepository.GetById(carrierIdAtLowestDesiDifference);

                orderPrice = carrierConfigurationAtLowestDesiDifference.CarrierCost + carrierAtLowestDesiDifference.CarrierPlusDesiCost * lowestDesiDifference;
            }

            newOrder.CarrierId = carrierIdAtLowestDesiDifference;
            newOrder.OrderDesi = createOrderDTO.OrderDesi;
            newOrder.OrderDate = DateTime.Now;
            newOrder.OrderCarrierCost = orderPrice;

            await _orderRepository.Insert(newOrder);
            await _orderRepository.CommitAsync();

            return Ok("The new Order has been successfully added!");
        }

        [HttpDelete("{orderId:int}")]
        public async Task<ActionResult<string>> DeleteOrder(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);

            if (order == null)
                return NotFound("Any Order couldn't be found by given OrderId!");
            
            await _orderRepository.Delete(order);
            await _orderRepository.CommitAsync();

            return Ok(String.Format("The Order with OrderId: {0} has been successfully deleted!", orderId));
        }
    }
}
