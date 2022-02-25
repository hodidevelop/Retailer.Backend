using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Retailer.Backend.Dal.OrderService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    DeliveryAddress = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    UnitPrice = table.Column<double>(type: "REAL", nullable: false),
                    Quantity = table.Column<double>(type: "REAL", nullable: false),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
