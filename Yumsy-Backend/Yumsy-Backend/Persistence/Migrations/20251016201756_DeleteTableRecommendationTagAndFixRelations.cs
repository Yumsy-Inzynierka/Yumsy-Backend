using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yumsy_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTableRecommendationTagAndFixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recommendation_tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recommendation",
                table: "recommendation");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "recommendation",
                newName: "TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recommendation",
                table: "recommendation",
                columns: new[] { "TagId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_recommendation_tag_TagId",
                table: "recommendation",
                column: "TagId",
                principalTable: "tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recommendation_tag_TagId",
                table: "recommendation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recommendation",
                table: "recommendation");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "recommendation",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recommendation",
                table: "recommendation",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "recommendation_tag",
                columns: table => new
                {
                    RecommendationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendation_tag", x => new { x.RecommendationId, x.TagId });
                    table.ForeignKey(
                        name: "FK_recommendation_tag_recommendation_RecommendationId",
                        column: x => x.RecommendationId,
                        principalTable: "recommendation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recommendation_tag_tag_TagId",
                        column: x => x.TagId,
                        principalTable: "tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recommendation_tag_TagId",
                table: "recommendation_tag",
                column: "TagId");
        }
    }
}
