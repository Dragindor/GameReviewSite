﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewSite.Infrastructure.Migrations
{
    public partial class Gamepicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Games",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Games");
        }
    }
}
