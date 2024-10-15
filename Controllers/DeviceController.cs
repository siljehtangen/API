using AdminDevicesAPI.DTOs;
using AdminDevicesAPI.Responses;
using AdminDevicesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AdminDevicesAPI.Interfaces;

namespace AdminDevicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly ILogger<DeviceController> _logger;

        public DeviceController(IDeviceService deviceService, ILogger<DeviceController> logger) =>
            (_deviceService, _logger) = (deviceService, logger);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceResponseDTO>>> GetDevices(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                _logger.LogWarning("Invalid pagination parameters: pageNumber = {PageNumber}, pageSize = {PageSize}", pageNumber, pageSize);
                return BadRequest(new ErrorResponse { Message = "Page number and page size must be greater than zero." });
            }

            var devices = await _deviceService.GetDevices(pageNumber, pageSize);
            _logger.LogInformation("Successfully retrieved {DeviceCount} devices at {LastUpdated}.", devices.Count(), DateTime.UtcNow);
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceResponseDTO>> GetDevice(int id)
        {
            var device = await _deviceService.GetDevice(id);
            if (device == null)
            {
                _logger.LogWarning("Device with ID {Id} not found at {LastUpdated}.", id, DateTime.UtcNow);
                return NotFound(new ErrorResponse { Message = $"Device with ID {id} not found." });
            }

            _logger.LogInformation("Successfully retrieved device: ID = {Id}, Name = {Name}, LastUpdated = {LastUpdated}.", 
            device.Id, device.Name, device.LastUpdated);
            return Ok(device);
        }

        [HttpPost]
        public async Task<ActionResult<DeviceResponseDTO>> PostDevice([FromBody] DeviceCreateDTO deviceDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating a device at {LastUpdated}.", DateTime.UtcNow);
                return BadRequest(ModelState);
            }

            var createdDevice = await _deviceService.CreateDevice(deviceDto);
            _logger.LogInformation("Device created successfully: ID = {Id}, Name = {Name}, LastUpdated = {LastUpdated}.", 
            createdDevice?.Id, createdDevice?.Name, createdDevice?.LastUpdated);
            return CreatedAtAction(nameof(GetDevice), new { id = createdDevice?.Id }, createdDevice);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeviceResponseDTO>> PutDevice(int id, [FromBody] DeviceUpdateDTO deviceDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating a device at {LastUpdated}.", DateTime.UtcNow);
                return BadRequest(ModelState);
            }

            var updatedDevice = await _deviceService.UpdateDevice(id, deviceDto);
            if (updatedDevice == null)
            {
                _logger.LogWarning("Device with ID {Id} not found for update at {LastUpdated}.", id, DateTime.UtcNow);
                return NotFound(new ErrorResponse { Message = $"Device with ID {id} not found." });
            }

            _logger.LogInformation("Device updated successfully: ID = {Id}, Name = {Name}, LastUpdated = {LastUpdated}.", 
            updatedDevice.Id, updatedDevice.Name, updatedDevice.LastUpdated);
            return Ok(updatedDevice);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuccessResponse>> DeleteDevice(int id)
        {
            var response = await _deviceService.DeleteDevice(id);
            if (response == null)
            {
                _logger.LogWarning("Attempt to delete non-existing device with ID {Id} at {LastUpdated}.", id, DateTime.UtcNow);
                return NotFound(new ErrorResponse { Message = $"Device with ID {id} not found." });
            }

            _logger.LogInformation("Device deleted successfully: ID = {Id}, DeletedAt = {LastUpdated}.", id, DateTime.UtcNow);
            return Ok(response);
        }
    }
}
