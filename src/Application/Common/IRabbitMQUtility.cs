using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Model;

namespace Application.Common
{
    public interface IRabbitMQUtility
    {
        void RecieveMessage(RabbitMQRecieveRequest rabbitMQRecieveRequest);
        bool SendMessage(RabbitMQSendRequest rabbitMQSendRequest);
    }
}
