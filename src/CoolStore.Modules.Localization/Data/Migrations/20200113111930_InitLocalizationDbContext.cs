using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoolStore.Modules.Localization.Data.Migrations
{
    public partial class InitLocalizationDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "localization");

            migrationBuilder.CreateTable(
                name: "Cultures",
                schema: "localization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                schema: "localization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    CultureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Cultures_CultureId",
                        column: x => x.CultureId,
                        principalSchema: "localization",
                        principalTable: "Cultures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CultureId",
                schema: "localization",
                table: "Resources",
                column: "CultureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources",
                schema: "localization");

            migrationBuilder.DropTable(
                name: "Cultures",
                schema: "localization");
        }
    }
}
