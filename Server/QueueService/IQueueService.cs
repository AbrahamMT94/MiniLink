using System;
using System.Threading.Tasks;

namespace MiniLink.Server.QueueService
{
    public interface IQueueService
    {
        Task EnqueueVisit(Guid? entryId, string ip);
    }
}