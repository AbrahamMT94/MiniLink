using System;
using System.Threading.Tasks;

namespace MiniLink.Server.QueueServices
{
    public interface IQueueService
    {
        Task EnqueueVisit(Guid? entryId, string ip);
    }
}