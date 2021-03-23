using MiniLink.Shared.Pagination;
using MiniLinkLogic.Libraries.MiniLink.Core;
using MiniLinkLogic.Libraries.MiniLink.Core.Domain;
using System;
using System.Threading.Tasks;

namespace MiniLinkLogic.Libraries.MiniLink.Services
{
    public interface ILinkEntryService
    {
        Task<OperationResult<LinkEntry>> AddLinkEntry(string url, string ipAddress);
        Task<OperationResult<LinkEntryVisit>> AddVisit(LinkEntry entry, string ip);
        Task<OperationResult<LinkEntry>> DeleteEntry(Guid? id);
        Task<IPaginatedList<LinkEntry>> GetAllPaginated(int pageIndex, string searchString, string sortOrder);
        Task<LinkEntry> GetLinkEntryById(Guid? id);
        Task<int> GetVisitCount(Guid? id);
    }
}