using System;
using System.ComponentModel.DataAnnotations;

namespace AdminDevicesAPI.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DeviceStatus Status { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Type cannot exceed 30 characters.")]
        public string Type { get; set; } = string.Empty;

        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
