using System;
using B3.Test.RabbitMq.Abstractions.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace B3.Test.RabbitMq.Abstractions
{
    public class B3RabbitMq : IRabbitMq
    {
        public ConnectionFactory ConnnectionFactory { get; set; }

        public static EventingBasicConsumer EventingBasicConsumer { get; set; }
        
        public bool IsOpen { get; private set; }
       
        public void InitiateConnectionFactory(string connectionString)
        {
            ConnnectionFactory = GetConnectionFactory(connectionString);
            ConnnectionFactory.CreateConnection();
            IsOpen = ConnnectionFactory.CreateConnection().IsOpen;
        }

        public EventingBasicConsumer EventBasicConsumer(IModel model)
        {
            return new EventingBasicConsumer(model);
        }

        public void CreateItemOnQueue(string queueName, ReadOnlyMemory<byte> message)
        {
            // SE CONEXÃO NÃO ESTIVER ABERTA, FALAR PARA ABRIR A CONEXÃO
            using var channel = ConnnectionFactory.CreateConnection().CreateModel();
            channel.BasicPublish("", queueName,null, message);
        }

        private ConnectionFactory GetConnectionFactory(string connectionString)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(connectionString)
            };

            return factory;
        }
    }
}
