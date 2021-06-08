using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MiniLinkLogic.Libraries.MiniLink.Core.Domain;
using MiniLinkLogic.Libraries.MiniLink.Data;
using MiniLinkLogic.Libraries.MiniLink.Data.Context;
using MiniLinkLogic.Libraries.MiniLink.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniLink.Tests.MiniLinkTests
{
    public class LinkEntryServiceTests
    {
        private DbConnection _connection;

        public LinkEntryServiceTests()
        {
            _connection = CreateInMemoryDatabase();
        }

        /// <summary>
        /// Generates an in memory context for testing
        /// </summary>
        /// <returns></returns>
        public MiniLinkContext GetContext()
        {
            var options = new DbContextOptionsBuilder<MiniLinkContext>().UseSqlite(_connection).Options;
            var context = new MiniLinkContext(options);
            context.Database.EnsureCreated();

            return context;
        }

        /// <summary>
        /// Generates a connection for our context to use
        /// </summary>
        /// <returns></returns>
        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        /// <summary>
        /// Tests insert and get
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestInsertAndGetByBase64Id()
        {

            MiniLinkContext context = GetContext();
            IBaseRepository<LinkEntry> linkRepo;
            IBaseRepository<LinkEntryVisit> linkVisitRepo;

            MemoryCacheOptions options;
            IMemoryCache cache;

            async Task<ILinkEntryService> SetupAsync() {
                context = GetContext();

                linkRepo = new BaseRepository<LinkEntry>(context);
                linkVisitRepo = new BaseRepository<LinkEntryVisit>(context);

                var options = new MemoryCacheOptions();
                IMemoryCache cache = new MemoryCache(options);

                return new LinkEntryService(linkRepo, linkVisitRepo, cache);
            }

         


            ILinkEntryService linkEntryService = await SetupAsync();
            var url = "https://www.google.com/";
            var add = await linkEntryService.AddLinkEntry(url,"");

            Assert.True(add.Success);

            // reset context to ensue ef doesnt keep the results
            context.Dispose();

            linkEntryService = await SetupAsync();

            var fetch = await linkEntryService.GetLinkEntryByBase64Id(add.Entry.Base64Id,true);

            Assert.Equal(fetch.URL, url);

            context.Dispose();
        }


        /// <summary>
        /// Tests adding a visit and result caching
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestAddVisitAndCount()
        {

            MiniLinkContext context;
            IBaseRepository<LinkEntry> linkRepo;
            IBaseRepository<LinkEntryVisit> linkVisitRepo;

            MemoryCacheOptions options;
            IMemoryCache cache;

            async Task<ILinkEntryService> SetupAsync()
            {
                context = GetContext();
                linkRepo = new BaseRepository<LinkEntry>(context);
                linkVisitRepo = new BaseRepository<LinkEntryVisit>(context);

                var options = new MemoryCacheOptions();
                IMemoryCache cache = new MemoryCache(options);


                return new LinkEntryService(linkRepo, linkVisitRepo, cache);
            }

            ILinkEntryService linkEntryService = await SetupAsync();


            var url = "https://www.google.com/";

            var add = await linkEntryService.AddLinkEntry(url, "");

            var addedEntry = await linkEntryService.GetLinkEntryByBase64Id(add.Entry.Base64Id, true);

            var addVisit = await linkEntryService.AddVisit(addedEntry,"");

            Assert.True(addVisit.Success);

            var entry = await linkEntryService.GetLinkEntryByBase64Id(add.Entry.Base64Id,true);

            // first visit
            Assert.Equal(1, entry.Visits);

            for (var i = 0; i<100; i++)
                await linkEntryService.AddVisit(addedEntry, "");


            //first visit is shown since we cache on read
            var cachedEntry = await linkEntryService.GetLinkEntryByBase64Id(add.Entry.Base64Id);

          
            Assert.Equal(1, cachedEntry.Visits);

            // after setting ignore cache true we should get the actual count.
            var updatedEntry = await linkEntryService.GetLinkEntryByBase64Id(add.Entry.Base64Id,true);

            Assert.Equal(101, updatedEntry.Visits);

            context.Dispose();
        }

    }
}
