using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BrainiacBattle.Data.Migrations
{
    public partial class AddTotalPLayingTimeandStartTimeforAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPLayingTime",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TotalPLayingTime",
                table: "Accounts");
        }
    }
}
