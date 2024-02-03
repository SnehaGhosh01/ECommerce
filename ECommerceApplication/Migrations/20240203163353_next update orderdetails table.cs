using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApplication.Migrations
{
    /// <inheritdoc />
    public partial class nextupdateorderdetailstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "orderDetailsForUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "orderDetailsForUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "orderDetailsForUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "orderDetailsForUsers");
        }
    }
}
