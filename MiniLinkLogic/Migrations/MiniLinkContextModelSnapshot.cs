﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniLinkLogic.Libraries.MiniLink.Data.Context;

namespace MiniLinkLogic.Migrations
{
    [DbContext(typeof(MiniLinkContext))]
    partial class MiniLinkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MiniLinkLogic.Libraries.MiniLink.Core.Domain.LinkEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IpAdress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("URL")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("Visits")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2147483647)
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id")
                        .IsClustered();

                    b.ToTable("LinkEntries");
                });

            modelBuilder.Entity("MiniLinkLogic.Libraries.MiniLink.Core.Domain.LinkEntryVisit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LinkEntryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("VisitorIPAdress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .IsClustered();

                    b.ToTable("LinkEntryVisits");
                });
#pragma warning restore 612, 618
        }
    }
}
