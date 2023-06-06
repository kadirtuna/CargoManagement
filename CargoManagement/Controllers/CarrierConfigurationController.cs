using CargoManagement.BLL.Infrastructure;
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
        public ICarrierConfigurationService _carrierConfigurationService;

        public CarrierConfigurationController(ILogger<CarrierConfiguration> logger, ICarrierConfigurationService carrierConfigurationService)
        {
            _logger = logger;
            _carrierConfigurationService = carrierConfigurationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadCarrierConfigurationDTO>>> GetCarrierConfigurations()
        {
            var response = await _carrierConfigurationService.GetCarrierConfigurations();
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(response.Item1);
        }

        [HttpGet("{carrierId:int}/")]
        public async Task<ActionResult<ReadCarrierConfigurationDTO>> GetCarrierConfiguration(int carrierId)
        {
            var response = await _carrierConfigurationService.GetCarrierConfiguration(carrierId);

            if (response.Item2 == false)
                return NotFound("Any CarrierConfiguration couldn't be found by given carrierId!");

            return Ok(response.Item1);
        }

        [HttpPut("{carrierId:int}")]
        public async Task<ActionResult<string>> PutCarrierConfiguration(int carrierId, CarrierConfigurationDTO carrierConfigurationDTO)
        {
            var response = await _carrierConfigurationService.PutCarrierConfiguration(carrierId, carrierConfigurationDTO);

            if (response.Item2 == false)
                return NotFound(response.Item1);

            return Ok(response.Item1);
        }

        [HttpPost("{carrierId:int}")]
        public async Task<ActionResult<string>> PostCarrierConfiguration(int carrierId, CarrierConfigurationDTO carrierConfigurationDTO)
        {
            var response = await _carrierConfigurationService.PostCarrierConfiguration(carrierId, carrierConfigurationDTO);

            if (response.Item2 == false)
                return NotFound(response.Item1);

            return Ok(response.Item1);
        }

        [HttpDelete("{carrierId:int}")]
        public async Task<ActionResult<string>> DeleteCarrierConfiguration(int carrierId)
        {
            var response = await _carrierConfigurationService.DeleteCarrierConfiguration(carrierId);

            if (response.Item2 == false)
                return NotFound(response.Item1);

            return Ok(response.Item1);
        }
    }
}
