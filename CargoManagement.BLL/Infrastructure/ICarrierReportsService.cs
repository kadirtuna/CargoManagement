using CargoManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagement.BLL.Infrastructure
{
    public interface ICarrierReportsService
    {
        public Task<string> PostCarrierReports(CarrierReports carrierReports);
    }
}
