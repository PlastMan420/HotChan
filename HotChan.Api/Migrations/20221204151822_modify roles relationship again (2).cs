using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotChan.Api.Migrations
{
    /// <inheritdoc />
    public partial class modifyrolesrelationshipagain2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "eRoleId",
                table: "AspNetRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "eRoleId",
                table: "AspNetRoles",
                type: "integer",
                nullable: true);
        }
    }
}
