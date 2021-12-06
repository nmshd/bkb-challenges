using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Challenges.Infrastructure.Persistence.Database.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(20)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "char(36)", nullable: true),
                    CreatedByDevice = table.Column<string>(type: "char(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Challenges");
        }
    }
}
