using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Infrastructure.Migrations
{
    public partial class initPharmacyDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Pharmacy",
                columns: new[] { "Id", "CreatedBy", "CreatedUtc", "IsDeleted", "ModifiedBy", "ModifiedUtc", "Name", "Address1", "Address2", "PostCode", "State", "Suburb", "Email", "NumberPhone" },
                values: new object[,]
                {
                    { 1, "Diem Khang", new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5045), false, "Diem Khang", new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5045), "ECO", "148 Hoang Hoa Tham, Ward.12, Tan Binh Dist., HCMC", "6 Nguyen van Cu, Ward.7, Tan Binh Dist., HCMC", "19010", "HCMC", "12 Dong Khoi, Ben Tre", "eco2020@gmail.com", "0979797979" },
                    { 2, "Diem Khang", new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5097), false, "Diem Khang", new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5097), "An Khang", "142 Vuon Chuoi, Ward.5, Dist.3, HCMC", "6 Nguyen van Cu, Ward.7, Tan Binh Dist., HCMC", "19320", "HCMC", "12 Dong Khoi, Ben Tre", "ankhang@gmail.com", "0989565656" },
                    { 3, "Diem Khang", new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5098), false, "Diem Khang", new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5098), "Pharmacity", "148 Hoang Hoa Tham, Ward.12, Dist.3, HCMC", "67 Vuon Chuoi, Ward.5, Dist.3, HCMC", "19300", "HCMC", "12 Dong Khoi, Ben Tre", "pharmacity@gmail.com", "0354545454" },
                    { 4, "Diem Khang", new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5099), false, "Diem Khang", new DateTime(2023, 12, 22, 8, 56, 33, 388, DateTimeKind.Local).AddTicks(5099), "Long Chau", "457 Nguyen Dinh Chieu, Ward.5, Dist.3, HCMC", "6 Nguyen van Cu, Ward.7, Tan Binh Dist., HCMC", "19040", "HCMC", "12 Dong Khoi, Ben Tre", "longchau@gmail.com", "0875757575" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pharmacy");
        }
    }
}
