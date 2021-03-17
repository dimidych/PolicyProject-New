using System;
using RabbitMQ.Client;

namespace RabbitMqEventBus
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}