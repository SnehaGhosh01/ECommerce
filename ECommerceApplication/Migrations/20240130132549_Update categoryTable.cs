using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdatecategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "subCategory",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "subCategory",
                table: "Categories");
        }
    }
}
