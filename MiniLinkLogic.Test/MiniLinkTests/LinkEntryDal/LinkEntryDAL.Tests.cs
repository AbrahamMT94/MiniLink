using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MiniLinkLogic.Libraries.MiniLink.Core.Domain;
using MiniLinkLogic.Libraries.MiniLink.Data;
using MiniLinkLogic.Libraries.MiniLink.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniLink.Tests.MiniLinkTests
{
    /// <summary>
    /// While these set of tests seems pointless since since they basically test ef core 
    /// there might be a point where we might add filters 
    /// for multi user support with 
    /// userid columns so we will add and modify as needed.
    /// </summary>
    public class LinkEntryDALTests
    {
        private DbConnection _connection;
        public LinkEntryDALTests()
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
        public async Task TestInsert()
        {
            MiniLinkContext context = GetContext();
            IBaseRepository<LinkEntry> linkRepo = new BaseRepository<LinkEntry>(context);


            var entry = new LinkEntry("https://www.google.com/", "", DateTime.UtcNow);
            var entry2 = new LinkEntry("https://www.facebook.com/", "", DateTime.UtcNow);

            await linkRepo.InsertAsync(entry);
            await linkRepo.InsertAsync(entry2);
            var save = await linkRepo.SaveChangesAsync();

            Assert.Equal(2, save);

            context.Dispose();

        }

        [Fact]
        public async Task TestRead()
        {
            MiniLinkContext context = GetContext();
            IBaseRepository<LinkEntry> linkRepo = new BaseRepository<LinkEntry>(context);

            async Task ResetRepo()
            {
                context.Dispose();
                context = GetContext();
                linkRepo = new BaseRepository<LinkEntry>(context);
                return;
            }

            var entry = new LinkEntry("https://www.google.com/", "", DateTime.UtcNow);
            var entry2 = new LinkEntry("https://www.facebook.com/", "", DateTime.UtcNow);

            await linkRepo.InsertAsync(entry);
            await linkRepo.InsertAsync(entry2);
            var save = await linkRepo.SaveChangesAsync();

            await ResetRepo();

            var readEntry = await linkRepo.GetByIdAsync(entry.Id);

            Assert.Equal(entry.Id, readEntry.Id);
            Assert.Equal(entry.URL, readEntry.URL);

            var readAll = await linkRepo.GetAllAsync();

            Assert.Equal(2, readAll.Count);

            context.Dispose();
        }

        [Fact]
        public async Task TestUpdate()
        {
            MiniLinkContext context = GetContext();
            IBaseRepository<LinkEntry> linkRepo = new BaseRepository<LinkEntry>(context);

            async Task ResetRepo()
            {
                context.Dispose();
                context = GetContext();
                linkRepo = new BaseRepository<LinkEntry>(context);
                return;
            }

            var entry = new LinkEntry("https://www.google.com/", "", DateTime.UtcNow);
           
            await linkRepo.InsertAsync(entry);
           
            var save = await linkRepo.SaveChangesAsync();

            await ResetRepo();

            var readEntry = await linkRepo.GetByIdAsync(entry.Id);

            readEntry.Visits = 5;

            await linkRepo.UpdateAsync(readEntry);
            await linkRepo.SaveChangesAsync();

            await ResetRepo();

            var updatedEntry =await linkRepo.GetByIdAsync(entry.Id);

            Assert.Equal(readEntry.Visits, updatedEntry.Visits);
            Assert.True(entry.Visits != updatedEntry.Visits);

            context.Dispose();
        }

        [Fact]
        public async Task TestDelete()
        {
            MiniLinkContext context = GetContext();
            IBaseRepository<LinkEntry> linkRepo = new BaseRepository<LinkEntry>(context);

            async Task ResetRepo()
            {
                context.Dispose();
                context = GetContext();
                linkRepo = new BaseRepository<LinkEntry>(context);
                return;
            }

            var entry = new LinkEntry("https://www.google.com/", "", DateTime.UtcNow);

            await linkRepo.InsertAsync(entry);
            await linkRepo.SaveChangesAsync();

            await ResetRepo();

            var entryToDelete = await linkRepo.GetByIdAsync(entry.Id);

            await linkRepo.DeleteAsync(entryToDelete);
            await linkRepo.SaveChangesAsync();

            await ResetRepo();

            var deletedEntry = await linkRepo.GetByIdAsync(entry.Id);
            Assert.True(deletedEntry == default(LinkEntry));

            context.Dispose();
        }
    }
}
