using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeviceService;
using LoginService.Models;

namespace LoginDevicesService.Models
{
    public class LoginDevices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid LoginDeviceId { get; set; }

        [Required] public Guid LoginId { get; set; }

        [Required] public Guid DeviceId { get; set; }

        public bool? NeedUpdateDevice { get; set; }

        [NotMapped]
        public virtual Login LdLogin { get; set; }

        [NotMapped]
        public virtual Device LdDevice { get; set; }

        [Timestamp] public byte[] Timestamp { get; set; }
    }
}