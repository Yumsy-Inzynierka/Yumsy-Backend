using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yumsy_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixPostAndUserRelationInCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_user_PostId",
                table: "comment");

            migrationBuilder.CreateIndex(
                name: "IX_comment_UserId",
                table: "comment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_user_UserId",
                table: "comment",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_user_UserId",
                table: "comment");

            migrationBuilder.DropIndex(
                name: "IX_comment_UserId",
                table: "comment");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_user_PostId",
                table: "comment",
                column: "PostId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
