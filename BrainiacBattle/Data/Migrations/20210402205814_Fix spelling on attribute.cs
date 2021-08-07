using Microsoft.EntityFrameworkCore.Migrations;

namespace BrainiacBattle.Data.Migrations
{
    public partial class Fixspellingonattribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPLayingTime",
                table: "Accounts",
                newName: "TotalPlayingTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPlayingTime",
                table: "Accounts",
                newName: "TotalPLayingTime");
        }
    }
}
