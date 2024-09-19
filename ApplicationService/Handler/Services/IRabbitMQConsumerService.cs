using Domain.CaseAktifAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Handler.Services
{
    public interface IRabbitMQConsumerService
    {
        void ReceiveOrderMessages();
        void  SendEmail(string email, CustomerOrder order);
        void SendSms(string phoneNumber, CustomerOrder order);
    }
}
