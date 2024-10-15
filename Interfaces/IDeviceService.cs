using System.Collections.Generic;
using System.Threading.Tasks;
using AdminDevicesAPI.Models;
using AdminDevicesAPI.DTOs;
using AdminDevicesAPI.Responses;

namespace AdminDevicesAPI.Interfaces
{
    public interface IDeviceService
    {
         Task<IEnumerable<DeviceResponseDTO>> GetDevices(int pageNumber, int pageSize);
         Task<DeviceResponseDTO?> GetDevice(int id);
         Task<DeviceResponseDTO?> CreateDevice(DeviceCreateDTO deviceDto);
         Task<DeviceResponseDTO?> UpdateDevice(int id, DeviceUpdateDTO deviceDto);
         Task<SuccessResponse?> DeleteDevice(int id);
    }
}
