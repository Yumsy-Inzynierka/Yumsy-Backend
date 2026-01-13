using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yumsy_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPostAttributesToTopDailyPostsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "comments_count",
                table: "top_daily_post",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "top_daily_post",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "likes_count",
                table: "top_daily_post",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "posted_date",
                table: "top_daily_post",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "top_daily_post",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "top_daily_post",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "top_daily_post",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comments_count",
                table: "top_daily_post");

            migrationBuilder.DropColumn(
                name: "image_url",
                table: "top_daily_post");

            migrationBuilder.DropColumn(
                name: "likes_count",
                table: "top_daily_post");

            migrationBuilder.DropColumn(
                name: "posted_date",
                table: "top_daily_post");

            migrationBuilder.DropColumn(
                name: "title",
                table: "top_daily_post");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "top_daily_post");

            migrationBuilder.DropColumn(
                name: "username",
                table: "top_daily_post");
        }
    }
}
