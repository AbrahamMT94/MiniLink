using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniLinkLogic.Migrations
{
    public partial class addedDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "LinkEntries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_LinkEntryVisits_LinkEntryId",
                table: "LinkEntryVisits",
                column: "LinkEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkEntryVisits_TimeStamp",
                table: "LinkEntryVisits",
                column: "TimeStamp");

            migrationBuilder.CreateIndex(
                name: "IX_LinkEntries_DateAdded",
                table: "LinkEntries",
                column: "DateAdded");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LinkEntryVisits_LinkEntryId",
                table: "LinkEntryVisits");

            migrationBuilder.DropIndex(
                name: "IX_LinkEntryVisits_TimeStamp",
                table: "LinkEntryVisits");

            migrationBuilder.DropIndex(
                name: "IX_LinkEntries_DateAdded",
                table: "LinkEntries");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "LinkEntries");
        }
    }
}
