using Microsoft.Extensions.Caching.Memory;
using MiniLink.Shared.Pagination;
using MiniLinkLogic.Libraries.MiniLink.Core;
using MiniLinkLogic.Libraries.MiniLink.Core.Domain;
using MiniLinkLogic.Libraries.MiniLink.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLinkLogic.Libraries.MiniLink.Services
{
    public class LinkEntryService : ILinkEntryService
    {
        public IBaseRepository<LinkEntry> _linkEntryRepository;
        public IBaseRepository<LinkEntryVisit> _visitsRepository;

        private IMemoryCache _cache;

        public LinkEntryService(IBaseRepository<LinkEntry> linkEntryRepository, IBaseRepository<LinkEntryVisit> visitsRepository, IMemoryCache cache)
        {
            _linkEntryRepository = linkEntryRepository;
            _visitsRepository = visitsRepository;
            _cache = cache;
        }



        /// <summary>
        /// Gets the entry by id.
        /// This will be a hot route so we will add a cache here
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<LinkEntry> GetLinkEntryById(Guid? id, bool ignoreCache = false)
        {
            if (id == Guid.Empty || !id.HasValue)
                return null;

            LinkEntry entry;

            if (_cache.TryGetValue(id, out entry) && !ignoreCache)
            {
                return entry;
            }

            entry = await _linkEntryRepository.GetByIdAsync(id);

            await RefreshCount(entry);

            var cacheEntryOptions = new MemoryCacheEntryOptions()

           // Keep in cache for this time, reset time if accessed.
           .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            if (entry != null)
            {
                _cache.Set(entry.Id, entry, cacheEntryOptions);
            }
            return entry;
        }

        public async Task RefreshCount(LinkEntry entry)
        {
            entry.Visits = await GetVisitCount(entry.Id);

            await _linkEntryRepository.UpdateAsync(entry);
            await _linkEntryRepository.SaveChangesAsync();

        }

        public async Task<IPaginatedList<LinkEntry>> GetAllPaginated(int pageIndex, string searchString, string sortOrder)
        {
            return await _linkEntryRepository.GetAllPagedAsync(query =>
            {

                if (!string.IsNullOrEmpty(searchString))
                    query = query.Where(m => m.URL.Contains(searchString) || searchString.Contains(m.URL)).Distinct();

                switch (sortOrder)
                {
                    case "asc":
                        query = query.OrderBy(m => m.DateAdded);
                        break;
                    case "desc":
                        query = query.OrderByDescending(m => m.DateAdded);
                        break;
                    default:
                        query = query.OrderByDescending(m => m.DateAdded);
                        break;
                }

                return query;
            }, pageIndex, 100, false);
        }

        public async Task<int> GetVisitCount(Guid? id)
        {
            var result = await _visitsRepository.GetAllPagedAsync(query => query.Where(m => m.LinkEntryId == id), 1, 10, true);
            return result.TotalCount;
        }

        public async Task<OperationResult<LinkEntry>> AddLinkEntry(string url, string ipAddress)
        {
            var linkEntry = new LinkEntry(url, ipAddress, DateTime.UtcNow);

            var result = new OperationResult<LinkEntry>(linkEntry);

            // validate entry
            LinkEntryValidator.Validate(linkEntry, result);

            // if entry is invalid return errors
            if (!result.Success)
            {
                return result;
            }


            await _linkEntryRepository.InsertAsync(linkEntry);
            await _linkEntryRepository.SaveChangesAsync();

            return result;
        }

        public async Task<OperationResult<LinkEntryVisit>> AddVisit(LinkEntry entry, string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                ip = string.Empty;
            }

            var visit = new LinkEntryVisit()
            {
                LinkEntryId = entry.Id,
                TimeStamp = DateTime.UtcNow,
                VisitorIPAdress = ip
            };

            await _visitsRepository.InsertAsync(visit);
            await _visitsRepository.SaveChangesAsync();

            return new OperationResult<LinkEntryVisit>(visit);
        }

        public async Task<OperationResult<LinkEntry>> DeleteEntry(Guid? id)
        {
            if (id == Guid.Empty || !id.HasValue)
                throw new ArgumentNullException(nameof(id));

            var entry = await _linkEntryRepository.GetByIdAsync(id);

            if (entry is null)
            {
                throw new InvalidOperationException("Invalid entry");
            }

            var visits = await _visitsRepository.GetAllAsync(query => query.Where(m => m.LinkEntryId == id));

            await _linkEntryRepository.DeleteAsync(entry);
            await _visitsRepository.DeleteAsync(visits);

            await _linkEntryRepository.SaveChangesAsync();

            return new OperationResult<LinkEntry>(entry);
        }
    }
}
