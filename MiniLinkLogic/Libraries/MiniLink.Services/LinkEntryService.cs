using Microsoft.Extensions.Caching.Memory;
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
        public async Task<LinkEntry> GetLinkEntryById(Guid? id, string ip = null)
        {
            if (id == Guid.Empty || !id.HasValue)
                return null;

            LinkEntry entry;

            if (_cache.TryGetValue(id, out entry))
            {

                await AddVisit(entry, ip);

                return entry;
            }

            entry = await _linkEntryRepository.GetByIdAsync(id);

            var cacheEntryOptions = new MemoryCacheEntryOptions()

           // Keep in cache for this time, reset time if accessed.
           .SetSlidingExpiration(TimeSpan.FromDays(1));

            if (entry != null)
            {
                _cache.Set(entry.Id, entry, cacheEntryOptions);
                await AddVisit(entry, ip);
            }
            return entry;
        }

        public async Task<OperationResult<LinkEntry>> AddLinkEntry(string url, string ipAddress)
        {
            var linkEntry = new LinkEntry(url, ipAddress);

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
