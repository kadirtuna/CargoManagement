using CargoManagement.DAL.DTO;
using CargoManagement.DAL.DTO.ReadDTO;
using CargoManagement.DAL.Models;
using CargoManagement.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CargoManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrierController : ControllerBase
    {
        public ILogger<Carrier> _logger { get; set; }
        public IRepository<Carrier> _carrierRepository;
        public IRepository<CarrierConfiguration> _carrierConfigurationRepository;

        public CarrierController(ILogger<Carrier> logger, IRepository<Carrier> carrierRepository, IRepository<CarrierConfiguration> carrierConfigurationRepository)
        {
            _logger = logger;
            _carrierRepository = carrierRepository;
            _carrierConfigurationRepository = carrierConfigurationRepository;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<ReadCarrierDTO>>> GetCarriers()
        {
            var carriers = await _carrierRepository.GetAll();
            var carrierConfigurations = await _carrierConfigurationRepository.GetAll();
            List<ReadCarrierDTO> listReadCarrierDTO = new List<ReadCarrierDTO>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            return Ok(listReadCarrierDTO);
        }

        [HttpGet("{carrierId:int}/")]
        public async Task<ActionResult<ReadCarrierDTO>> GetCarrier(int carrierId)
        {
            var carrier = await _carrierRepository.GetById(carrierId);
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(carrierId);
            ReadCarrierDTO readCarrierDTO = new ReadCarrierDTO();

            if (carrier == null)
                return NotFound("Any carrier couldn't be found by given carrierId!");

            readCarrierDTO.CarrierId= carrier.CarrierId;  
            readCarrierDTO.CarrierName = carrier.CarrierName;
            readCarrierDTO.CarrierIsActive = carrier.CarrierIsActive;
            readCarrierDTO.CarrierPlusDesiCost = carrier.CarrierPlusDesiCost;

            return Ok(readCarrierDTO);
        }

        [HttpPut("{carrierId:int}")]
        public async Task<ActionResult<string>> PutCarrier(int carrierId, CarrierDTO carrierDTO)
        {
            var carrier = await _carrierRepository.GetById(carrierId);
            
            if (carrier == null)
                return NotFound("Any carrier couldn't be found by given carrierId!");

            carrier.CarrierName = carrierDTO.CarrierName;
            carrier.CarrierIsActive = carrierDTO.CarrierIsActive;
            carrier.CarrierPlusDesiCost = carrierDTO.CarrierPlusDesiCost;

            await _carrierRepository.Update(carrier);
            await _carrierRepository.CommitAsync();

            return Ok(String.Format("The carrier with the carrierId: {0} has been successfully updated!", carrierId)); 
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostCarrier(CarrierDTO carrierDTO)
        {
            Carrier newCarrier = new Carrier();

            newCarrier.CarrierName = carrierDTO.CarrierName;
            newCarrier.CarrierIsActive = carrierDTO.CarrierIsActive;
            newCarrier.CarrierPlusDesiCost = carrierDTO.CarrierPlusDesiCost;

            await _carrierRepository.Insert(newCarrier);
            await _carrierRepository.CommitAsync();

            return Ok("The new carrier has been successfully added!");
        }

        [HttpDelete("{carrierId:int}")]
        public async Task<ActionResult<string>> DeleteCarrier(int carrierId)
        {
            var carrier = await _carrierRepository.GetById(carrierId);

            if (carrier == null)
                return NotFound("Any carrier couldn't be found by given carrierId!");
            
            await _carrierRepository.Delete(carrier);
            await _carrierRepository.CommitAsync();

            return Ok(String.Format("The carrier with carrierId: {0} has been successfully deleted!", carrierId));
        }
    }
}
