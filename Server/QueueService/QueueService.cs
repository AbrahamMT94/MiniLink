using MassTransit;
using MiniLink.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniLink.Server.QueueServices
{
    public class QueueService : IQueueService
    {
        private readonly IBus _bus;
        public QueueService(IBus bus)
        {
            _bus = bus;
        }

        public async Task EnqueueVisit(Guid? entryId, string ip)
        {
            if (!entryId.HasValue || entryId == Guid.Empty)
                return;
           
                Uri uri = new Uri("rabbitmq://rabbitmq:5672/visitQueue");

            try
            {
                var endPoint = await _bus.GetSendEndpoint(uri);
                var visit = new LinkEntryVisitMessage()
                {
                    Id = entryId.Value,
                    Ip = ip
                };

                await endPoint.Send(visit);

            }
            catch (Exception)
            {
                //logging perhaps
                return;
            }
        }
    }
}
