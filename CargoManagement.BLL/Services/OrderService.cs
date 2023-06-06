using CargoManagement.BLL.Infrastructure;
using CargoManagement.DAL.DTO.CreateDTO;
using CargoManagement.DAL.DTO.ReadDTO;
using CargoManagement.DAL.DTO.UpdateDTO;
using CargoManagement.DAL.Models;
using CargoManagement.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagement.BLL.Services
{
    public class OrderService : IOrderService
    {
        public ILogger<Order> _logger { get; set; }
        public IRepository<Carrier> _carrierRepository;
        public IRepository<CarrierConfiguration> _carrierConfigurationRepository;
        public IRepository<Order> _orderRepository;

        public OrderService(ILogger<Order> logger, IRepository<Carrier> carrierRepository, IRepository<CarrierConfiguration> carrierConfigurationRepository, IRepository<Order> orderRepository)
        {
            _logger = logger;
            _carrierRepository = carrierRepository;
            _carrierConfigurationRepository = carrierConfigurationRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Tuple<List<ReadOrderDTO>, bool>> GetOrders()
        {
            var orders = await _orderRepository.GetAll();
            List<ReadOrderDTO> listReadOrderDTO = new List<ReadOrderDTO>();

            foreach (Order order in orders)
            {
                ReadOrderDTO readOrderDTO = new ReadOrderDTO()
                {
                    OrderId = order.OrderId,
                    CarrierId = order.CarrierId,
                    OrderDesi = order.OrderDesi,
                    OrderCarrierCost = order.OrderCarrierCost,
                    OrderDate = order.OrderDate,
                };

                listReadOrderDTO.Add(readOrderDTO);
            }

            return Tuple.Create(listReadOrderDTO, true);
        }

        public async Task<Tuple<ReadOrderDTO, bool>> GetOrder(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            ReadOrderDTO readOrderDTO = new ReadOrderDTO();

            if (order == null)
                return Tuple.Create(new ReadOrderDTO(), false);

            readOrderDTO.OrderId = order.OrderId;
            readOrderDTO.CarrierId = order.CarrierId;
            readOrderDTO.OrderDesi = order.OrderDesi;
            readOrderDTO.OrderCarrierCost = order.OrderCarrierCost;
            readOrderDTO.OrderDate = order.OrderDate;

            return Tuple.Create(readOrderDTO, true);
        }

        public async Task<Tuple<string, bool>> PutOrder(int orderId, UpdateOrderDTO updateOrderDTO)
        {
            var order = await _orderRepository.GetById(orderId);

            if (order == null)
                return Tuple.Create("Any Order couldn't be found by given OrderId!", false);

            order.CarrierId = updateOrderDTO.CarrierId;
            order.OrderDesi = updateOrderDTO.OrderDesi;
            order.OrderCarrierCost = updateOrderDTO.OrderCarrierCost;

            await _orderRepository.Update(order);
            await _orderRepository.CommitAsync();

            return Tuple.Create(String.Format("The Order with the OrderId: {0} has been successfully updated!", orderId), true);
        }

        public async Task<Tuple<string, bool>> PostOrder(CreateOrderDTO createOrderDTO)
        {
            var carrierConfigurations = await _carrierConfigurationRepository.GetAll();
            var carriers = await _carrierRepository.GetAll();

            Decimal cheapestPrice = Decimal.MaxValue;
            Decimal orderPrice = Decimal.MaxValue;
            int cheapestCarrierId = -1;
            Dictionary<int, int> dataOfMaxDesiOfCarriers = new Dictionary<int, int>(); //The format is Dictionary<CarrierId, CarrierMaxDesi>
            int lowestDesiDifference = int.MaxValue;
            int carrierIdAtLowestDesiDifference = -1;
            Order newOrder = new Order();

            if (carrierConfigurations == null)
                return Tuple.Create("There is not any registered Carrier in the system!", false);

            foreach (CarrierConfiguration carrierConfiguration in carrierConfigurations) //Determining the cheapestCarrier for the order. Subsequently, The order will be associated with that Carrier.
            {
                Decimal localOrderPrice = decimal.MaxValue;
                var carrier= carriers.Where<Carrier>(p => p.CarrierId == carrierConfiguration.CarrierId).LastOrDefault();

                if (carrier != null && carrier.CarrierIsActive == true)
                {
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
            }

            orderPrice = cheapestPrice;
            carrierIdAtLowestDesiDifference = cheapestCarrierId;

            if (cheapestCarrierId == -1)
            {
                foreach (var dataOfmaxDesiOfCarrier in dataOfMaxDesiOfCarriers)
                {
                    int desiDifference = Math.Abs(dataOfmaxDesiOfCarrier.Value - createOrderDTO.OrderDesi);

                    if (desiDifference < lowestDesiDifference)
                    {
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

            return Tuple.Create("The new Order has been successfully added!", true);
        }

        public async Task<Tuple<string, bool>> DeleteOrder(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);

            if (order == null)
                return Tuple.Create("Any Order couldn't be found by given OrderId!", false);

            await _orderRepository.Delete(order);
            await _orderRepository.CommitAsync();

            return Tuple.Create(String.Format("The Order with OrderId: {0} has been successfully deleted!", orderId), true);
        }
    }
}
