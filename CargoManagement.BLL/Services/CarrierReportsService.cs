using CargoManagement.BLL.Infrastructure;
using CargoManagement.DAL.Models;
using CargoManagement.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagement.BLL.Services
{
    public class CarrierReportsService : ICarrierReportsService
    {
        public IRepository<CarrierReports> _carrierReportsRepository;

        public CarrierReportsService(IRepository<CarrierReports> carrierReportsRepository)
        {
            _carrierReportsRepository = carrierReportsRepository;
        }

        public async Task<string> PostCarrierReports(CarrierReports carrierReports)
        {
            await _carrierReportsRepository.Insert(carrierReports);
            await _carrierReportsRepository.CommitAsync();

            return "";
        }
    }
}
