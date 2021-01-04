using Microsoft.EntityFrameworkCore.Migrations;

namespace HotChanApi.Migrations
{
    public partial class PostReplyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isArchived",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "isPruned",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "Replies",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Replies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "mediaUrl",
                table: "Replies",
                newName: "MediaUrl");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "Replies",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "parentPostGet",
                table: "Replies",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "flags",
                table: "Replies",
                newName: "MediaThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "get",
                table: "Replies",
                newName: "ReplyId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "Posts",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Posts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "mediaUrl",
                table: "Posts",
                newName: "MediaUrl");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "Posts",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "flags",
                table: "Posts",
                newName: "Tags");

            migrationBuilder.RenameColumn(
                name: "board",
                table: "Posts",
                newName: "MediaThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "get",
                table: "Posts",
                newName: "PostId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Posts_PostId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_PostId",
                table: "Replies");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Replies",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Replies",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MediaUrl",
                table: "Replies",
                newName: "mediaUrl");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Replies",
                newName: "comment");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Replies",
                newName: "parentPostGet");

            migrationBuilder.RenameColumn(
                name: "MediaThumbnailUrl",
                table: "Replies",
                newName: "flags");

            migrationBuilder.RenameColumn(
                name: "ReplyId",
                table: "Replies",
                newName: "get");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Posts",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Posts",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Posts",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MediaUrl",
                table: "Posts",
                newName: "mediaUrl");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Posts",
                newName: "comment");

            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "Posts",
                newName: "flags");

            migrationBuilder.RenameColumn(
                name: "MediaThumbnailUrl",
                table: "Posts",
                newName: "board");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Posts",
                newName: "get");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isArchived",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPruned",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
