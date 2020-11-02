﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceService
{
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DeviceId { get; set; }
        
        [Required]
        public string DeviceName { get; set; }

        [Required]
        public string DeviceSerialNumber { get; set; }

        [Required]
        public string DeviceMacAddress { get; set; }

        [Required]
        public string DeviceIpAddress { get; set; }

        [Required]
        public short DevicePlatformId { get; set; }

        public virtual DevicePlatform DevicePlatform { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}