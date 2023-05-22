using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdataShippingOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "送货人的电话号码",
                table: "ShippingOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "送货人的电话号码",
                table: "ShippingOrders");
        }
    }
}
