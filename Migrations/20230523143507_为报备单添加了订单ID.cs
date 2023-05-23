using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsMS.Migrations
{
    /// <inheritdoc />
    public partial class 为报备单添加了订单ID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "送货单id",
                table: "ReimbursementForm",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ReimbursementForm_送货单id",
                table: "ReimbursementForm",
                column: "送货单id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReimbursementForm_ContainerCargo_送货单id",
                table: "ReimbursementForm",
                column: "送货单id",
                principalTable: "ContainerCargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReimbursementForm_ContainerCargo_送货单id",
                table: "ReimbursementForm");

            migrationBuilder.DropIndex(
                name: "IX_ReimbursementForm_送货单id",
                table: "ReimbursementForm");

            migrationBuilder.DropColumn(
                name: "送货单id",
                table: "ReimbursementForm");
        }
    }
}
