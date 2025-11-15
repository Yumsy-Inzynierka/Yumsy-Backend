using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yumsy_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesForQuizFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quiz_question_tag_TagId",
                table: "quiz_question");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "quiz_question");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "quiz_question");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "quiz_question",
                newName: "TagCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_quiz_question_TagId",
                table: "quiz_question",
                newName: "IX_quiz_question_TagCategoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "TagCategoryId",
                table: "tag",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "recommendation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "quiz_question",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "quiz_answer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    QuizQuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quiz_answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quiz_answer_quiz_question_QuizQuestionId",
                        column: x => x.QuizQuestionId,
                        principalTable: "quiz_question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_quiz_answer_tag_TagId",
                        column: x => x.TagId,
                        principalTable: "tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "seen_post",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seen_post", x => new { x.UserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_seen_post_post_PostId",
                        column: x => x.PostId,
                        principalTable: "post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_seen_post_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tag_category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag_category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tag_TagCategoryId",
                table: "tag",
                column: "TagCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_quiz_answer_QuizQuestionId",
                table: "quiz_answer",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_quiz_answer_TagId",
                table: "quiz_answer",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_seen_post_PostId",
                table: "seen_post",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_question_tag_category_TagCategoryId",
                table: "quiz_question",
                column: "TagCategoryId",
                principalTable: "tag_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tag_tag_category_TagCategoryId",
                table: "tag",
                column: "TagCategoryId",
                principalTable: "tag_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quiz_question_tag_category_TagCategoryId",
                table: "quiz_question");

            migrationBuilder.DropForeignKey(
                name: "FK_tag_tag_category_TagCategoryId",
                table: "tag");

            migrationBuilder.DropTable(
                name: "quiz_answer");

            migrationBuilder.DropTable(
                name: "seen_post");

            migrationBuilder.DropTable(
                name: "tag_category");

            migrationBuilder.DropIndex(
                name: "IX_tag_TagCategoryId",
                table: "tag");

            migrationBuilder.DropColumn(
                name: "TagCategoryId",
                table: "tag");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "recommendation");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "quiz_question");

            migrationBuilder.RenameColumn(
                name: "TagCategoryId",
                table: "quiz_question",
                newName: "TagId");

            migrationBuilder.RenameIndex(
                name: "IX_quiz_question_TagCategoryId",
                table: "quiz_question",
                newName: "IX_quiz_question_TagId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "quiz_question",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "quiz_question",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_question_tag_TagId",
                table: "quiz_question",
                column: "TagId",
                principalTable: "tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
