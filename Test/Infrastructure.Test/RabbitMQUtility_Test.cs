using Application.Model;
using Common;
using Infrastructure.RabbitMQ;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Test
{
    public class RabbitMQUtility_Test
    {
        private readonly IOptions<AppSettings> option;
        private readonly RabbitMQUtility directQueue;

        public RabbitMQUtility_Test()
        {
            option = Options.Create<AppSettings>(new AppSettings(
                "F?Q2y+8bl5DQZJtw>fCY>}Z|Q=Ir#U?Y@o3(B)}[i~CK{<6yYUQn?!P6hYvhx><",
                "localhost")
           );
            directQueue = new RabbitMQUtility(option);
        }
        [Fact]
        public async Task PublishSub_CannotRecieveMessageInDifferentRouting_ResultTest()
        {
            string result = "test message";
            string recieveMessage = "";
            directQueue.RecieveMessage(new RabbitMQRecieveRequest(
                    "Test",
                    "",
                     (model, args) =>
                     {
                         var body = args.Body.ToArray();
                         recieveMessage = Encoding.UTF8.GetString(body);

                     }
                )
            );
            directQueue.SendMessage(new RabbitMQSendRequest(
                        "Test",
                        "Test_Routing",
                        result
                    ));
            await Task.Delay(5000);

            Assert.Equal("", recieveMessage);
        }
        [Fact]
        public async Task PublishSub_EmptyQueueTest_ResultTest()
        {
            string result = "test message";
            string recieveMessage = "";
            directQueue.RecieveMessage(new RabbitMQRecieveRequest(
                    "",
                    "Test_Message",
                     (model, args) =>
                     {
                         var body = args.Body.ToArray();
                         recieveMessage = Encoding.UTF8.GetString(body);

                     }
                ));
            directQueue.SendMessage(new RabbitMQSendRequest(
                        "",
                        "Test_Message",
                        result
                    ));
            await Task.Delay(5000);

            Assert.Equal(result, recieveMessage);
        }
        [Fact]
        public async Task PublishSub_SuccessfulTest_ResultTest()
        {
            string result = "test message";
            string recieveMessage = "";
            directQueue.RecieveMessage(new RabbitMQRecieveRequest(
                    "Test",
                    "Test_Message",
                     (model, args) =>
                     {
                         var body = args.Body.ToArray();
                         recieveMessage = Encoding.UTF8.GetString(body);

                     }
                ));
            directQueue.SendMessage(new RabbitMQSendRequest(
                        "Test",
                        "Test_Message",
                        result
                    ));
            await Task.Delay(5000);

            Assert.Equal(result, recieveMessage);
        }
    }
}
