using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApplication.Migrations
{
    /// <inheritdoc />
    public partial class ExtendIdentityUserwithvenderLicence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VenderLicenceNumber",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VenderLicenceNumber",
                table: "AspNetUsers");
        }
    }
}
