using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gateways.Data.Migrations
{
    public partial class UpdatePeripheralDevices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PeripheralDevices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PeripheralDevices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PeripheralDevices_IsDeleted",
                table: "PeripheralDevices",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PeripheralDevices_IsDeleted",
                table: "PeripheralDevices");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PeripheralDevices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PeripheralDevices");
        }
    }
}
