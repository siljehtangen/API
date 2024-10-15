using AdminDevicesAPI.Models;

namespace AdminDevicesAPI.DTOs
{
    public class DeviceResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DeviceStatus Status { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
    }
}
