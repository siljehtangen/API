using AdminDevicesAPI.Interfaces;
using AdminDevicesAPI.Models;
using Microsoft.EntityFrameworkCore;
using AdminDevicesAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDevicesAPI.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DeviceContext _context;

        public DeviceRepository(DeviceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Device>> GetAllDevices(int pageNumber, int pageSize)
        {
            return await _context.Devices
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Device?> GetDeviceById(int id)
        {
            return await _context.Devices
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public void AddDevice(Device device)
        {
            _context.Devices.Add(device);
        }

        public void UpdateDevice(Device device)
        {
            _context.Entry(device).State = EntityState.Modified;
        }

        public void DeleteDevice(Device device)
        {
            _context.Devices.Remove(device);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
