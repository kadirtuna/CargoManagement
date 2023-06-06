using CargoManagement.DAL.DTO;
using CargoManagement.DAL.DTO.ReadDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagement.BLL.Infrastructure
{
    public interface ICarrierService
    {
        public Task<Tuple<List<ReadCarrierDTO>, bool>> GetCarriers();
        public Task<Tuple<ReadCarrierDTO, bool>> GetCarrier(int carrierId);
        public Task<Tuple<string, bool>> PutCarrier(int carrierId, CarrierDTO carrierDTO);
        public Task<Tuple<string, bool>> PostCarrier(CarrierDTO carrierDTO);
        public Task<Tuple<string, bool>> DeleteCarrier(int carrierId);

    }
}
