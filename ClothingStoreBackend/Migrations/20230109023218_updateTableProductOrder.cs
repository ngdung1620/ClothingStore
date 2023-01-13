using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingStoreBackend.Migrations
{
    public partial class updateTableProductOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ProductOrders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "ProductOrders");
        }
    }
}
