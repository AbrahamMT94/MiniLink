using Microsoft.EntityFrameworkCore;
using MiniLinkLogic.Libraries.MiniLink.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLinkLogic.Libraries.MiniLink.Data.Context
{
    public class MiniLinkContext : DbContext
    {

        public DbContext Instance => this;

        public MiniLinkContext(DbContextOptions<MiniLinkContext> options): base(options)
        {

        }


        public virtual DbSet<LinkEntry> LinkEntries { get; set; }
        public virtual DbSet<LinkEntryVisit> LinkEntryVisits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Define the primary key and cluster by it to take advantage of Sequential guids
            modelBuilder.Entity<LinkEntry>()
                        .HasKey(m => m.Id)
                        .IsClustered(true);

            // The mas length allowed by most browsers is 2000
            modelBuilder.Entity<LinkEntry>()
                .Property(m => m.URL)
                .HasMaxLength(2000);

            // set default to 0 and limit the highest number to max int
            modelBuilder.Entity<LinkEntry>()
                .Property(m => m.Visits)
                .HasDefaultValue(0)
                .HasMaxLength(int.MaxValue);      
            
            modelBuilder.Entity<LinkEntry>()
               .Property(m => m.IpAdress)
               .HasMaxLength(50)
               .IsRequired(true);

            modelBuilder.Entity<LinkEntry>()
              .Property(m => m.DateAdded)
              .IsRequired(true);

            modelBuilder.Entity<LinkEntry>()
                .HasIndex(m => m.DateAdded);

            modelBuilder.Entity<LinkEntry>()
               .Property(m => m.Base64Id)
               .HasMaxLength(22)
               .IsRequired(true);

            modelBuilder.Entity<LinkEntry>()
               .HasIndex(m => m.Base64Id);
            // Define the primary key and cluster by it to take advantage of Sequential guids
            modelBuilder.Entity<LinkEntryVisit>()
               .HasKey(m => m.Id)
               .IsClustered(true);

            modelBuilder.Entity<LinkEntryVisit>()
              .Property(m => m.LinkEntryId)
              .IsRequired(true);

            modelBuilder.Entity<LinkEntryVisit>()
             .Property(m => m.TimeStamp)
             .IsRequired(true);

            modelBuilder.Entity<LinkEntryVisit>()
                .HasIndex(m => m.LinkEntryId);

            modelBuilder.Entity<LinkEntryVisit>()
                .HasIndex(m => m.TimeStamp);

            // ip address usually in ip v6 we'll have 4 sets of chars separated by colons so about 39 chars
            // we'll make it 50 in case there is anything unusual
            modelBuilder.Entity<LinkEntryVisit>()
                .Property(m => m.VisitorIPAdress)
                .HasMaxLength(50)
                .IsRequired(true);

            

        }

    }
}
