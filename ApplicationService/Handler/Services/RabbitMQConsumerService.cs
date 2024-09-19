using Domain.CaseAktifAggregate;
using MimeKit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ApplicationService.Handler.Services
{
    public class RabbitMQConsumerService
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "orderNotifications";
        private IConnection _connection;

        public RabbitMQConsumerService()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            _connection = factory.CreateConnection();
        }

        public void ReceiveOrderMessages()
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var order = JsonConvert.DeserializeObject<CustomerOrder>(message); // Sipariş mesajını deserialization yapıyoruz

                    // E-posta gönderme
                    SendEmail(order.Customer.Email, order);

                    // SMS gönderme (simülasyon)
                    SendSms(order.Customer.PhoneNumber, order);

                    System.IO.File.AppendAllText("order_notifications.txt", message + "\n");
                };

                channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private void SendEmail(string email, CustomerOrder order)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Order Notification", "tugba_bm@outlook.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Sipariş oluşturuldu";

            message.Body = new TextPart("plain")
            {
                Text = $"Sevgili {order.Customer.Name},\n\nSiparişiniz başarılı bir şekilde oluşturuldu!\nSipariş ID: {order.Id}\nToplam Tutar: {order.Products.Sum(p => p.Price * p.Quantity)}\n\nTeşekkürler.!"
            };

            //using (var client = new SmtpClient())
            //{
            //    client.Connect("", 587, false); // Email SMTP server bilgileri
            //    client.Authenticate("", "");
            //    client.Send(message);
            //    client.Disconnect(true);
            //}
        }

        private void SendSms(string phoneNumber, CustomerOrder order)
        {
            Console.WriteLine($"SMS gönderildi {phoneNumber}: Sipariş Id {order.Id}");
        }
    }
}
