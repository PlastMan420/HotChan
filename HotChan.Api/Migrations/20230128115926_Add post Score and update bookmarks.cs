using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotChan.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddpostScoreandupdatebookmarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_AspNetUsers_UserId",
                table: "Bookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Bookmarks_bookmarksId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_bookmarksId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks");

            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_UserId",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "SaltHash",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "bookmarksId",
                table: "Posts",
                newName: "BookmarksuserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Bookmarks",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bookmarks",
                newName: "BookmarkListId");

            migrationBuilder.AddColumn<Guid>(
                name: "BookmarksBookmarkListId",
                table: "Posts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks",
                columns: new[] { "userId", "BookmarkListId" });

            migrationBuilder.CreateTable(
                name: "PostScores",
                columns: table => new
                {
                    postId = table.Column<Guid>(type: "uuid", nullable: false),
                    userId = table.Column<Guid>(type: "uuid", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostScores", x => new { x.userId, x.postId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BookmarksuserId_BookmarksBookmarkListId",
                table: "Posts",
                columns: new[] { "BookmarksuserId", "BookmarksBookmarkListId" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_BookmarkListId_userId",
                table: "Bookmarks",
                columns: new[] { "BookmarkListId", "userId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostScores_postId_userId",
                table: "PostScores",
                columns: new[] { "postId", "userId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Bookmarks_BookmarksuserId_BookmarksBookmarkListId",
                table: "Posts",
                columns: new[] { "BookmarksuserId", "BookmarksBookmarkListId" },
                principalTable: "Bookmarks",
                principalColumns: new[] { "userId", "BookmarkListId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Bookmarks_BookmarksuserId_BookmarksBookmarkListId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "PostScores");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BookmarksuserId_BookmarksBookmarkListId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks");

            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_BookmarkListId_userId",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "BookmarksBookmarkListId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "BookmarksuserId",
                table: "Posts",
                newName: "bookmarksId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Bookmarks",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "BookmarkListId",
                table: "Bookmarks",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "SaltHash",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_bookmarksId",
                table: "Posts",
                column: "bookmarksId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId",
                table: "Bookmarks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_AspNetUsers_UserId",
                table: "Bookmarks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Bookmarks_bookmarksId",
                table: "Posts",
                column: "bookmarksId",
                principalTable: "Bookmarks",
                principalColumn: "Id");
        }
    }
}
