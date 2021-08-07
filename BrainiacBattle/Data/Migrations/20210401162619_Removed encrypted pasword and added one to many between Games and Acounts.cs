using Microsoft.EntityFrameworkCore.Migrations;

namespace BrainiacBattle.Data.Migrations
{
    public partial class RemovedencryptedpaswordandaddedonetomanybetweenGamesandAcounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptedPassword",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "CurrentGameId",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrentGameId",
                table: "Accounts",
                column: "CurrentGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Games",
                table: "Accounts",
                column: "CurrentGameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Games",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CurrentGameId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CurrentGameId",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "EncryptedPassword",
                table: "Accounts",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
