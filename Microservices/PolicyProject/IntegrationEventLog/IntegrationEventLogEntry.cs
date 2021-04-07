using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using EventBus;

namespace IntegrationEventLog
{
    public class IntegrationEventLogEntry
    {
        public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId)
        {
            EventId = @event.Id;
            CreationTime = @event.CreationDate;
            EventTypeName = @event.GetType().FullName;
            Content = JsonConvert.SerializeObject(@event);
            State = EventStateEnum.NotPublished;
            TimesSent = 0;
            TransactionId = transactionId.ToString();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid EventId { get; set; }

        [Required]
        public string EventTypeName { get; private set; }

        [NotMapped] 
        public string EventTypeShortName => EventTypeName.Split('.')?.Last();
        
        [NotMapped] 
        public IntegrationEvent IntegrationEvent { get; private set; }

        [Required]
        public EventStateEnum State { get; set; }

        [Required]
        public int TimesSent { get; set; }

        [Required]
        public DateTime CreationTime { get;  }

        [Required]
        public string Content { get; set; }

        public string TransactionId { get; private set; }

        public IntegrationEventLogEntry DeserializeJsonContent(Type type)
        {
            IntegrationEvent = JsonConvert.DeserializeObject(Content, type) as IntegrationEvent;
            return this;
        }
    }
}