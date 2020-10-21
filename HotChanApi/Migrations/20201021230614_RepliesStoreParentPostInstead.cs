using Microsoft.EntityFrameworkCore.Migrations;

namespace HotChanApi.Migrations
{
    public partial class RepliesStoreParentPostInstead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "subPosts",
                table: "Posts");

            migrationBuilder.AddColumn<long>(
                name: "parentPostGet",
                table: "Replies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "parentPostGet",
                table: "Replies");

            migrationBuilder.AddColumn<string>(
                name: "subPosts",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
