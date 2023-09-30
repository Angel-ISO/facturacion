using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id_Customer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customer_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer_address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer_phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id_Customer);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id_Product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    product_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    product_price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id_Product);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    id_Invoice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    invoice_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.id_Invoice);
                    table.ForeignKey(
                        name: "FK_invoices_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id_Customer",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoice_details",
                columns: table => new
                {
                    id_invDetail = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    subtotal = table.Column<int>(type: "int", nullable: false),
                    invoice_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice_details", x => x.id_invDetail);
                    table.ForeignKey(
                        name: "FK_invoice_details_invoices_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "invoices",
                        principalColumn: "id_Invoice",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invoice_details_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id_Product",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "id_Customer", "customer_address", "customer_name", "customer_phone" },
                values: new object[,]
                {
                    { 1, "145 QuintaSanisidro St", "Angel", "+1-1234460694" },
                    { 2, "145 LaArboleda st", "Ximenita", "+1-3496504055" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id_Product", "product_name", "product_price" },
                values: new object[,]
                {
                    { 1, "arroz", 2400 },
                    { 2, "leche", 2500 },
                    { 3, "jabon en polvo", 1500 },
                    { 4, "manzanaU", 2000 }
                });

            migrationBuilder.InsertData(
                table: "invoices",
                columns: new[] { "id_Invoice", "customer_id", "invoice_date", "total" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 650 },
                    { 2, 2, new DateTime(2023, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 950 },
                    { 3, 2, new DateTime(2023, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 750 },
                    { 4, 1, new DateTime(2023, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 550 },
                    { 5, 2, new DateTime(2023, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 850 }
                });

            migrationBuilder.InsertData(
                table: "invoice_details",
                columns: new[] { "id_invDetail", "invoice_id", "product_id", "quantity", "subtotal" },
                values: new object[,]
                {
                    { 1, 1, 1, 2, 200 },
                    { 2, 1, 2, 3, 300 },
                    { 3, 2, 1, 4, 400 },
                    { 4, 2, 3, 2, 300 },
                    { 5, 3, 2, 5, 750 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_invoice_details_invoice_id",
                table: "invoice_details",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_details_product_id",
                table: "invoice_details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_customer_id",
                table: "invoices",
                column: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "invoice_details");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
