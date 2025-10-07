using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yumsy_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSearchScoreToSearchCountAndTypeToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchScore",
                table: "ingredient");

            migrationBuilder.AddColumn<int>(
                name: "SearchCount",
                table: "ingredient",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchCount",
                table: "ingredient");

            migrationBuilder.AddColumn<decimal>(
                name: "SearchScore",
                table: "ingredient",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
