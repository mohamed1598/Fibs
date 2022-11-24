using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fibs.Data.Migrations
{
    public partial class UpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShoppingBags_UserId",
                table: "ShoppingBags");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_UserId",
                table: "ShoppingBags",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShoppingBags_UserId",
                table: "ShoppingBags");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_UserId",
                table: "ShoppingBags",
                column: "UserId");
        }
    }
}
