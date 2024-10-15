using System.ComponentModel.DataAnnotations;
using AdminDevicesAPI.Models;

namespace AdminDevicesAPI.DTOs
{
    public class DeviceUpdateDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name length can't exceed 50 characters.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public DeviceStatus Status { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [MaxLength(30, ErrorMessage = "Type length can't exceed 30 characters.")]
        public required string Type { get; set; }
    }
}
