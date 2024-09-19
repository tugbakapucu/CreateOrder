namespace ApplicationService.Handler.Services
{
    public interface IRabbitMQProducerService
    {
        void SendOrderMessage(string message);
    }
}
