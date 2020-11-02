using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActionLogService.Models
{
    public class ActionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ActionLogId { get; set; }

        [Required] public DateTime ActionLogDate { get; set; }

        [Required] public int EventActionId { get; set; }

        public long? DocumentId { get; set; }

        [Required] public string Message { get; set; }

        [Required] public string DeviceSerialNumber { get; set; }

        [Required] public string Login { get; set; }

        public virtual EventAction Action { get; set; }

        [Timestamp] public byte[] Timestamp { get; set; }
    }
}