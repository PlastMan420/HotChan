using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotChanApi.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    get = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    options = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: false),
                    flags = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    board = table.Column<string>(nullable: true),
                    time = table.Column<DateTime>(nullable: false),
                    mediaUrl = table.Column<string>(nullable: true),
                    priority = table.Column<int>(nullable: false),
                    isHeadThread = table.Column<bool>(nullable: false),
                    isArchived = table.Column<bool>(nullable: false),
                    isPruned = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.get);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
