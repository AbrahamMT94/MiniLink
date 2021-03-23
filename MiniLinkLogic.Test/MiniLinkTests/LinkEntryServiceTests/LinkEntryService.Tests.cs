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

        public MiniLinkContext GetContext()
        {

            var options = new DbContextOptionsBuilder<MiniLinkContext>().UseSqlite(_connection).Options;
            var context = new MiniLinkContext(options);
            context.Database.EnsureCreated();
            return context;
        }
        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        [Fact]
        public async Task TestInsertAndGetById()
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


           linkEntryService = await SetupAsync();

            var fetch = await linkEntryService.GetLinkEntryById(add.Entry.Id,true);

            Assert.Equal(fetch.URL, url);

        }

        [Fact]
        public async Task TestAddVisitAndCount()
        {

            MiniLinkContext context = GetContext();
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

         

            var fetch = await linkEntryService.GetLinkEntryById(add.Entry.Id, true);

            var addVisit = await linkEntryService.AddVisit(fetch,"");

            Assert.True(addVisit.Success);

            for(var i = 0; i<100; i++)
                await linkEntryService.AddVisit(fetch, "");

            linkEntryService = await SetupAsync();

            var entryWithUpdatedCount = await linkEntryService.GetLinkEntryById(add.Entry.Id, true);


            Assert.Equal(101, entryWithUpdatedCount.Visits);
        }

    }
}
