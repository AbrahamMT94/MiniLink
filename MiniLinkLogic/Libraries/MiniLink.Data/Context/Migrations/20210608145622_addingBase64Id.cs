using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniLinkLogic.Migrations
{
    public partial class addingBase64Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64Id",
                table: "LinkEntries",
                type: "nvarchar(22)",
                maxLength: 22,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LinkEntries_Base64Id",
                table: "LinkEntries",
                column: "Base64Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LinkEntries_Base64Id",
                table: "LinkEntries");

            migrationBuilder.DropColumn(
                name: "Base64Id",
                table: "LinkEntries");
        }
    }
}
