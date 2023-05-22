using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsMS.Migrations
{
    /// <inheritdoc />
    public partial class AShippingOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShippingOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    送货单id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    车牌号 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    汽车类型 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    送货人名字 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    送货人Id = table.Column<int>(type: "int", nullable: false),
                    送货时间 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    订单状态 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingOrders_ContainerCargo_送货单id",
                        column: x => x.送货单id,
                        principalTable: "ContainerCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrders_送货单id",
                table: "ShippingOrders",
                column: "送货单id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShippingOrders");
        }
    }
}
