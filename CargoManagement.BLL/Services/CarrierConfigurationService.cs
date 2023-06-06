using CargoManagement.BLL.Infrastructure;
using CargoManagement.DAL.DTO;
using CargoManagement.DAL.DTO.ReadDTO;
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
    public class CarrierConfigurationService : ICarrierConfigurationService
    {
        public ILogger<CarrierConfiguration> _logger;
        public IRepository<Carrier> _carrierRepository;
        public IRepository<CarrierConfiguration> _carrierConfigurationRepository;

        public CarrierConfigurationService(ILogger<CarrierConfiguration> logger, IRepository<Carrier> carrierRepository, IRepository<CarrierConfiguration> carrierConfigurationRepository)
        {
            _logger = logger;
            _carrierRepository = carrierRepository;
            _carrierConfigurationRepository = carrierConfigurationRepository;
        }

        public async Task<Tuple<List<ReadCarrierConfigurationDTO>, bool>> GetCarrierConfigurations()
        {
            var carrierConfigurations = await _carrierConfigurationRepository.GetAll();
            List<ReadCarrierConfigurationDTO> listReadCarrierConfigurations = new List<ReadCarrierConfigurationDTO>();

            foreach (CarrierConfiguration carrierConfiguration in carrierConfigurations)
            {
                ReadCarrierConfigurationDTO carrierConfigurationDTO = new ReadCarrierConfigurationDTO()
                {
                    CarrierId = carrierConfiguration.CarrierId,
                    CarrierMaxDesi = carrierConfiguration.CarrierMaxDesi,
                    CarrierMinDesi = carrierConfiguration.CarrierMinDesi,
                    CarrierCost = carrierConfiguration.CarrierCost
                };

                listReadCarrierConfigurations.Add(carrierConfigurationDTO);
            }

            return Tuple.Create(listReadCarrierConfigurations, true);
        }

        public async Task<Tuple<ReadCarrierConfigurationDTO, bool>> GetCarrierConfiguration(int carrierId)
        {
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(carrierId);

            if (carrierConfiguration == null)
                return Tuple.Create(new ReadCarrierConfigurationDTO(), false);
            ReadCarrierConfigurationDTO readCarrierConfigurationDTO = new ReadCarrierConfigurationDTO();

            readCarrierConfigurationDTO.CarrierId = carrierConfiguration.CarrierId;
            readCarrierConfigurationDTO.CarrierMaxDesi = carrierConfiguration.CarrierMaxDesi;
            readCarrierConfigurationDTO.CarrierMinDesi = carrierConfiguration.CarrierMinDesi;
            readCarrierConfigurationDTO.CarrierCost = carrierConfiguration.CarrierCost;

            return Tuple.Create(readCarrierConfigurationDTO, true);
        }

        public async Task<Tuple<string, bool>> PutCarrierConfiguration(int carrierId, CarrierConfigurationDTO carrierConfigurationDTO)
        {
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(carrierId);

            if (carrierConfiguration == null)
                return Tuple.Create("Any CarrierConfiguration couldn't be found by given carrierId!", false);

            carrierConfiguration.CarrierMaxDesi = carrierConfigurationDTO.CarrierMaxDesi;
            carrierConfiguration.CarrierMinDesi = carrierConfigurationDTO.CarrierMinDesi;
            carrierConfiguration.CarrierCost = carrierConfigurationDTO.CarrierCost;

            await _carrierConfigurationRepository.Update(carrierConfiguration);
            await _carrierConfigurationRepository.CommitAsync();

            return Tuple.Create("Congrutulations! The CarrierConfiguration with the carrierConfigurationId: {0} has been successfully updated!" + carrierId, true);
        }

        public async Task<Tuple<string, bool>> PostCarrierConfiguration(int carrierId, CarrierConfigurationDTO carrierConfigurationDTO)
        {
            var carrier = await _carrierRepository.GetById(carrierId);
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(carrierId);
            CarrierConfiguration newCarrierConfiguration = new CarrierConfiguration();

            if (carrier == null)
                return Tuple.Create("Error! Any corresponding Carrier couldn't be found by given carrierId due to CarrierConfiguration's carrierId has to be the same id with the corresponding Carrier! Therefore, New CarrierConfiguration couldn't be added", false);

            if (carrierConfiguration != null)
                return Tuple.Create("Error! There is already a CarrierConfiguration with given carrierId!", false);

            newCarrierConfiguration.CarrierId = carrierId;
            newCarrierConfiguration.CarrierMaxDesi = carrierConfigurationDTO.CarrierMaxDesi;
            newCarrierConfiguration.CarrierMinDesi = carrierConfigurationDTO.CarrierMinDesi;
            newCarrierConfiguration.CarrierCost = carrierConfigurationDTO.CarrierCost;


            //newCarrierConfiguration.Carrier = carrier;
            //carrier.CarrierConfiguration = carrierConfiguration;

            await _carrierConfigurationRepository.Insert(newCarrierConfiguration);
            await _carrierRepository.Update(carrier);
            await _carrierConfigurationRepository.CommitAsync();

            return Tuple.Create("The new CarrierConfiguration has been successfully added!", true);
        }

        public async Task<Tuple<string, bool>> DeleteCarrierConfiguration(int carrierId)
        {
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(carrierId);

            if (carrierConfiguration == null)
                return Tuple.Create("Any CarrierConfiguration couldn't be found by given carrierId!", false);

            await _carrierConfigurationRepository.Delete(carrierConfiguration);
            await _carrierConfigurationRepository.CommitAsync();

            return Tuple.Create("The carrierConfiguration with carrierConfigurationId: {0} has been successfully deleted!" + carrierId, true);
        }
    }
}
