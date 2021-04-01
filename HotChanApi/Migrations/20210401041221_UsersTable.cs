using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotChanApi.Migrations
{
    public partial class UsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Posts_PostId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_PostId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "MediaThumbnailUrl",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Replies",
                newName: "AvatarThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Posts",
                newName: "PostTitle");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Posts",
                newName: "InternalReplyIds");

            migrationBuilder.RenameColumn(
                name: "MediaThumbnailUrl",
                table: "Posts",
                newName: "Description");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostId",
                table: "Replies",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReplyId",
                table: "Replies",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Replies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PostId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "UserDb",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalPostIds = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDb", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDb");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "AvatarThumbnailUrl",
                table: "Replies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PostTitle",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "InternalReplyIds",
                table: "Posts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Posts",
                newName: "MediaThumbnailUrl");

            migrationBuilder.AlterColumn<long>(
                name: "PostId",
                table: "Replies",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<long>(
                name: "ReplyId",
                table: "Replies",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "MediaThumbnailUrl",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PostId",
                table: "Posts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_PostId",
                table: "Replies",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Posts_PostId",
                table: "Replies",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
