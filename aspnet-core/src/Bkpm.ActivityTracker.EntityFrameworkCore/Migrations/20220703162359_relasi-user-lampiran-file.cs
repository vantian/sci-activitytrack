using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bkpm.ActivityTracker.Migrations
{
    public partial class relasiuserlampiranfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ActivityFiles_CreatorUserId",
                table: "ActivityFiles",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityFiles_AbpUsers_CreatorUserId",
                table: "ActivityFiles",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityFiles_AbpUsers_CreatorUserId",
                table: "ActivityFiles");

            migrationBuilder.DropIndex(
                name: "IX_ActivityFiles_CreatorUserId",
                table: "ActivityFiles");
        }
    }
}
