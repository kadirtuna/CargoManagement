using CargoManagement.DAL.DTO;
using CargoManagement.DAL.DTO.ReadDTO;
using CargoManagement.DAL.Models;
using CargoManagement.DAL.Repositories;
using CargoManagement.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CargoManagement.API.Properties
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierConfigurationController : ControllerBase
    {
        public ILogger<CarrierConfiguration> _logger;
        public IRepository<Carrier> _carrierRepository;
        public IRepository<CarrierConfiguration> _carrierConfigurationRepository;

        public CarrierConfigurationController(ILogger<CarrierConfiguration> logger, IRepository<Carrier> carrierRepository, IRepository<CarrierConfiguration> carrierConfigurationRepository)
        {
            _logger = logger;
            _carrierRepository = carrierRepository;
            _carrierConfigurationRepository = carrierConfigurationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadCarrierConfigurationDTO>>> GetCarrierConfigurations()
        {
            var carrierConfigurations = await _carrierConfigurationRepository.GetAll();
            List<ReadCarrierConfigurationDTO> listReadCarrierConfigurations = new List<ReadCarrierConfigurationDTO>();  

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            return Ok(listReadCarrierConfigurations);
        }

        [HttpGet("{carrierId:int}/")]
        public async Task<ActionResult<ReadCarrierConfigurationDTO>> GetCarrierConfiguration(int carrierId)
        {
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(carrierId);
            ReadCarrierConfigurationDTO readCarrierConfigurationDTO = new ReadCarrierConfigurationDTO();

            if (carrierConfiguration == null)
                return NotFound("Any CarrierConfiguration couldn't be found by given carrierId!");

            readCarrierConfigurationDTO.CarrierId = carrierConfiguration.CarrierId;
            readCarrierConfigurationDTO.CarrierMaxDesi = carrierConfiguration.CarrierMaxDesi;
            readCarrierConfigurationDTO.CarrierMinDesi = carrierConfiguration.CarrierMinDesi;
            readCarrierConfigurationDTO.CarrierCost = carrierConfiguration.CarrierCost;

            return Ok(readCarrierConfigurationDTO);
        }

        [HttpPut("{carrierId:int}")]
        public async Task<ActionResult<string>> PutCarrierConfiguration(int carrierId, CarrierConfigurationDTO carrierConfigurationDTO)
        {
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(carrierId);

            if (carrierConfiguration == null)
                return NotFound("Any CarrierConfiguration couldn't be found by given carrierId!");

            carrierConfiguration.CarrierMaxDesi = carrierConfigurationDTO.CarrierMaxDesi;
            carrierConfiguration.CarrierMinDesi = carrierConfigurationDTO.CarrierMinDesi;
            carrierConfiguration.CarrierCost = carrierConfigurationDTO.CarrierCost;

            await _carrierConfigurationRepository.Update(carrierConfiguration);
            await _carrierConfigurationRepository.CommitAsync();

            return Ok(String.Format("Congrutulations! The CarrierConfiguration with the carrierConfigurationId: {0} has been successfully updated!", carrierId));
        }

        [HttpPost("{carrierId:int}")]
        public async Task<ActionResult<string>> PostCarrierConfiguration(int carrierId, CarrierConfigurationDTO carrierConfigurationDTO)
        {
            var carrier = await _carrierRepository.GetById(carrierId);
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(carrierId);
            CarrierConfiguration newCarrierConfiguration = new CarrierConfiguration();

            if (carrier == null)
                return NotFound("Error! Any corresponding Carrier couldn't be found by given carrierId due to CarrierConfiguration's carrierId has to be the same id with the corresponding Carrier! Therefore, New CarrierConfiguration couldn't be added");

            if (carrierConfiguration != null)
                return Conflict("Error! There is already a CarrierConfiguration with given carrierId!");

            newCarrierConfiguration.CarrierId = carrierId;
            newCarrierConfiguration.CarrierMaxDesi = carrierConfigurationDTO.CarrierMaxDesi;
            newCarrierConfiguration.CarrierMinDesi = carrierConfigurationDTO.CarrierMinDesi;
            newCarrierConfiguration.CarrierCost = carrierConfigurationDTO.CarrierCost;
            

            //newCarrierConfiguration.Carrier = carrier;
            //carrier.CarrierConfiguration = carrierConfiguration;

            await _carrierConfigurationRepository.Insert(newCarrierConfiguration);
            await _carrierRepository.Update(carrier);
            await _carrierConfigurationRepository.CommitAsync();

            return Ok("The new CarrierConfiguration has been successfully added!");
        }

        [HttpDelete("{carrierId:int}")]
        public async Task<ActionResult<string>> DeleteCarrierConfiguration(int carrierId)
        {
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(carrierId);

            if (carrierConfiguration == null)
                return NotFound("Any CarrierConfiguration couldn't be found by given carrierId!");

            await _carrierConfigurationRepository.Delete(carrierConfiguration);
            await _carrierConfigurationRepository.CommitAsync();

            return Ok(String.Format("The carrierConfiguration with carrierConfigurationId: {0} has been successfully deleted!", carrierId));
        }
    }
}
