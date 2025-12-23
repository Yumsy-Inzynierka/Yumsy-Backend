using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yumsy_Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_comment_ParentCommentId",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "FK_comment_post_PostId",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "FK_comment_user_UserId",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "FK_comment_like_comment_CommentId",
                table: "comment_like");

            migrationBuilder.DropForeignKey(
                name: "FK_comment_like_user_UserId",
                table: "comment_like");

            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_post_ingredient_IngredientId",
                table: "ingredient_post");

            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_post_post_PostId",
                table: "ingredient_post");

            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_shopping_list_ingredient_IngredientId",
                table: "ingredient_shopping_list");

            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_shopping_list_shopping_list_ShoppingListId",
                table: "ingredient_shopping_list");

            migrationBuilder.DropForeignKey(
                name: "FK_like_post_PostId",
                table: "like");

            migrationBuilder.DropForeignKey(
                name: "FK_like_user_UserId",
                table: "like");

            migrationBuilder.DropForeignKey(
                name: "FK_post_user_UserId",
                table: "post");

            migrationBuilder.DropForeignKey(
                name: "FK_post_image_post_PostId",
                table: "post_image");

            migrationBuilder.DropForeignKey(
                name: "FK_post_tag_post_PostId",
                table: "post_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_post_tag_tag_TagId",
                table: "post_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_quiz_answer_quiz_question_QuizQuestionId",
                table: "quiz_answer");

            migrationBuilder.DropForeignKey(
                name: "FK_quiz_answer_tag_TagId",
                table: "quiz_answer");

            migrationBuilder.DropForeignKey(
                name: "FK_quiz_question_tag_category_TagCategoryId",
                table: "quiz_question");

            migrationBuilder.DropForeignKey(
                name: "FK_recommendation_tag_TagId",
                table: "recommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_recommendation_user_UserId",
                table: "recommendation");

            migrationBuilder.DropForeignKey(
                name: "FK_saved_post_PostId",
                table: "saved");

            migrationBuilder.DropForeignKey(
                name: "FK_saved_user_UserId",
                table: "saved");

            migrationBuilder.DropForeignKey(
                name: "FK_seen_post_post_PostId",
                table: "seen_post");

            migrationBuilder.DropForeignKey(
                name: "FK_seen_post_user_UserId",
                table: "seen_post");

            migrationBuilder.DropForeignKey(
                name: "FK_shopping_list_post_CreatedFromId",
                table: "shopping_list");

            migrationBuilder.DropForeignKey(
                name: "FK_shopping_list_user_UserId",
                table: "shopping_list");

            migrationBuilder.DropForeignKey(
                name: "FK_step_post_PostId",
                table: "step");

            migrationBuilder.DropForeignKey(
                name: "FK_tag_tag_category_TagCategoryId",
                table: "tag");

            migrationBuilder.DropForeignKey(
                name: "FK_user_follower_user_FollowerId",
                table: "user_follower");

            migrationBuilder.DropForeignKey(
                name: "FK_user_follower_user_FollowingId",
                table: "user_follower");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_follower",
                table: "user_follower");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tag_category",
                table: "tag_category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tag",
                table: "tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_step",
                table: "step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shopping_list",
                table: "shopping_list");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seen_post",
                table: "seen_post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_saved",
                table: "saved");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recommendation",
                table: "recommendation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quiz_question",
                table: "quiz_question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quiz_answer",
                table: "quiz_answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_post_tag",
                table: "post_tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_post_image",
                table: "post_image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_post",
                table: "post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_like",
                table: "like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredient_shopping_list",
                table: "ingredient_shopping_list");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredient_post",
                table: "ingredient_post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredient",
                table: "ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comment_like",
                table: "comment_like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comment",
                table: "comment");

            migrationBuilder.RenameColumn(
                name: "FollowedAt",
                table: "user_follower",
                newName: "followed_at");

            migrationBuilder.RenameColumn(
                name: "FollowingId",
                table: "user_follower",
                newName: "following_id");

            migrationBuilder.RenameColumn(
                name: "FollowerId",
                table: "user_follower",
                newName: "follower_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_follower_FollowingId",
                table: "user_follower",
                newName: "ix_user_follower_following_id");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "user",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "user",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "user",
                newName: "bio");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "user",
                newName: "registration_date");

            migrationBuilder.RenameColumn(
                name: "RecipesCount",
                table: "user",
                newName: "recipes_count");

            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "user",
                newName: "profile_picture");

            migrationBuilder.RenameColumn(
                name: "ProfileName",
                table: "user",
                newName: "profile_name");

            migrationBuilder.RenameColumn(
                name: "FollowingCount",
                table: "user",
                newName: "following_count");

            migrationBuilder.RenameColumn(
                name: "FollowersCount",
                table: "user",
                newName: "followers_count");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tag_category",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tag_category",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tag",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Emote",
                table: "tag",
                newName: "emote");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tag",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TagCategoryId",
                table: "tag",
                newName: "tag_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_tag_TagCategoryId",
                table: "tag",
                newName: "ix_tag_tag_category_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "step",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "step",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StepNumber",
                table: "step",
                newName: "step_number");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "step",
                newName: "post_id");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "step",
                newName: "image_url");

            migrationBuilder.RenameIndex(
                name: "IX_step_PostId",
                table: "step",
                newName: "ix_step_post_id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "shopping_list",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "shopping_list",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "shopping_list",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "CreatedFromId",
                table: "shopping_list",
                newName: "created_from_id");

            migrationBuilder.RenameIndex(
                name: "IX_shopping_list_UserId",
                table: "shopping_list",
                newName: "ix_shopping_list_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_shopping_list_CreatedFromId",
                table: "shopping_list",
                newName: "ix_shopping_list_created_from_id");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "seen_post",
                newName: "post_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "seen_post",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_seen_post_PostId",
                table: "seen_post",
                newName: "ix_seen_post_post_id");

            migrationBuilder.RenameColumn(
                name: "SavedAt",
                table: "saved",
                newName: "saved_at");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "saved",
                newName: "post_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "saved",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_saved_PostId",
                table: "saved",
                newName: "ix_saved_post_id");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "recommendation",
                newName: "score");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "recommendation",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "recommendation",
                newName: "tag_id");

            migrationBuilder.RenameIndex(
                name: "IX_recommendation_UserId",
                table: "recommendation",
                newName: "ix_recommendation_user_id");

            migrationBuilder.RenameColumn(
                name: "Question",
                table: "quiz_question",
                newName: "question");

            migrationBuilder.RenameColumn(
                name: "Mandatory",
                table: "quiz_question",
                newName: "mandatory");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "quiz_question",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TagCategoryId",
                table: "quiz_question",
                newName: "tag_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_quiz_question_TagCategoryId",
                table: "quiz_question",
                newName: "ix_quiz_question_tag_category_id");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "quiz_answer",
                newName: "answer");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "quiz_answer",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "quiz_answer",
                newName: "tag_id");

            migrationBuilder.RenameColumn(
                name: "QuizQuestionId",
                table: "quiz_answer",
                newName: "quiz_question_id");

            migrationBuilder.RenameIndex(
                name: "IX_quiz_answer_TagId",
                table: "quiz_answer",
                newName: "ix_quiz_answer_tag_id");

            migrationBuilder.RenameIndex(
                name: "IX_quiz_answer_QuizQuestionId",
                table: "quiz_answer",
                newName: "ix_quiz_answer_quiz_question_id");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "post_tag",
                newName: "tag_id");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "post_tag",
                newName: "post_id");

            migrationBuilder.RenameIndex(
                name: "IX_post_tag_TagId",
                table: "post_tag",
                newName: "ix_post_tag_tag_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "post_image",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "post_image",
                newName: "post_id");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "post_image",
                newName: "image_url");

            migrationBuilder.RenameIndex(
                name: "IX_post_image_PostId",
                table: "post_image",
                newName: "ix_post_image_post_id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "post",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "post",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "post",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "post",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "SharedCount",
                table: "post",
                newName: "shared_count");

            migrationBuilder.RenameColumn(
                name: "SavedCount",
                table: "post",
                newName: "saved_count");

            migrationBuilder.RenameColumn(
                name: "PostedDate",
                table: "post",
                newName: "posted_date");

            migrationBuilder.RenameColumn(
                name: "LikesCount",
                table: "post",
                newName: "likes_count");

            migrationBuilder.RenameColumn(
                name: "CookingTime",
                table: "post",
                newName: "cooking_time");

            migrationBuilder.RenameColumn(
                name: "CommentsCount",
                table: "post",
                newName: "comments_count");

            migrationBuilder.RenameIndex(
                name: "IX_post_UserId",
                table: "post",
                newName: "ix_post_user_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "like",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "like",
                newName: "post_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "like",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_like_PostId",
                table: "like",
                newName: "ix_like_post_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ingredient_shopping_list",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "ShoppingListId",
                table: "ingredient_shopping_list",
                newName: "shopping_list_id");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "ingredient_shopping_list",
                newName: "ingredient_id");

            migrationBuilder.RenameIndex(
                name: "IX_ingredient_shopping_list_ShoppingListId",
                table: "ingredient_shopping_list",
                newName: "ix_ingredient_shopping_list_shopping_list_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ingredient_post",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "ingredient_post",
                newName: "post_id");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "ingredient_post",
                newName: "ingredient_id");

            migrationBuilder.RenameIndex(
                name: "IX_ingredient_post_PostId",
                table: "ingredient_post",
                newName: "ix_ingredient_post_post_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ingredient",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "ingredient",
                newName: "brand");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ingredient",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Sugars100g",
                table: "ingredient",
                newName: "sugars_100g");

            migrationBuilder.RenameColumn(
                name: "SearchCount",
                table: "ingredient",
                newName: "search_count");

            migrationBuilder.RenameColumn(
                name: "Salt100g",
                table: "ingredient",
                newName: "salt_100g");

            migrationBuilder.RenameColumn(
                name: "Proteins100g",
                table: "ingredient",
                newName: "proteins_100g");

            migrationBuilder.RenameColumn(
                name: "MainCategory",
                table: "ingredient",
                newName: "main_category");

            migrationBuilder.RenameColumn(
                name: "Fiber100g",
                table: "ingredient",
                newName: "fiber_100g");

            migrationBuilder.RenameColumn(
                name: "Fat100g",
                table: "ingredient",
                newName: "fat_100g");

            migrationBuilder.RenameColumn(
                name: "EnergyKcal100g",
                table: "ingredient",
                newName: "energy_kcal_100g");

            migrationBuilder.RenameColumn(
                name: "Carbohydrates100g",
                table: "ingredient",
                newName: "carbohydrates_100g");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "comment_like",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "comment_like",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "comment_like",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "comment_like",
                newName: "comment_id");

            migrationBuilder.RenameIndex(
                name: "IX_comment_like_UserId",
                table: "comment_like",
                newName: "ix_comment_like_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_comment_like_CommentId",
                table: "comment_like",
                newName: "ix_comment_like_comment_id");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "comment",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "comment",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "comment",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "comment",
                newName: "post_id");

            migrationBuilder.RenameColumn(
                name: "ParentCommentId",
                table: "comment",
                newName: "parent_comment_id");

            migrationBuilder.RenameColumn(
                name: "LikesCount",
                table: "comment",
                newName: "likes_count");

            migrationBuilder.RenameColumn(
                name: "CommentedDate",
                table: "comment",
                newName: "commented_date");

            migrationBuilder.RenameIndex(
                name: "IX_comment_UserId",
                table: "comment",
                newName: "ix_comment_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_comment_PostId",
                table: "comment",
                newName: "ix_comment_post_id");

            migrationBuilder.RenameIndex(
                name: "IX_comment_ParentCommentId",
                table: "comment",
                newName: "ix_comment_parent_comment_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "tag_id",
                table: "quiz_answer",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_follower",
                table: "user_follower",
                columns: new[] { "follower_id", "following_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_user",
                table: "user",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tag_category",
                table: "tag_category",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tag",
                table: "tag",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_step",
                table: "step",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_shopping_list",
                table: "shopping_list",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_seen_post",
                table: "seen_post",
                columns: new[] { "user_id", "post_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_saved",
                table: "saved",
                columns: new[] { "user_id", "post_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_recommendation",
                table: "recommendation",
                columns: new[] { "tag_id", "user_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_quiz_question",
                table: "quiz_question",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_quiz_answer",
                table: "quiz_answer",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_post_tag",
                table: "post_tag",
                columns: new[] { "post_id", "tag_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_post_image",
                table: "post_image",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_post",
                table: "post",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_like",
                table: "like",
                columns: new[] { "user_id", "post_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_ingredient_shopping_list",
                table: "ingredient_shopping_list",
                columns: new[] { "ingredient_id", "shopping_list_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_ingredient_post",
                table: "ingredient_post",
                columns: new[] { "ingredient_id", "post_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_ingredient",
                table: "ingredient",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_comment_like",
                table: "comment_like",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_comment",
                table: "comment",
                column: "id");

            migrationBuilder.CreateTable(
                name: "app_event_log",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: true),
                    action = table.Column<string>(type: "text", nullable: false),
                    result = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_event_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "error_log",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    exception_type = table.Column<string>(type: "text", nullable: false),
                    status_code = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    stack_trace = table.Column<string>(type: "text", nullable: false),
                    correlation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_error_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "http_log",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    method = table.Column<string>(type: "text", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    status_code = table.Column<int>(type: "integer", nullable: false),
                    duration_ms = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    correlation_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_http_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "top_daily_post",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    rank = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_top_daily_post", x => x.id);
                    table.ForeignKey(
                        name: "fk_top_daily_post_post_post_id",
                        column: x => x.post_id,
                        principalTable: "post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "top_daily_tag",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tag_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    rank = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_top_daily_tag", x => x.id);
                    table.ForeignKey(
                        name: "fk_top_daily_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_top_daily_post_post_id",
                table: "top_daily_post",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "unique_date_rank",
                table: "top_daily_post",
                columns: new[] { "date", "rank" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_top_daily_tag_tag_id",
                table: "top_daily_tag",
                column: "tag_id");

            migrationBuilder.AddForeignKey(
                name: "fk_comment_comment_parent_comment_id",
                table: "comment",
                column: "parent_comment_id",
                principalTable: "comment",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_comment_post_post_id",
                table: "comment",
                column: "post_id",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_comment_user_user_id",
                table: "comment",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_comment_like_comment_comment_id",
                table: "comment_like",
                column: "comment_id",
                principalTable: "comment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_comment_like_user_user_id",
                table: "comment_like",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ingredient_post_ingredient_ingredient_id",
                table: "ingredient_post",
                column: "ingredient_id",
                principalTable: "ingredient",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ingredient_post_post_post_id",
                table: "ingredient_post",
                column: "post_id",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ingredient_shopping_list_ingredient_ingredient_id",
                table: "ingredient_shopping_list",
                column: "ingredient_id",
                principalTable: "ingredient",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ingredient_shopping_list_shopping_list_shopping_list_id",
                table: "ingredient_shopping_list",
                column: "shopping_list_id",
                principalTable: "shopping_list",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_like_post_post_id",
                table: "like",
                column: "post_id",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_like_user_user_id",
                table: "like",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_user_user_id",
                table: "post",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_image_post_post_id",
                table: "post_image",
                column: "post_id",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_tag_post_post_id",
                table: "post_tag",
                column: "post_id",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_tag_tag_tag_id",
                table: "post_tag",
                column: "tag_id",
                principalTable: "tag",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_quiz_answer_quiz_question_quiz_question_id",
                table: "quiz_answer",
                column: "quiz_question_id",
                principalTable: "quiz_question",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_quiz_answer_tag_tag_id",
                table: "quiz_answer",
                column: "tag_id",
                principalTable: "tag",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_quiz_question_tag_category_tag_category_id",
                table: "quiz_question",
                column: "tag_category_id",
                principalTable: "tag_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_recommendation_tag_tag_id",
                table: "recommendation",
                column: "tag_id",
                principalTable: "tag",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_recommendation_user_user_id",
                table: "recommendation",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_saved_post_post_id",
                table: "saved",
                column: "post_id",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_saved_user_user_id",
                table: "saved",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_seen_post_post_post_id",
                table: "seen_post",
                column: "post_id",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_seen_post_user_user_id",
                table: "seen_post",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_shopping_list_post_created_from_id",
                table: "shopping_list",
                column: "created_from_id",
                principalTable: "post",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_shopping_list_user_user_id",
                table: "shopping_list",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_step_post_post_id",
                table: "step",
                column: "post_id",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_tag_tag_category_tag_category_id",
                table: "tag",
                column: "tag_category_id",
                principalTable: "tag_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_follower_user_follower_id",
                table: "user_follower",
                column: "follower_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_follower_user_following_id",
                table: "user_follower",
                column: "following_id",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comment_comment_parent_comment_id",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "fk_comment_post_post_id",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "fk_comment_user_user_id",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "fk_comment_like_comment_comment_id",
                table: "comment_like");

            migrationBuilder.DropForeignKey(
                name: "fk_comment_like_user_user_id",
                table: "comment_like");

            migrationBuilder.DropForeignKey(
                name: "fk_ingredient_post_ingredient_ingredient_id",
                table: "ingredient_post");

            migrationBuilder.DropForeignKey(
                name: "fk_ingredient_post_post_post_id",
                table: "ingredient_post");

            migrationBuilder.DropForeignKey(
                name: "fk_ingredient_shopping_list_ingredient_ingredient_id",
                table: "ingredient_shopping_list");

            migrationBuilder.DropForeignKey(
                name: "fk_ingredient_shopping_list_shopping_list_shopping_list_id",
                table: "ingredient_shopping_list");

            migrationBuilder.DropForeignKey(
                name: "fk_like_post_post_id",
                table: "like");

            migrationBuilder.DropForeignKey(
                name: "fk_like_user_user_id",
                table: "like");

            migrationBuilder.DropForeignKey(
                name: "fk_post_user_user_id",
                table: "post");

            migrationBuilder.DropForeignKey(
                name: "fk_post_image_post_post_id",
                table: "post_image");

            migrationBuilder.DropForeignKey(
                name: "fk_post_tag_post_post_id",
                table: "post_tag");

            migrationBuilder.DropForeignKey(
                name: "fk_post_tag_tag_tag_id",
                table: "post_tag");

            migrationBuilder.DropForeignKey(
                name: "fk_quiz_answer_quiz_question_quiz_question_id",
                table: "quiz_answer");

            migrationBuilder.DropForeignKey(
                name: "fk_quiz_answer_tag_tag_id",
                table: "quiz_answer");

            migrationBuilder.DropForeignKey(
                name: "fk_quiz_question_tag_category_tag_category_id",
                table: "quiz_question");

            migrationBuilder.DropForeignKey(
                name: "fk_recommendation_tag_tag_id",
                table: "recommendation");

            migrationBuilder.DropForeignKey(
                name: "fk_recommendation_user_user_id",
                table: "recommendation");

            migrationBuilder.DropForeignKey(
                name: "fk_saved_post_post_id",
                table: "saved");

            migrationBuilder.DropForeignKey(
                name: "fk_saved_user_user_id",
                table: "saved");

            migrationBuilder.DropForeignKey(
                name: "fk_seen_post_post_post_id",
                table: "seen_post");

            migrationBuilder.DropForeignKey(
                name: "fk_seen_post_user_user_id",
                table: "seen_post");

            migrationBuilder.DropForeignKey(
                name: "fk_shopping_list_post_created_from_id",
                table: "shopping_list");

            migrationBuilder.DropForeignKey(
                name: "fk_shopping_list_user_user_id",
                table: "shopping_list");

            migrationBuilder.DropForeignKey(
                name: "fk_step_post_post_id",
                table: "step");

            migrationBuilder.DropForeignKey(
                name: "fk_tag_tag_category_tag_category_id",
                table: "tag");

            migrationBuilder.DropForeignKey(
                name: "fk_user_follower_user_follower_id",
                table: "user_follower");

            migrationBuilder.DropForeignKey(
                name: "fk_user_follower_user_following_id",
                table: "user_follower");

            migrationBuilder.DropTable(
                name: "app_event_log");

            migrationBuilder.DropTable(
                name: "error_log");

            migrationBuilder.DropTable(
                name: "http_log");

            migrationBuilder.DropTable(
                name: "top_daily_post");

            migrationBuilder.DropTable(
                name: "top_daily_tag");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_follower",
                table: "user_follower");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tag_category",
                table: "tag_category");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tag",
                table: "tag");

            migrationBuilder.DropPrimaryKey(
                name: "pk_step",
                table: "step");

            migrationBuilder.DropPrimaryKey(
                name: "pk_shopping_list",
                table: "shopping_list");

            migrationBuilder.DropPrimaryKey(
                name: "pk_seen_post",
                table: "seen_post");

            migrationBuilder.DropPrimaryKey(
                name: "pk_saved",
                table: "saved");

            migrationBuilder.DropPrimaryKey(
                name: "pk_recommendation",
                table: "recommendation");

            migrationBuilder.DropPrimaryKey(
                name: "pk_quiz_question",
                table: "quiz_question");

            migrationBuilder.DropPrimaryKey(
                name: "pk_quiz_answer",
                table: "quiz_answer");

            migrationBuilder.DropPrimaryKey(
                name: "pk_post_tag",
                table: "post_tag");

            migrationBuilder.DropPrimaryKey(
                name: "pk_post_image",
                table: "post_image");

            migrationBuilder.DropPrimaryKey(
                name: "pk_post",
                table: "post");

            migrationBuilder.DropPrimaryKey(
                name: "pk_like",
                table: "like");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ingredient_shopping_list",
                table: "ingredient_shopping_list");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ingredient_post",
                table: "ingredient_post");

            migrationBuilder.DropPrimaryKey(
                name: "pk_ingredient",
                table: "ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "pk_comment_like",
                table: "comment_like");

            migrationBuilder.DropPrimaryKey(
                name: "pk_comment",
                table: "comment");

            migrationBuilder.RenameColumn(
                name: "followed_at",
                table: "user_follower",
                newName: "FollowedAt");

            migrationBuilder.RenameColumn(
                name: "following_id",
                table: "user_follower",
                newName: "FollowingId");

            migrationBuilder.RenameColumn(
                name: "follower_id",
                table: "user_follower",
                newName: "FollowerId");

            migrationBuilder.RenameIndex(
                name: "ix_user_follower_following_id",
                table: "user_follower",
                newName: "IX_user_follower_FollowingId");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "user",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "user",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "user",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "bio",
                table: "user",
                newName: "Bio");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "registration_date",
                table: "user",
                newName: "RegistrationDate");

            migrationBuilder.RenameColumn(
                name: "recipes_count",
                table: "user",
                newName: "RecipesCount");

            migrationBuilder.RenameColumn(
                name: "profile_picture",
                table: "user",
                newName: "ProfilePicture");

            migrationBuilder.RenameColumn(
                name: "profile_name",
                table: "user",
                newName: "ProfileName");

            migrationBuilder.RenameColumn(
                name: "following_count",
                table: "user",
                newName: "FollowingCount");

            migrationBuilder.RenameColumn(
                name: "followers_count",
                table: "user",
                newName: "FollowersCount");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "tag_category",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tag_category",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "tag",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "emote",
                table: "tag",
                newName: "Emote");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tag",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tag_category_id",
                table: "tag",
                newName: "TagCategoryId");

            migrationBuilder.RenameIndex(
                name: "ix_tag_tag_category_id",
                table: "tag",
                newName: "IX_tag_TagCategoryId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "step",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "step",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "step_number",
                table: "step",
                newName: "StepNumber");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "step",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "step",
                newName: "ImageUrl");

            migrationBuilder.RenameIndex(
                name: "ix_step_post_id",
                table: "step",
                newName: "IX_step_PostId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "shopping_list",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "shopping_list",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "shopping_list",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "created_from_id",
                table: "shopping_list",
                newName: "CreatedFromId");

            migrationBuilder.RenameIndex(
                name: "ix_shopping_list_user_id",
                table: "shopping_list",
                newName: "IX_shopping_list_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_shopping_list_created_from_id",
                table: "shopping_list",
                newName: "IX_shopping_list_CreatedFromId");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "seen_post",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "seen_post",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "ix_seen_post_post_id",
                table: "seen_post",
                newName: "IX_seen_post_PostId");

            migrationBuilder.RenameColumn(
                name: "saved_at",
                table: "saved",
                newName: "SavedAt");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "saved",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "saved",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "ix_saved_post_id",
                table: "saved",
                newName: "IX_saved_PostId");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "recommendation",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "recommendation",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "tag_id",
                table: "recommendation",
                newName: "TagId");

            migrationBuilder.RenameIndex(
                name: "ix_recommendation_user_id",
                table: "recommendation",
                newName: "IX_recommendation_UserId");

            migrationBuilder.RenameColumn(
                name: "question",
                table: "quiz_question",
                newName: "Question");

            migrationBuilder.RenameColumn(
                name: "mandatory",
                table: "quiz_question",
                newName: "Mandatory");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "quiz_question",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tag_category_id",
                table: "quiz_question",
                newName: "TagCategoryId");

            migrationBuilder.RenameIndex(
                name: "ix_quiz_question_tag_category_id",
                table: "quiz_question",
                newName: "IX_quiz_question_TagCategoryId");

            migrationBuilder.RenameColumn(
                name: "answer",
                table: "quiz_answer",
                newName: "Answer");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "quiz_answer",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tag_id",
                table: "quiz_answer",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "quiz_question_id",
                table: "quiz_answer",
                newName: "QuizQuestionId");

            migrationBuilder.RenameIndex(
                name: "ix_quiz_answer_tag_id",
                table: "quiz_answer",
                newName: "IX_quiz_answer_TagId");

            migrationBuilder.RenameIndex(
                name: "ix_quiz_answer_quiz_question_id",
                table: "quiz_answer",
                newName: "IX_quiz_answer_QuizQuestionId");

            migrationBuilder.RenameColumn(
                name: "tag_id",
                table: "post_tag",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "post_tag",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "ix_post_tag_tag_id",
                table: "post_tag",
                newName: "IX_post_tag_TagId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "post_image",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "post_image",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "post_image",
                newName: "ImageUrl");

            migrationBuilder.RenameIndex(
                name: "ix_post_image_post_id",
                table: "post_image",
                newName: "IX_post_image_PostId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "post",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "post",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "post",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "post",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "shared_count",
                table: "post",
                newName: "SharedCount");

            migrationBuilder.RenameColumn(
                name: "saved_count",
                table: "post",
                newName: "SavedCount");

            migrationBuilder.RenameColumn(
                name: "posted_date",
                table: "post",
                newName: "PostedDate");

            migrationBuilder.RenameColumn(
                name: "likes_count",
                table: "post",
                newName: "LikesCount");

            migrationBuilder.RenameColumn(
                name: "cooking_time",
                table: "post",
                newName: "CookingTime");

            migrationBuilder.RenameColumn(
                name: "comments_count",
                table: "post",
                newName: "CommentsCount");

            migrationBuilder.RenameIndex(
                name: "ix_post_user_id",
                table: "post",
                newName: "IX_post_UserId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "like",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "like",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "like",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "ix_like_post_id",
                table: "like",
                newName: "IX_like_PostId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "ingredient_shopping_list",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "shopping_list_id",
                table: "ingredient_shopping_list",
                newName: "ShoppingListId");

            migrationBuilder.RenameColumn(
                name: "ingredient_id",
                table: "ingredient_shopping_list",
                newName: "IngredientId");

            migrationBuilder.RenameIndex(
                name: "ix_ingredient_shopping_list_shopping_list_id",
                table: "ingredient_shopping_list",
                newName: "IX_ingredient_shopping_list_ShoppingListId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "ingredient_post",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "ingredient_post",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "ingredient_id",
                table: "ingredient_post",
                newName: "IngredientId");

            migrationBuilder.RenameIndex(
                name: "ix_ingredient_post_post_id",
                table: "ingredient_post",
                newName: "IX_ingredient_post_PostId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ingredient",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "brand",
                table: "ingredient",
                newName: "Brand");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ingredient",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "sugars_100g",
                table: "ingredient",
                newName: "Sugars100g");

            migrationBuilder.RenameColumn(
                name: "search_count",
                table: "ingredient",
                newName: "SearchCount");

            migrationBuilder.RenameColumn(
                name: "salt_100g",
                table: "ingredient",
                newName: "Salt100g");

            migrationBuilder.RenameColumn(
                name: "proteins_100g",
                table: "ingredient",
                newName: "Proteins100g");

            migrationBuilder.RenameColumn(
                name: "main_category",
                table: "ingredient",
                newName: "MainCategory");

            migrationBuilder.RenameColumn(
                name: "fiber_100g",
                table: "ingredient",
                newName: "Fiber100g");

            migrationBuilder.RenameColumn(
                name: "fat_100g",
                table: "ingredient",
                newName: "Fat100g");

            migrationBuilder.RenameColumn(
                name: "energy_kcal_100g",
                table: "ingredient",
                newName: "EnergyKcal100g");

            migrationBuilder.RenameColumn(
                name: "carbohydrates_100g",
                table: "ingredient",
                newName: "Carbohydrates100g");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "comment_like",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "comment_like",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "comment_like",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "comment_id",
                table: "comment_like",
                newName: "CommentId");

            migrationBuilder.RenameIndex(
                name: "ix_comment_like_user_id",
                table: "comment_like",
                newName: "IX_comment_like_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_comment_like_comment_id",
                table: "comment_like",
                newName: "IX_comment_like_CommentId");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "comment",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "comment",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "comment",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "post_id",
                table: "comment",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "parent_comment_id",
                table: "comment",
                newName: "ParentCommentId");

            migrationBuilder.RenameColumn(
                name: "likes_count",
                table: "comment",
                newName: "LikesCount");

            migrationBuilder.RenameColumn(
                name: "commented_date",
                table: "comment",
                newName: "CommentedDate");

            migrationBuilder.RenameIndex(
                name: "ix_comment_user_id",
                table: "comment",
                newName: "IX_comment_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_comment_post_id",
                table: "comment",
                newName: "IX_comment_PostId");

            migrationBuilder.RenameIndex(
                name: "ix_comment_parent_comment_id",
                table: "comment",
                newName: "IX_comment_ParentCommentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TagId",
                table: "quiz_answer",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_follower",
                table: "user_follower",
                columns: new[] { "FollowerId", "FollowingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tag_category",
                table: "tag_category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tag",
                table: "tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_step",
                table: "step",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shopping_list",
                table: "shopping_list",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_seen_post",
                table: "seen_post",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_saved",
                table: "saved",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_recommendation",
                table: "recommendation",
                columns: new[] { "TagId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_quiz_question",
                table: "quiz_question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quiz_answer",
                table: "quiz_answer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_post_tag",
                table: "post_tag",
                columns: new[] { "PostId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_post_image",
                table: "post_image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_post",
                table: "post",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_like",
                table: "like",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredient_shopping_list",
                table: "ingredient_shopping_list",
                columns: new[] { "IngredientId", "ShoppingListId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredient_post",
                table: "ingredient_post",
                columns: new[] { "IngredientId", "PostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredient",
                table: "ingredient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comment_like",
                table: "comment_like",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comment",
                table: "comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_comment_ParentCommentId",
                table: "comment",
                column: "ParentCommentId",
                principalTable: "comment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_post_PostId",
                table: "comment",
                column: "PostId",
                principalTable: "post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comment_user_UserId",
                table: "comment",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comment_like_comment_CommentId",
                table: "comment_like",
                column: "CommentId",
                principalTable: "comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comment_like_user_UserId",
                table: "comment_like",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_post_ingredient_IngredientId",
                table: "ingredient_post",
                column: "IngredientId",
                principalTable: "ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_post_post_PostId",
                table: "ingredient_post",
                column: "PostId",
                principalTable: "post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_shopping_list_ingredient_IngredientId",
                table: "ingredient_shopping_list",
                column: "IngredientId",
                principalTable: "ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_shopping_list_shopping_list_ShoppingListId",
                table: "ingredient_shopping_list",
                column: "ShoppingListId",
                principalTable: "shopping_list",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_like_post_PostId",
                table: "like",
                column: "PostId",
                principalTable: "post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_like_user_UserId",
                table: "like",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_user_UserId",
                table: "post",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_image_post_PostId",
                table: "post_image",
                column: "PostId",
                principalTable: "post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_tag_post_PostId",
                table: "post_tag",
                column: "PostId",
                principalTable: "post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_post_tag_tag_TagId",
                table: "post_tag",
                column: "TagId",
                principalTable: "tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_answer_quiz_question_QuizQuestionId",
                table: "quiz_answer",
                column: "QuizQuestionId",
                principalTable: "quiz_question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_answer_tag_TagId",
                table: "quiz_answer",
                column: "TagId",
                principalTable: "tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_quiz_question_tag_category_TagCategoryId",
                table: "quiz_question",
                column: "TagCategoryId",
                principalTable: "tag_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recommendation_tag_TagId",
                table: "recommendation",
                column: "TagId",
                principalTable: "tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recommendation_user_UserId",
                table: "recommendation",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_saved_post_PostId",
                table: "saved",
                column: "PostId",
                principalTable: "post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_saved_user_UserId",
                table: "saved",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_seen_post_post_PostId",
                table: "seen_post",
                column: "PostId",
                principalTable: "post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_seen_post_user_UserId",
                table: "seen_post",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shopping_list_post_CreatedFromId",
                table: "shopping_list",
                column: "CreatedFromId",
                principalTable: "post",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_shopping_list_user_UserId",
                table: "shopping_list",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_step_post_PostId",
                table: "step",
                column: "PostId",
                principalTable: "post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tag_tag_category_TagCategoryId",
                table: "tag",
                column: "TagCategoryId",
                principalTable: "tag_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
