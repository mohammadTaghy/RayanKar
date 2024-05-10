using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{

    public class RabbitMQMessageModel
    {
        public string AssemblyFullName { get; set; }
        public string Body { get; set; }
        public int AggregateId { get; set; }
        public byte ChangedType { get; set; }
        public RabbitMQMessageModel(string assemblyFullName, string body, int aggregateId, byte changedType)
        {
            AssemblyFullName = assemblyFullName;
            Body = body;
            AggregateId = aggregateId;
            ChangedType = changedType;
        }


    }
    public class RabbitMQSendRequest
    {
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public string Message { get; set; }
        public RabbitMQSendRequest(string queueName, string routingKey, string message)
        {
            QueueName = queueName;
            RoutingKey = routingKey;
            Message = message;
        }


    }
    public class RabbitMQRecieveRequest
    {
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public EventHandler<BasicDeliverEventArgs> EventHandler { get; set; }
        public RabbitMQRecieveRequest(string queueName, string routingKey, EventHandler<BasicDeliverEventArgs> eventHandler)
        {
            QueueName = queueName;
            RoutingKey = routingKey;
            EventHandler = eventHandler;
        }

    }
}

