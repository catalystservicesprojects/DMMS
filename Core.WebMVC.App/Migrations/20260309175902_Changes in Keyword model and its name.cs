using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMMS.WebMVC.App.Migrations
{
    /// <inheritdoc />
    public partial class ChangesinKeywordmodelanditsname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AdImpressionShare",
                table: "Keywords",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvgMonthlySearches",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Competition",
                table: "Keywords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionIndexedValue",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Keywords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "InAccount",
                table: "Keywords",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InPlan",
                table: "Keywords",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword",
                table: "Keywords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OrganicAveragePosition",
                table: "Keywords",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OrganicImpressionShare",
                table: "Keywords",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesApr2025",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesAug2025",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesDec2025",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesFeb2025",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesJan2026",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesJul2025",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesJun2025",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesMar2025",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesMay2025",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesNov2025",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesOct2025",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchesSep2025",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThreeMonthChange",
                table: "Keywords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TopOfPageBidHigh",
                table: "Keywords",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TopOfPageBidLow",
                table: "Keywords",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoYChange",
                table: "Keywords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdImpressionShare",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "AvgMonthlySearches",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "Competition",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "CompetitionIndexedValue",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "InAccount",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "InPlan",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "Keyword",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "OrganicAveragePosition",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "OrganicImpressionShare",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesApr2025",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesAug2025",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesDec2025",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesFeb2025",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesJan2026",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesJul2025",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesJun2025",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesMar2025",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesMay2025",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesNov2025",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesOct2025",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "SearchesSep2025",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "ThreeMonthChange",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "TopOfPageBidHigh",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "TopOfPageBidLow",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "YoYChange",
                table: "Keywords");
        }
    }
}
