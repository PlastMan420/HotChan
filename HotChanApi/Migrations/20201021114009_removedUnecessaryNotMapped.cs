using Microsoft.EntityFrameworkCore.Migrations;

namespace HotChanApi.Migrations
{
    public partial class removedUnecessaryNotMapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "subPosts",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "subPosts",
                table: "Posts");
        }
    }
}
