using RabbitMQ.Client;
using System.Text;

namespace ApplicationService.Handler.Services
{
    public class RabbitMQProducerService
    {
        ///Bunlar configden alınabilir şimdilik static yazdım.
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "orderQueue";
        private IConnection _connection;

        public RabbitMQProducerService()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            _connection = factory.CreateConnection();
        }

        public void SendOrderMessage(string message)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}
