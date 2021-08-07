using Microsoft.EntityFrameworkCore.Migrations;

namespace BrainiacBattle.Data.Migrations
{
    public partial class Fixspellingsofids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountGameStatistics",
                table: "AccountGameStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountCategoryStatistics",
                table: "AccountCategoryStatistics");

            migrationBuilder.DropColumn(
                name: "AccountGameStaticId",
                table: "AccountGameStatistics");

            migrationBuilder.DropColumn(
                name: "AccountCategoryStaticId",
                table: "AccountCategoryStatistics");

            migrationBuilder.AddColumn<int>(
                name: "AccountGameStatisticId",
                table: "AccountGameStatistics",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AccountCategoryStatisticId",
                table: "AccountCategoryStatistics",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountGameStatistics",
                table: "AccountGameStatistics",
                column: "AccountGameStatisticId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountCategoryStatistics",
                table: "AccountCategoryStatistics",
                column: "AccountCategoryStatisticId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountGameStatistics",
                table: "AccountGameStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountCategoryStatistics",
                table: "AccountCategoryStatistics");

            migrationBuilder.DropColumn(
                name: "AccountGameStatisticId",
                table: "AccountGameStatistics");

            migrationBuilder.DropColumn(
                name: "AccountCategoryStatisticId",
                table: "AccountCategoryStatistics");

            migrationBuilder.AddColumn<int>(
                name: "AccountGameStaticId",
                table: "AccountGameStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AccountCategoryStaticId",
                table: "AccountCategoryStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountGameStatistics",
                table: "AccountGameStatistics",
                column: "AccountGameStaticId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountCategoryStatistics",
                table: "AccountCategoryStatistics",
                column: "AccountCategoryStaticId");
        }
    }
}
