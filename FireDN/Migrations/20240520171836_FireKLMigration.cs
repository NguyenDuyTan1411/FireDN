using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDN.Migrations
{
    public partial class FireKLMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESP",
                columns: table => new
                {
                    Esp_ID = table.Column<int>(type: "int", nullable: false),
                    Esp_N = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESP", x => x.Esp_ID);
                });

            migrationBuilder.CreateTable(
                name: "FireD",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    FireD_ID = table.Column<int>(type: "int", nullable: true),
                    Statistic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReadTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireD", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Humi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Humi_ID = table.Column<int>(type: "int", nullable: true),
                    Statistic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReadTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    IDuser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Pass_word = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role_user = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HinhAnh = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Gmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NguoiDun__B6EF7F6C6B244AFB", x => x.IDuser);
                });

            migrationBuilder.CreateTable(
                name: "Sensor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Sensor_N = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Smoke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Smoke_ID = table.Column<int>(type: "int", nullable: true),
                    Statistic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReadTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smoke", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Temp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Temp_ID = table.Column<int>(type: "int", nullable: true),
                    Statistic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReadTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temp", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TG_Sensor",
                columns: table => new
                {
                    TG_SID = table.Column<int>(type: "int", nullable: true),
                    Esp_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_esp_id",
                        column: x => x.Esp_ID,
                        principalTable: "ESP",
                        principalColumn: "Esp_ID");
                    table.ForeignKey(
                        name: "FK_TG_SID",
                        column: x => x.TG_SID,
                        principalTable: "Sensor",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TG_ESP",
                columns: table => new
                {
                    Esp_ID = table.Column<int>(type: "int", nullable: true),
                    FireD_ID = table.Column<int>(type: "int", nullable: true),
                    Humi_ID = table.Column<int>(type: "int", nullable: true),
                    Temp_ID = table.Column<int>(type: "int", nullable: true),
                    Smoke_ID = table.Column<int>(type: "int", nullable: true),
                    Record = table.Column<DateTime>(type: "datetime", nullable: true),
                    Alerts = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ESP",
                        column: x => x.Esp_ID,
                        principalTable: "ESP",
                        principalColumn: "Esp_ID");
                    table.ForeignKey(
                        name: "FK_FireD",
                        column: x => x.FireD_ID,
                        principalTable: "FireD",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Humi",
                        column: x => x.Humi_ID,
                        principalTable: "Humi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Smoke",
                        column: x => x.Smoke_ID,
                        principalTable: "Smoke",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Temp",
                        column: x => x.Temp_ID,
                        principalTable: "Temp",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TG_ESP_Esp_ID",
                table: "TG_ESP",
                column: "Esp_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TG_ESP_FireD_ID",
                table: "TG_ESP",
                column: "FireD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TG_ESP_Humi_ID",
                table: "TG_ESP",
                column: "Humi_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TG_ESP_Smoke_ID",
                table: "TG_ESP",
                column: "Smoke_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TG_ESP_Temp_ID",
                table: "TG_ESP",
                column: "Temp_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TG_Sensor_Esp_ID",
                table: "TG_Sensor",
                column: "Esp_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TG_Sensor_TG_SID",
                table: "TG_Sensor",
                column: "TG_SID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "TG_ESP");

            migrationBuilder.DropTable(
                name: "TG_Sensor");

            migrationBuilder.DropTable(
                name: "FireD");

            migrationBuilder.DropTable(
                name: "Humi");

            migrationBuilder.DropTable(
                name: "Smoke");

            migrationBuilder.DropTable(
                name: "Temp");

            migrationBuilder.DropTable(
                name: "ESP");

            migrationBuilder.DropTable(
                name: "Sensor");
        }
    }
}
