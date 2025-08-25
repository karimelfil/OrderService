using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addtablesddfdffd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrder_Orders_OrderId",
                schema: "orders",
                table: "ItemsOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrder_orderrs_OrderId",
                schema: "orders",
                table: "ItemsOrder",
                column: "OrderId",
                principalSchema: "orders",
                principalTable: "orderrs",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrder_orderrs_OrderId",
                schema: "orders",
                table: "ItemsOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrder_Orders_OrderId",
                schema: "orders",
                table: "ItemsOrder",
                column: "OrderId",
                principalSchema: "orders",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
