using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarMaintenance.Migrations.Car
{
    public partial class addedCarPeriodicity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodicityId",
                table: "Cars",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarPeriodicity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RevisionKm = table.Column<int>(nullable: false),
                    RevisionMonths = table.Column<int>(nullable: false),
                    PtiMonths = table.Column<int>(nullable: false),
                    VigMonths = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarPeriodicity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PeriodicityId",
                table: "Cars",
                column: "PeriodicityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarPeriodicity_PeriodicityId",
                table: "Cars",
                column: "PeriodicityId",
                principalTable: "CarPeriodicity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarPeriodicity_PeriodicityId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarPeriodicity");

            migrationBuilder.DropIndex(
                name: "IX_Cars_PeriodicityId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PeriodicityId",
                table: "Cars");
        }
    }
}
