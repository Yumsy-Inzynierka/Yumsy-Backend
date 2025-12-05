using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yumsy_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeTagPropertyNullableInQuizAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quiz_answer_tag_TagId",
                table: "quiz_answer");

            migrationBuilder.AlterColumn<Guid>(
                name: "TagId",
                table: "quiz_answer",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_answer_tag_TagId",
                table: "quiz_answer",
                column: "TagId",
                principalTable: "tag",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quiz_answer_tag_TagId",
                table: "quiz_answer");

            migrationBuilder.AlterColumn<Guid>(
                name: "TagId",
                table: "quiz_answer",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_answer_tag_TagId",
                table: "quiz_answer",
                column: "TagId",
                principalTable: "tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
