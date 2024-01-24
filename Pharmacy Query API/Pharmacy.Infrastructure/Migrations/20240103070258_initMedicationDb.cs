using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    public partial class initMedicationDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Pharmacy",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedUtc", "ModifiedUtc" },
                values: new object[] { new DateTime(2024, 1, 3, 14, 2, 57, 740, DateTimeKind.Local).AddTicks(5454), new DateTime(2024, 1, 3, 14, 2, 57, 740, DateTimeKind.Local).AddTicks(5454) });

            migrationBuilder.UpdateData(
                table: "Pharmacy",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedUtc", "ModifiedUtc" },
                values: new object[] { new DateTime(2024, 1, 3, 14, 2, 57, 740, DateTimeKind.Local).AddTicks(5467), new DateTime(2024, 1, 3, 14, 2, 57, 740, DateTimeKind.Local).AddTicks(5467) });

            migrationBuilder.UpdateData(
                table: "Pharmacy",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedUtc", "ModifiedUtc" },
                values: new object[] { new DateTime(2024, 1, 3, 14, 2, 57, 740, DateTimeKind.Local).AddTicks(5469), new DateTime(2024, 1, 3, 14, 2, 57, 740, DateTimeKind.Local).AddTicks(5469) });

            migrationBuilder.UpdateData(
                table: "Pharmacy",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedUtc", "ModifiedUtc" },
                values: new object[] { new DateTime(2024, 1, 3, 14, 2, 57, 740, DateTimeKind.Local).AddTicks(5470), new DateTime(2024, 1, 3, 14, 2, 57, 740, DateTimeKind.Local).AddTicks(5470) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medication");

            migrationBuilder.UpdateData(
                table: "Pharmacy",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedUtc", "ModifiedUtc" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5045), new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5045) });

            migrationBuilder.UpdateData(
                table: "Pharmacy",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedUtc", "ModifiedUtc" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5097), new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5097) });

            migrationBuilder.UpdateData(
                table: "Pharmacy",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedUtc", "ModifiedUtc" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5098), new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5098) });

            migrationBuilder.UpdateData(
                table: "Pharmacy",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedUtc", "ModifiedUtc" },
                values: new object[] { new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5099), new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5099) });
        }
    }
}
