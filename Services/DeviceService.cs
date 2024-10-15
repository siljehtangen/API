using AdminDevicesAPI.Interfaces;
using AdminDevicesAPI.Models;
using AdminDevicesAPI.DTOs;
using AdminDevicesAPI.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace AdminDevicesAPI.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository) =>
            _deviceRepository = deviceRepository;

        private DeviceResponseDTO MapToDeviceResponseDTO(Device device) => new()
        {
            Id = device.Id,
            Name = device.Name,
            Status = device.Status,
            Type = device.Type,
            LastUpdated = device.LastUpdated
        };

        public async Task<IEnumerable<DeviceResponseDTO>> GetDevices(int pageNumber, int pageSize)
        {
            var devices = await _deviceRepository.GetAllDevices(pageNumber, pageSize);
            return devices.Select(MapToDeviceResponseDTO);
        }

        public async Task<DeviceResponseDTO?> GetDevice(int id)
        {
            var device = await _deviceRepository.GetDeviceById(id);
            return device != null ? MapToDeviceResponseDTO(device) : null;
        }

        public async Task<DeviceResponseDTO?> CreateDevice(DeviceCreateDTO deviceDto)
        {
            var device = new Device
            {
                Name = deviceDto.Name,
                Status = deviceDto.Status,
                Type = deviceDto.Type,
                LastUpdated = DateTime.UtcNow
            };

            _deviceRepository.AddDevice(device);
            await _deviceRepository.SaveChangesAsync();

            return MapToDeviceResponseDTO(device);
        }

        public async Task<DeviceResponseDTO?> UpdateDevice(int id, DeviceUpdateDTO deviceDto)
        {
            var device = await _deviceRepository.GetDeviceById(id);
            if (device == null) return null;

            device.Name = deviceDto.Name;
            device.Status = deviceDto.Status;
            device.Type = deviceDto.Type;
            device.LastUpdated = DateTime.UtcNow;

            await _deviceRepository.SaveChangesAsync();

            return MapToDeviceResponseDTO(device);
        }

        public async Task<SuccessResponse?> DeleteDevice(int id)
        {
            var device = await _deviceRepository.GetDeviceById(id);
            if (device == null) return null;

            _deviceRepository.DeleteDevice(device);
            await _deviceRepository.SaveChangesAsync();

            return new SuccessResponse { Message = $"Device with ID {id} was deleted successfully." };
        }
    }
}
