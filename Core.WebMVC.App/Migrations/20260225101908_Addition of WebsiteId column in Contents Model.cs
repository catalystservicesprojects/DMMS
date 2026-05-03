using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMMS.WebMVC.App.Migrations
{
    /// <inheritdoc />
    public partial class AdditionofWebsiteIdcolumninContentsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackLinkColor",
                table: "Website");

            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Website");

            migrationBuilder.DropColumn(
                name: "BorderColor",
                table: "Website");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "Website");

            migrationBuilder.DropColumn(
                name: "OpenIn",
                table: "Website");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "Website");

            migrationBuilder.DropColumn(
                name: "TextColor",
                table: "Website");

            migrationBuilder.AddColumn<int>(
                name: "WebsiteId",
                table: "Content",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Content_WebsiteId",
                table: "Content",
                column: "WebsiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Website_WebsiteId",
                table: "Content",
                column: "WebsiteId",
                principalTable: "Website",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Website_WebsiteId",
                table: "Content");

            migrationBuilder.DropIndex(
                name: "IX_Content_WebsiteId",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "WebsiteId",
                table: "Content");

            migrationBuilder.AddColumn<string>(
                name: "BackLinkColor",
                table: "Website",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Website",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BorderColor",
                table: "Website",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Website",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OpenIn",
                table: "Website",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "Website",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TextColor",
                table: "Website",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
