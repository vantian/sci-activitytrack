﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bkpm.ActivityTracker.Migrations
{
    public partial class adddeskripsitolog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Deskripsi",
                table: "ActivityLog",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deskripsi",
                table: "ActivityLog");
        }
    }
}
