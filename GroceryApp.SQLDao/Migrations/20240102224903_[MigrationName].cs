using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warczynski.Zbaszyniak.GroceryApp.DAOMock1.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroceryEntityId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_GroceryEntityId",
                table: "Products",
                column: "GroceryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Groceries_GroceryEntityId",
                table: "Products",
                column: "GroceryEntityId",
                principalTable: "Groceries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Groceries_GroceryEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_GroceryEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GroceryEntityId",
                table: "Products");
        }
    }
}
