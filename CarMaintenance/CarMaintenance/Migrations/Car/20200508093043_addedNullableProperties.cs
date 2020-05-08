using Microsoft.EntityFrameworkCore.Migrations;

namespace CarMaintenance.Migrations.Car
{
    public partial class addedNullableProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VigMonths",
                table: "CarPeriodicity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RevisionMonths",
                table: "CarPeriodicity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RevisionKm",
                table: "CarPeriodicity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PtiMonths",
                table: "CarPeriodicity",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VigMonths",
                table: "CarPeriodicity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RevisionMonths",
                table: "CarPeriodicity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RevisionKm",
                table: "CarPeriodicity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PtiMonths",
                table: "CarPeriodicity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
