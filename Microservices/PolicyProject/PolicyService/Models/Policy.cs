using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevicePlatformEntity;

namespace PolicyService
{
    public class Policy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PolicyId { get; set; }

        [Required]
        public string PolicyName { get; set; }

        [Required]
        public short PlatformId { get; set; }

        [Required]
        public string PolicyInstruction { get; set; }

        public string PolicyDefaultParam { get; set; }

        public virtual DevicePlatform DevicePlatform { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
