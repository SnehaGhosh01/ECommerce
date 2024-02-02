using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApplication.Migrations
{
    /// <inheritdoc />
    public partial class Changeinorderdetailstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_orderDetailsForUsers_OrderDetailsForUserOrderId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_OrderDetailsForUserOrderId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "OrderDetailsForUserOrderId",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<string>(
                name: "OrderItems",
                table: "orderDetailsForUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PerPrice",
                table: "orderDetailsForUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderItems",
                table: "orderDetailsForUsers");

            migrationBuilder.DropColumn(
                name: "PerPrice",
                table: "orderDetailsForUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderDetailsForUserOrderId",
                table: "ShoppingCarts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_OrderDetailsForUserOrderId",
                table: "ShoppingCarts",
                column: "OrderDetailsForUserOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_orderDetailsForUsers_OrderDetailsForUserOrderId",
                table: "ShoppingCarts",
                column: "OrderDetailsForUserOrderId",
                principalTable: "orderDetailsForUsers",
                principalColumn: "OrderId");
        }
    }
}
