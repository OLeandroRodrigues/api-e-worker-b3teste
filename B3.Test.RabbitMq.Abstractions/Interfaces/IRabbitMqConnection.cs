using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace B3.Test.RabbitMq.Abstractions.Interfaces
{
    public interface IRabbitMq
    {
        void InitiateConnectionFactory(string connectionString);
        void CreateItemOnQueue(string queueName, ReadOnlyMemory<byte> message);
        EventingBasicConsumer EventBasicConsumer(IModel model);
    }
}
