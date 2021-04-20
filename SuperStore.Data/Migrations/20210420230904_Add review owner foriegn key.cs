using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperStore.Data.Migrations
{
    public partial class Addreviewownerforiegnkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OwnerId",
                table: "Reviews",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_OwnerId",
                table: "Reviews",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_OwnerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_OwnerId",
                table: "Reviews");
        }
    }
}
