using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop_NULL.Migrations
{
    public partial class AddFKFromShopToCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shops_CityName",
                table: "Shops",
                column: "CityName");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Cities_CityName",
                table: "Shops",
                column: "CityName",
                principalTable: "Cities",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Cities_CityName",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_CityName",
                table: "Shops");
        }
    }
}
