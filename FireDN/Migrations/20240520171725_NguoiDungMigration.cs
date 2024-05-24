using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDN.Migrations
{
    public partial class NguoiDungMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gmail",
                table: "NguoiDung",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "HinhAnh",
                table: "NguoiDung",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gmail",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "NguoiDung");
        }
    }
}
