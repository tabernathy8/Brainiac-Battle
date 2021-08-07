using Microsoft.EntityFrameworkCore.Migrations;

namespace BrainiacBattle.Data.Migrations
{
    public partial class AddLinkBetweenAccountandCategorywithCategoryratings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameRating",
                table: "AccountGameStatistics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccountCategoryStatistics",
                columns: table => new
                {
                    AccountCategoryStaticId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CategoryRating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCategoryStatistics", x => x.AccountCategoryStaticId);
                    table.ForeignKey(
                        name: "FK_AccountCategoryStatistics_Accounts",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountCategoryStatistics_Games",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountCategoryStatistics_AccountId",
                table: "AccountCategoryStatistics",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCategoryStatistics_CategoryId",
                table: "AccountCategoryStatistics",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountCategoryStatistics");

            migrationBuilder.DropColumn(
                name: "GameRating",
                table: "AccountGameStatistics");
        }
    }
}
