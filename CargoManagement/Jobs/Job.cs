using CargoManagement.BLL.Infrastructure;
using CargoManagement.DAL.Models;
using Microsoft.IdentityModel.Tokens;

namespace CargoManagement.API.Jobs
{
    public class Job
    {
        private readonly IOrderService _orderService;
        private readonly ICarrierReportsService _carrierReportsService;

        public Job(IOrderService orderService, ICarrierReportsService carrierReportsService) 
        { 
            _orderService = orderService;   
            _carrierReportsService = carrierReportsService;
        }  
        public async Task TotalFees()
        {
            var response = await _orderService.GetOrders();

            var orders = response.Item1.GroupBy(x => new 
            { 
                x.CarrierId,
                x.OrderDate
            }).ToList();

            if (!orders.IsNullOrEmpty())
            {
                CarrierReports tempCarrierReports = new CarrierReports();
                var tempOrder = orders.First().First();

                if (tempOrder != null)
                {
                    tempCarrierReports.CarrierId = tempOrder.CarrierId;
                    var lastOrder = orders.Last().First();  

                    foreach (var orderQuery in orders)
                    {
                        var order = orderQuery.First();

                        if(order.CarrierId != tempCarrierReports.CarrierId) 
                        {
                            tempCarrierReports.CarrierReportDate = DateTime.Now;
                            await _carrierReportsService.PostCarrierReports(tempCarrierReports);

                            tempCarrierReports = new CarrierReports();
                            tempCarrierReports.CarrierId = order.CarrierId;
                            tempCarrierReports.CarrierCost += order.OrderCarrierCost;

                            if (order.Equals(lastOrder))
                            {
                                tempCarrierReports.CarrierReportDate = DateTime.Now;
                                await _carrierReportsService.PostCarrierReports(tempCarrierReports);
                            }
                        }
                        else
                        {
                            tempCarrierReports.CarrierCost += order.OrderCarrierCost;
                        }
                    }
                }
            }
        }
    }
}
