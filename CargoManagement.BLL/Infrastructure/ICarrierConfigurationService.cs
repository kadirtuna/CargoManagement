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
    public interface ICarrierConfigurationService
    {
        public Task<Tuple<List<ReadCarrierConfigurationDTO>, bool>> GetCarrierConfigurations();
        public Task<Tuple<ReadCarrierConfigurationDTO, bool>> GetCarrierConfiguration(int carrierId);
        public Task<Tuple<string, bool>> PutCarrierConfiguration(int carrierId, CarrierConfigurationDTO carrierConfigurationDTO);
        public Task<Tuple<string, bool>> PostCarrierConfiguration(int carrierId, CarrierConfigurationDTO carrierConfigurationDTO);
        public Task<Tuple<string, bool>> DeleteCarrierConfiguration(int carrierId);
    }
}
