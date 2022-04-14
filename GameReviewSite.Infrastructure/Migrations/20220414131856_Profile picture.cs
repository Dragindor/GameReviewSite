using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewSite.Infrastructure.Migrations
{
    public partial class Profilepicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "User",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "User");
        }
    }
}
