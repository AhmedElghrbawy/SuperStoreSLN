using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperStore.Data.Migrations
{
    public partial class ShoppingcartItemamountaddedandvalidationforamount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ShoppingCartItems");
        }
    }
}
