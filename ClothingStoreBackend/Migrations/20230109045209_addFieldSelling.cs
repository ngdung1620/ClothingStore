using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingStoreBackend.Migrations
{
    public partial class addFieldSelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Selling",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selling",
                table: "Products");
        }
    }
}
