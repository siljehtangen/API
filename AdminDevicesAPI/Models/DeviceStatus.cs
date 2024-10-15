using System.Text.Json.Serialization;

namespace AdminDevicesAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeviceStatus
    {
        Active,
        Inactive
    }
}
