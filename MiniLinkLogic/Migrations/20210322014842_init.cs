using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniLinkLogic.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IpAdress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Visits = table.Column<int>(type: "int", maxLength: 2147483647, nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkEntries", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "LinkEntryVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitorIPAdress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LinkEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkEntryVisits", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkEntries");

            migrationBuilder.DropTable(
                name: "LinkEntryVisits");
        }
    }
}
