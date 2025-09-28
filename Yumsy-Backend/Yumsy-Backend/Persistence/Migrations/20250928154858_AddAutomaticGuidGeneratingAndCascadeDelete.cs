using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yumsy_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAutomaticGuidGeneratingAndCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_post_UserId",
                table: "comment");

            migrationBuilder.DropIndex(
                name: "IX_comment_UserId",
                table: "comment");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_post_PostId",
                table: "comment",
                column: "PostId",
                principalTable: "post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_post_PostId",
                table: "comment");

            migrationBuilder.CreateIndex(
                name: "IX_comment_UserId",
                table: "comment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_post_UserId",
                table: "comment",
                column: "UserId",
                principalTable: "post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
