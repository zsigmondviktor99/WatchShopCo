using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_gyakorlas.Data.Migrations
{
    public partial class AddDescriptionToBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descreption",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descreption",
                table: "Brands");
        }
    }
}
