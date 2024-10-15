using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AdminDevicesAPI.Data;
using AdminDevicesAPI.Models;
using AdminDevicesAPI.DTOs;

namespace AdminDevicesAPI
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DeviceContext(
                serviceProvider.GetRequiredService<DbContextOptions<DeviceContext>>());

            if (context.Devices.Any())
            {
                return;  
            }

            var devices = Enumerable.Range(1, 20).Select(i => new Device
            {
                Name = $"Device {i}",
                Status = i % 2 == 0 ? DeviceStatus.Active : DeviceStatus.Inactive,
                Type = i % 3 == 0 ? "Actuator" : "Sensor",
                LastUpdated = DateTime.Now
            }).ToArray();

            context.Devices.AddRange(devices);

            context.SaveChanges();
        }
    }
}
