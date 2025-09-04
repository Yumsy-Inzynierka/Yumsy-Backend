using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yumsy_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserFollowerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_follower_user_UserId",
                table: "user_follower");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_follower",
                newName: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_follower_user_FollowingId",
                table: "user_follower",
                column: "FollowingId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_follower_user_FollowingId",
                table: "user_follower");

            migrationBuilder.RenameColumn(
                name: "FollowingId",
                table: "user_follower",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_follower_user_UserId",
                table: "user_follower",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
