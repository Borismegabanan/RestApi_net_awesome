using System.Text;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Interfaces;
using RabbitMQ.Client;

namespace GMCS_RestApi.Domain.Providers
{
    public class RabbitMessagesProvider : IRabbitMessagesProvider
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task SendMessageToQueueAsync(string message)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "TestWcfPost",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                routingKey: "TestWcfPost",
                basicProperties: null,
                body: body);
        }
    }
}
