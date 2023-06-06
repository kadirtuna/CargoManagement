using CargoManagement.DAL.DTO.CreateDTO;
using CargoManagement.DAL.DTO.ReadDTO;
using CargoManagement.DAL.DTO.UpdateDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagement.BLL.Infrastructure
{
    public interface IOrderService
    {
        public Task<Tuple<List<ReadOrderDTO>, bool>> GetOrders();
        public Task<Tuple<ReadOrderDTO, bool>> GetOrder(int orderId);
        public Task<Tuple<string, bool>> PutOrder(int orderId, UpdateOrderDTO updateOrderDTO);
        public Task<Tuple<string, bool>> PostOrder(CreateOrderDTO createOrderDTO);
        public Task<Tuple<string, bool>> DeleteOrder(int orderId);
    }
}
