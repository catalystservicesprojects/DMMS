using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMMS.WebMVC.App.Migrations
{
    /// <inheritdoc />
    public partial class Additionofadditionalkeywordtypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LongTailKeywords",
                table: "ContentMetas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongTailKeywords",
                table: "ContentMetas");
        }
    }
}
