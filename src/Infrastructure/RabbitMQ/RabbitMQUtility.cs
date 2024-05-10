using Application.Common;
using Application.Model;
using Common;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RabbitMQ
{
    public class RabbitMQUtility : IRabbitMQUtility
    {
        private IModel? _channel;
        private const string directName = "direct_queue";
        private string queueName = string.Empty;
        private readonly IOptions<AppSettings> appSettings;
        private readonly Object _object = new();
        private IModel channel
        {
            get
            {
                if (_channel == null)
                {
                    lock (_object)
                    {
                        if (_channel == null)
                        {
                            ConnectionFactory factory = new ConnectionFactory() { HostName = appSettings.Value.RabbitMQHostName };
                            IConnection connection = factory.CreateConnection();
                            _channel = connection.CreateModel();
                            _channel.ExchangeDeclare(exchange: directName, type: ExchangeType.Direct);
                            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                        }
                    }
                }
                return _channel;
            }
        }
        public RabbitMQUtility(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings;
        }


        public void RecieveMessage(RabbitMQRecieveRequest rabbitMQRecieveRequest)
        {
            queueName = rabbitMQRecieveRequest.QueueName ?? "MainDirectQueue";
            channel.QueueBind(queue: queueName, exchange: directName, routingKey: rabbitMQRecieveRequest.RoutingKey);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += rabbitMQRecieveRequest.EventHandler;
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
        public bool SendMessage(RabbitMQSendRequest rabbitMQSendRequest)
        {
            queueName = rabbitMQSendRequest.QueueName ?? "MainDirectQueue";
            var body = Encoding.UTF8.GetBytes(rabbitMQSendRequest.Message);
            channel.BasicPublish(exchange: directName, routingKey: rabbitMQSendRequest.RoutingKey, basicProperties: null, body: body);
            Console.WriteLine($" [x] Sent {rabbitMQSendRequest.Message}");
            return true;
        }
    }
}
