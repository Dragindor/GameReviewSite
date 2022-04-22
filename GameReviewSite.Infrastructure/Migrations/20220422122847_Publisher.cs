using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewSite.Infrastructure.Migrations
{
    public partial class Publisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "publisher",
                table: "Games",
                newName: "Publisher");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Publisher",
                table: "Games",
                newName: "publisher");
        }
    }
}
