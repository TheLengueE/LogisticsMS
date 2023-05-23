using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsMS.Migrations
{
    /// <inheritdoc />
    public partial class AddRFS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReimbursementForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    申报人姓名 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    金额 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    申请时间 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    事由 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    审查人名字 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    状态 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReimbursementForm", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReimbursementForm");
        }
    }
}
