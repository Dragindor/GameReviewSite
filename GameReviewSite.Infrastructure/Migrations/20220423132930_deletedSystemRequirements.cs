using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewSite.Infrastructure.Migrations
{
    public partial class deletedSystemRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemRequirements",
                table: "Games");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemRequirements",
                table: "Games",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }
    }
}
