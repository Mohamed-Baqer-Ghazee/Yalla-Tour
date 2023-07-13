using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yalla_Tour.Migrations
{
    /// <inheritdoc />
    public partial class addImgUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagesUrl",
                table: "Restaurant",
                newName: "ImgUrl");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Location",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Location");

            migrationBuilder.RenameColumn(
                name: "ImgUrl",
                table: "Restaurant",
                newName: "ImagesUrl");
        }
    }
}
