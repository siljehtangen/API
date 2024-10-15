using System.Collections.Generic;
using System.Threading.Tasks;
using AdminDevicesAPI.Models;

namespace AdminDevicesAPI.Interfaces
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetAllDevices(int pageNumber, int pageSize);
        Task<Device?> GetDeviceById(int id);
        void AddDevice(Device device);
        void UpdateDevice(Device device);
        void DeleteDevice(Device device);
        Task<bool> SaveChangesAsync();
    }
}
