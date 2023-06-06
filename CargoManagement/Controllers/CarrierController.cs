using CargoManagement.BLL.Infrastructure;
using CargoManagement.DAL.DTO;
using CargoManagement.DAL.DTO.ReadDTO;
using CargoManagement.DAL.Models;
using CargoManagement.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CargoManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrierController : ControllerBase
    {
        public ILogger<Carrier> _logger { get; set; }
        public ICarrierService _carrierService;
        public CarrierController(ILogger<Carrier> logger, ICarrierService carrierService)
        {
            _logger = logger;
            _carrierService = carrierService;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<ReadCarrierDTO>>> GetCarriers()
        {
            var response = await _carrierService.GetCarriers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(response.Item1);
        }

        [HttpGet("{carrierId:int}/")]
        public async Task<ActionResult<ReadCarrierDTO>> GetCarrier(int carrierId)
        {
            var response = await _carrierService.GetCarrier(carrierId);

            if (response.Item2 == false)
                return NotFound("Any carrier couldn't be found by given carrierId!");


            return Ok(response.Item1);
        }

        [HttpPut("{carrierId:int}")]
        public async Task<ActionResult<string>> PutCarrier(int carrierId, CarrierDTO carrierDTO)
        {
            var response = await _carrierService.PutCarrier(carrierId, carrierDTO);
            
            if (response.Item2 == false)
                return NotFound("Any carrier couldn't be found by given carrierId!");

            return Ok(String.Format("The carrier with the carrierId: {0} has been successfully updated!", carrierId)); 
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostCarrier(CarrierDTO carrierDTO)
        {
            var response = await _carrierService.PostCarrier(carrierDTO);
            
            if (response.Item2 == true)
                return Ok("The new carrier has been successfully added!");

            return BadRequest("There is a server-side error in the back-end");
        }

        [HttpDelete("{carrierId:int}")]
        public async Task<ActionResult<string>> DeleteCarrier(int carrierId)
        {
            var response = await _carrierService.DeleteCarrier(carrierId);

            if (response.Item2 == false)
                return NotFound("Any carrier couldn't be found by given carrierId!");

            return Ok(String.Format("The carrier with carrierId: {0} has been successfully deleted!", carrierId));
        }
    }
}
