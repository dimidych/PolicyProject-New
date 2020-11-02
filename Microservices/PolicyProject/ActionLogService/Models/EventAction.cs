using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActionLogService.Models
{
    public class EventAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventId { get; set; }
        
        [Required]
        public string EventName { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}