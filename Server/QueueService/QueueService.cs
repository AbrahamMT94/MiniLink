using MassTransit;
using MiniLink.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniLink.Server.QueueService
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
            try
            {
                Uri uri = new Uri("rabbitmq://rabbitmq:5672/visitQueue");

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


            }
        }
    }
}
