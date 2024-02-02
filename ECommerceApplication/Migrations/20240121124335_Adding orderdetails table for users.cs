using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApplication.Migrations
{
    /// <inheritdoc />
    public partial class Addingorderdetailstableforusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderDetailsForUserOrderId",
                table: "ShoppingCarts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderDetailsForUsers",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailsForUsers", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderDetailsForUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_OrderDetailsForUserOrderId",
                table: "ShoppingCarts",
                column: "OrderDetailsForUserOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailsForUsers_UserId",
                table: "OrderDetailsForUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_OrderDetailsForUsers_OrderDetailsForUserOrderId",
                table: "ShoppingCarts",
                column: "OrderDetailsForUserOrderId",
                principalTable: "OrderDetailsForUsers",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_OrderDetailsForUsers_OrderDetailsForUserOrderId",
                table: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "OrderDetailsForUsers");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_OrderDetailsForUserOrderId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "OrderDetailsForUserOrderId",
                table: "ShoppingCarts");
        }
    }
}
