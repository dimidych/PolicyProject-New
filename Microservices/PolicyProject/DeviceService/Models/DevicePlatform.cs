using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceService
{
    public class DevicePlatform
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short DevicePlatformId { get; set; }

        [Required]
        public string DevicePlatformName { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}