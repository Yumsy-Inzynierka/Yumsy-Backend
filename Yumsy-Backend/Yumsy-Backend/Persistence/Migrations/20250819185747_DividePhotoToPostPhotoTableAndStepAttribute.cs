using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yumsy_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DividePhotoToPostPhotoTableAndStepAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_step_post_PostId",
                table: "step");

            migrationBuilder.DropTable(
                name: "photo");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostId",
                table: "step",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "step",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "post_image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_image_post_PostId",
                        column: x => x.PostId,
                        principalTable: "post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_image_PostId",
                table: "post_image",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_step_post_PostId",
                table: "step",
                column: "PostId",
                principalTable: "post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_step_post_PostId",
                table: "step");

            migrationBuilder.DropTable(
                name: "post_image");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "step");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostId",
                table: "step",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "photo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StepId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_photo_post_PostId",
                        column: x => x.PostId,
                        principalTable: "post",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_photo_step_StepId",
                        column: x => x.StepId,
                        principalTable: "step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_photo_PostId",
                table: "photo",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_photo_StepId",
                table: "photo",
                column: "StepId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_step_post_PostId",
                table: "step",
                column: "PostId",
                principalTable: "post",
                principalColumn: "Id");
        }
    }
}
