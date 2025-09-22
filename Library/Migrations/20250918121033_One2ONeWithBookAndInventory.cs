using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class One2ONeWithBookAndInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventories_BookId",
                table: "Inventories");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_BookId",
                table: "Inventories",
                column: "BookId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventories_BookId",
                table: "Inventories");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_BookId",
                table: "Inventories",
                column: "BookId");
        }
    }
}
