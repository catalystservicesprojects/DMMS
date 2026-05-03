using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMMS.WebMVC.App.Migrations
{
    /// <inheritdoc />
    public partial class additionofWebsiteContentTypeWebsiteContentTypeMapWebsiteTypeWebsiteConfigmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WebsiteType");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WebsiteType");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "WebsiteConfig",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "WebsiteConfig",
                type: "nvarchar(max)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<string>(
                name: "CharSet",
                table: "WebsiteConfig",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Collate",
                table: "WebsiteConfig",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Host",
                table: "WebsiteConfig",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WebsiteConfig",
                type: "nvarchar(max)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "WebsiteConfig",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "WebsiteConfig",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "WebsiteContentTypeMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteTypeId = table.Column<int>(type: "int", nullable: false),
                    WebsiteContentTypeId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Identity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Tenant = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteContentTypeMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebsiteContentTypeMaps_WebsiteType_WebsiteTypeId",
                        column: x => x.WebsiteTypeId,
                        principalTable: "WebsiteType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteContentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteTypeId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Identity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Tenant = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteContentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebsiteContentTypes_WebsiteType_WebsiteTypeId",
                        column: x => x.WebsiteTypeId,
                        principalTable: "WebsiteType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteContentTypeMaps_WebsiteTypeId",
                table: "WebsiteContentTypeMaps",
                column: "WebsiteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteContentTypes_WebsiteTypeId",
                table: "WebsiteContentTypes",
                column: "WebsiteTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentMetas");

            migrationBuilder.DropTable(
                name: "WebsiteContentTypeMaps");

            migrationBuilder.DropTable(
                name: "WebsiteContentTypes");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "WebsiteConfig");

            migrationBuilder.DropColumn(
                name: "CharSet",
                table: "WebsiteConfig");

            migrationBuilder.DropColumn(
                name: "Collate",
                table: "WebsiteConfig");

            migrationBuilder.DropColumn(
                name: "Host",
                table: "WebsiteConfig");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WebsiteConfig");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "WebsiteConfig");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "WebsiteConfig");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "WebsiteType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "WebsiteType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "WebsiteConfig",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("Relational:ColumnOrder", 0)
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
