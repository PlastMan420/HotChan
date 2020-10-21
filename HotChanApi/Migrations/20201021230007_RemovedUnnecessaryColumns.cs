using Microsoft.EntityFrameworkCore.Migrations;

namespace HotChanApi.Migrations
{
    public partial class RemovedUnnecessaryColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "options",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "options",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "options",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "options",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
