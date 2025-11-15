using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yumsy_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixFollowUserRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_follower_user_FollowerId",
                table: "user_follower");

            migrationBuilder.DropForeignKey(
                name: "FK_user_follower_user_FollowingId",
                table: "user_follower");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_follower",
                table: "user_follower");

            migrationBuilder.DropIndex(
                name: "IX_user_follower_FollowerId",
                table: "user_follower");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_follower",
                table: "user_follower",
                columns: new[] { "FollowerId", "FollowingId" });

            migrationBuilder.CreateIndex(
                name: "IX_user_follower_FollowingId",
                table: "user_follower",
                column: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_follower_user_FollowerId",
                table: "user_follower",
                column: "FollowerId",
                principalTable: "user",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_follower_user_FollowingId",
                table: "user_follower",
                column: "FollowingId",
                principalTable: "user",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_follower_user_FollowerId",
                table: "user_follower");

            migrationBuilder.DropForeignKey(
                name: "FK_user_follower_user_FollowingId",
                table: "user_follower");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_follower",
                table: "user_follower");

            migrationBuilder.DropIndex(
                name: "IX_user_follower_FollowingId",
                table: "user_follower");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_follower",
                table: "user_follower",
                columns: new[] { "FollowingId", "FollowerId" });

            migrationBuilder.CreateIndex(
                name: "IX_user_follower_FollowerId",
                table: "user_follower",
                column: "FollowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_follower_user_FollowerId",
                table: "user_follower",
                column: "FollowerId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_follower_user_FollowingId",
                table: "user_follower",
                column: "FollowingId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
