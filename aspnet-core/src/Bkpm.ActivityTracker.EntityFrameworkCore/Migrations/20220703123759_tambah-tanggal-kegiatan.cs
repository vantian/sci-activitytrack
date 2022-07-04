using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bkpm.ActivityTracker.Migrations
{
    public partial class tambahtanggalkegiatan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TanggalKegiatan",
                table: "ActivityDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TanggalKegiatan",
                table: "ActivityDetails");
        }
    }
}
