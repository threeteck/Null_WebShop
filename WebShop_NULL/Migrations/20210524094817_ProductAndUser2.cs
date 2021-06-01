using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop_NULL.Migrations
{
    public partial class ProductAndUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Products_ProductId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProductId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProductId",
                table: "Users",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Products_ProductId",
                table: "Users",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
