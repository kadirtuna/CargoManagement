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
    public class CarrierService : ICarrierService
    {
        public ILogger<Carrier> _logger { get; set; }
        public IRepository<Carrier> _carrierRepository;
        public IRepository<CarrierConfiguration> _carrierConfigurationRepository;

        public CarrierService(ILogger<Carrier> logger, IRepository<Carrier> carrierRepository, IRepository<CarrierConfiguration> carrierConfigurationRepository)
        {
            _logger = logger;
            _carrierRepository = carrierRepository;
            _carrierConfigurationRepository = carrierConfigurationRepository;
        }

        public async Task<Tuple<List<ReadCarrierDTO>, bool>> GetCarriers()
        {
            var carriers = await _carrierRepository.GetAll();
            var carrierConfigurations = await _carrierConfigurationRepository.GetAll();
            List<ReadCarrierDTO> listReadCarrierDTO = new List<ReadCarrierDTO>();

            foreach (Carrier carrier in carriers)
            {
                ReadCarrierDTO readCarrierDTO = new ReadCarrierDTO()
                {
                    CarrierId = carrier.CarrierId,
                    CarrierName = carrier.CarrierName,
                    CarrierIsActive = carrier.CarrierIsActive,
                    CarrierPlusDesiCost = carrier.CarrierPlusDesiCost,
                };

                listReadCarrierDTO.Add(readCarrierDTO);
            }

            return Tuple.Create(listReadCarrierDTO, true);
        }

        public async Task<Tuple<ReadCarrierDTO, bool>> GetCarrier(int carrierId)
        {
            var carrier = await _carrierRepository.GetById(carrierId);
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(carrierId);
            ReadCarrierDTO readCarrierDTO = new ReadCarrierDTO();

            if (carrier == null)
                return Tuple.Create(new ReadCarrierDTO(), false);

            readCarrierDTO.CarrierId = carrier.CarrierId;
            readCarrierDTO.CarrierName = carrier.CarrierName;
            readCarrierDTO.CarrierIsActive = carrier.CarrierIsActive;
            readCarrierDTO.CarrierPlusDesiCost = carrier.CarrierPlusDesiCost;

            return Tuple.Create(readCarrierDTO, true);
        }

        public async Task<Tuple<string, bool>> PutCarrier(int carrierId, CarrierDTO carrierDTO)
        {
            var carrier = await _carrierRepository.GetById(carrierId);

            if (carrier == null)
                return Tuple.Create("Any carrier couldn't be found by given carrierId!", false);

            carrier.CarrierName = carrierDTO.CarrierName;
            carrier.CarrierIsActive = carrierDTO.CarrierIsActive;
            carrier.CarrierPlusDesiCost = carrierDTO.CarrierPlusDesiCost;

            await _carrierRepository.Update(carrier);
            await _carrierRepository.CommitAsync();

            return Tuple.Create(String.Format("The carrier with the carrierId: {0} has been successfully updated!", carrierId), true);
        }

        public async Task<Tuple<string, bool>> PostCarrier(CarrierDTO carrierDTO)
        {
            Carrier newCarrier = new Carrier();

            newCarrier.CarrierName = carrierDTO.CarrierName;
            newCarrier.CarrierIsActive = carrierDTO.CarrierIsActive;
            newCarrier.CarrierPlusDesiCost = carrierDTO.CarrierPlusDesiCost;

            await _carrierRepository.Insert(newCarrier);
            await _carrierRepository.CommitAsync();

            return Tuple.Create("The new carrier has been successfully added!", true);
        }

        public async Task<Tuple<string, bool>> DeleteCarrier(int carrierId)
        {
            var carrier = await _carrierRepository.GetById(carrierId);

            if (carrier == null)
                return Tuple.Create("Any carrier couldn't be found by given carrierId!", false);

            await _carrierRepository.Delete(carrier);
            await _carrierRepository.CommitAsync();

            return Tuple.Create(String.Format("The carrier with carrierId: {0} has been successfully deleted!", carrierId), true);
        }
    }
}
