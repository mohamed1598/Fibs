using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fibs.Data.Migrations
{
    public partial class ModifyingShoppingBag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductShoppingBag");

            migrationBuilder.AddColumn<DateTime>(
                name: "BuyingTime",
                table: "ShoppingBags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isCheckedOut",
                table: "ShoppingBags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_ProductId",
                table: "ShoppingBags",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBags_Products_ProductId",
                table: "ShoppingBags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBags_Products_ProductId",
                table: "ShoppingBags");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingBags_ProductId",
                table: "ShoppingBags");

            migrationBuilder.DropColumn(
                name: "BuyingTime",
                table: "ShoppingBags");

            migrationBuilder.DropColumn(
                name: "isCheckedOut",
                table: "ShoppingBags");

            migrationBuilder.CreateTable(
                name: "ProductShoppingBag",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingBagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShoppingBag", x => new { x.ProductId, x.ShoppingBagsId });
                    table.ForeignKey(
                        name: "FK_ProductShoppingBag_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductShoppingBag_ShoppingBags_ShoppingBagsId",
                        column: x => x.ShoppingBagsId,
                        principalTable: "ShoppingBags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductShoppingBag_ShoppingBagsId",
                table: "ProductShoppingBag",
                column: "ShoppingBagsId");
        }
    }
}
