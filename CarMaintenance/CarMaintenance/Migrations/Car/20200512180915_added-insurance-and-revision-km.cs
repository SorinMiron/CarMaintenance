using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarMaintenance.Migrations.Car
{
    public partial class addedinsuranceandrevisionkm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarPeriodicity_PeriodicityId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_PeriodicityId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "LastRevision",
                table: "Cars",
                newName: "LastRevisionDate");

            migrationBuilder.AlterColumn<int>(
                name: "PeriodicityId",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastInsurance",
                table: "Cars",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastRevisionKm",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsuranceMonths",
                table: "CarPeriodicity",
                nullable: true);

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

            migrationBuilder.DropIndex(
                name: "IX_Cars_PeriodicityId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "LastInsurance",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "LastRevisionKm",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "InsuranceMonths",
                table: "CarPeriodicity");

            migrationBuilder.RenameColumn(
                name: "LastRevisionDate",
                table: "Cars",
                newName: "LastRevision");

            migrationBuilder.AlterColumn<int>(
                name: "PeriodicityId",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PeriodicityId",
                table: "Cars",
                column: "PeriodicityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarPeriodicity_PeriodicityId",
                table: "Cars",
                column: "PeriodicityId",
                principalTable: "CarPeriodicity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
