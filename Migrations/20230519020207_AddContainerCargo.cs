using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsMS.Migrations
{
    /// <inheritdoc />
    public partial class AddContainerCargo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "UserRoles",
                newName: "密码");

            migrationBuilder.RenameColumn(
                name: "account",
                table: "UserRoles",
                newName: "账号");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "UserRoles",
                newName: "用户角色");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "UserRoles",
                newName: "电话号码");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserRoles",
                newName: "姓名");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "UserRoles",
                newName: "邮箱");

            migrationBuilder.CreateTable(
                name: "ContainerCargo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    发货点 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    收货点 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    发货城市 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    收货城市 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    托运人名字 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    托运人电话 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    托运人地址 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    托运人邮箱 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    收货人名字 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    收货人电话 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    收货人地址 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    收货人邮箱 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    运货名称 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    集装箱类型 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    集装箱数量 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    集装箱号码 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    施封号码 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    运输费用 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    托用人记载 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    其它附件 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    货物价格 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    运输员备注 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerCargo", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerCargo");

            migrationBuilder.RenameColumn(
                name: "邮箱",
                table: "UserRoles",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "账号",
                table: "UserRoles",
                newName: "account");

            migrationBuilder.RenameColumn(
                name: "电话号码",
                table: "UserRoles",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "用户角色",
                table: "UserRoles",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "密码",
                table: "UserRoles",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "姓名",
                table: "UserRoles",
                newName: "Name");
        }
    }
}
