using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HurtowniaReptiGood.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceAddresses",
                columns: table => new
                {
                    InvoiceAddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerSurname = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    NIP = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAddresses", x => x.InvoiceAddressId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSymbol = table.Column<string>(nullable: false),
                    ProductName = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Photo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddresses",
                columns: table => new
                {
                    ShippingAddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeAddress = table.Column<string>(nullable: false),
                    Street = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddresses", x => x.ShippingAddressId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerSurname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    ShippingAddressId = table.Column<int>(nullable: false),
                    InvoiceAddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_InvoiceAddresses_InvoiceAddressId",
                        column: x => x.InvoiceAddressId,
                        principalTable: "InvoiceAddresses",
                        principalColumn: "InvoiceAddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_ShippingAddresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "ShippingAddresses",
                        principalColumn: "ShippingAddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderEntity",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    StateOrder = table.Column<string>(nullable: false),
                    StatusOrder = table.Column<string>(nullable: false),
                    DateOrder = table.Column<DateTime>(nullable: false),
                    ValueOrder = table.Column<double>(nullable: false),
                    OrderDetailId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntity", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderEntity_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailEntity",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    ProductSymbol = table.Column<string>(nullable: false),
                    ProductName = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailEntity", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetailEntity_OrderEntity_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderEntity",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailEntity_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_InvoiceAddressId",
                table: "Customers",
                column: "InvoiceAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ShippingAddressId",
                table: "Customers",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailEntity_OrderId",
                table: "OrderDetailEntity",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailEntity_ProductId",
                table: "OrderDetailEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_CustomerId",
                table: "OrderEntity",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_OrderDetailId",
                table: "OrderEntity",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntity_OrderDetailEntity_OrderDetailId",
                table: "OrderEntity",
                column: "OrderDetailId",
                principalTable: "OrderDetailEntity",
                principalColumn: "OrderDetailId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_InvoiceAddresses_InvoiceAddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ShippingAddresses_ShippingAddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailEntity_OrderEntity_OrderId",
                table: "OrderDetailEntity");

            migrationBuilder.DropTable(
                name: "InvoiceAddresses");

            migrationBuilder.DropTable(
                name: "ShippingAddresses");

            migrationBuilder.DropTable(
                name: "OrderEntity");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "OrderDetailEntity");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
