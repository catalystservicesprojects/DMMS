using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMMS.WebMVC.App.Migrations
{
    /// <inheritdoc />
    public partial class AdditionofintroductioncolumninContentAi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "ContentAi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "ContentAi");
        }
    }
}
