using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yalla_Tour.Migrations
{
    /// <inheritdoc />
    public partial class removedTypeMenuFromRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Menu",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Restaurant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Menu",
                table: "Restaurant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Restaurant",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
