using Microsoft.EntityFrameworkCore;  
using AdminDevicesAPI.Models;          
using AdminDevicesAPI.Data;           

namespace AdminDevicesAPI.Data        
{
    public class DeviceContext : DbContext  
    {
        public DeviceContext(DbContextOptions<DeviceContext> options) : base(options)  
        {
        }
        
        public DbSet<Device> Devices { get; set; } 
    }
}
