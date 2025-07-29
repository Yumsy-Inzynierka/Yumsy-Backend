
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.Modele;

namespace Yumsy_Backend.Persistence.DbContext;

public partial class SupabaseDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SupabaseDbContext()
    {
    }

    public SupabaseDbContext(DbContextOptions<SupabaseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentLike> CommentLikes { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientPost> IngredientPosts { get; set; }

    public virtual DbSet<IngredientShoppingList> IngredientShoppingLists { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }

    public virtual DbSet<Recommendation> Recommendations { get; set; }

    public virtual DbSet<RecommendationTag> RecommendationTags { get; set; }

    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }

    public virtual DbSet<Step> Steps { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=aws-0-eu-north-1.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.kitubqamchqakbyysyuk;Password=k2ig2odPgpKNISFC;Ssl Mode=Require;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_pk");

            entity.ToTable("comment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ChildComment).HasColumnName("child_comment");
            entity.Property(e => e.CommentedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("commented_date");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ChildCommentNavigation).WithMany(p => p.InverseChildCommentNavigation)
                .HasForeignKey(d => d.ChildComment)
                .HasConstraintName("comment_comment");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_post");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_user");
        });

        modelBuilder.Entity<CommentLike>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_like_pk");

            entity.ToTable("comment_like");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentLikes)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_like_comment");

            entity.HasOne(d => d.User).WithMany(p => p.CommentLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_like_user");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ingredient_pk");

            entity.ToTable("ingredient");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Carbohydrates100g).HasColumnName("carbohydrates_100g");
            entity.Property(e => e.EnergyKcal100g).HasColumnName("energy_kcal_100g");
            entity.Property(e => e.Fat100g).HasColumnName("fat_100g");
            entity.Property(e => e.Fiber100g).HasColumnName("fiber_100g");
            entity.Property(e => e.MainCategory).HasColumnName("main_category");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Proteins100g).HasColumnName("proteins_100g");
            entity.Property(e => e.Salt100g).HasColumnName("salt_100g");
            entity.Property(e => e.SearchCount)
                .HasDefaultValue(0)
                .HasColumnName("search_count");
            entity.Property(e => e.Sugars100g).HasColumnName("sugars_100g");
        });

        modelBuilder.Entity<IngredientPost>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.IngredientId }).HasName("ingredient_post_pk");

            entity.ToTable("ingredient_post");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientPosts)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_post_ingredient");

            entity.HasOne(d => d.Post).WithMany(p => p.IngredientPosts)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_post_post");
        });

        modelBuilder.Entity<IngredientShoppingList>(entity =>
        {
            entity.HasKey(e => new { e.ShoppingListId, e.IngredientId }).HasName("ingredient_shopping_list_pk");

            entity.ToTable("ingredient_shopping_list");

            entity.Property(e => e.ShoppingListId).HasColumnName("shopping_list_id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientShoppingLists)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingridient_shopping_list_ingredient");

            entity.HasOne(d => d.ShoppingList).WithMany(p => p.IngredientShoppingLists)
                .HasForeignKey(d => d.ShoppingListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingridient_shopping_list_shopping_list");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PostId }).HasName("like_pk");

            entity.ToTable("like");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Post).WithMany(p => p.Likes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("like_post");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("polubienie_user");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("photo_pk");

            entity.ToTable("photo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CloudKey)
                .HasMaxLength(4000)
                .HasColumnName("cloud_key");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.StepId).HasColumnName("step_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Photos)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("photo_post");

            entity.HasOne(d => d.Step).WithMany(p => p.Photos)
                .HasForeignKey(d => d.StepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("photo_step");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("post_pk");

            entity.ToTable("post");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CommentsCount).HasColumnName("comments_count");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.LikesCount).HasColumnName("likes_count");
            entity.Property(e => e.PostedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("posted_date");
            entity.Property(e => e.SavedCount).HasColumnName("saved_count");
            entity.Property(e => e.SharedCount).HasColumnName("shared_count");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_user");

            entity.HasMany(d => d.Tags).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("post_tag_tag"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("post_tag_post"),
                    j =>
                    {
                        j.HasKey("PostId", "TagId").HasName("post_tag_pk");
                        j.ToTable("post_tag");
                        j.IndexerProperty<Guid>("PostId").HasColumnName("post_id");
                        j.IndexerProperty<Guid>("TagId").HasColumnName("tag_id");
                    });
        });

        modelBuilder.Entity<QuizQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("quiz_question_pk");

            entity.ToTable("quiz_question");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.TagId).HasColumnName("tag_id");

            entity.HasOne(d => d.Tag).WithMany(p => p.QuizQuestions)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("quiz_question_tag");
        });

        modelBuilder.Entity<Recommendation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recommendation_pk");

            entity.ToTable("recommendation");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Recommendations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_favourite_tags_user");
        });

        modelBuilder.Entity<RecommendationTag>(entity =>
        {
            entity.HasKey(e => new { e.RecommendationId, e.TagId }).HasName("recommendation_tag_pk");

            entity.ToTable("recommendation_tag");

            entity.Property(e => e.RecommendationId).HasColumnName("recommendation_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.Count).HasColumnName("count");

            entity.HasOne(d => d.Recommendation).WithMany(p => p.RecommendationTags)
                .HasForeignKey(d => d.RecommendationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recommendation_tag_recommendation");

            entity.HasOne(d => d.Tag).WithMany(p => p.RecommendationTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recommendation_tag_tag");
        });

        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shopping_list_pk");

            entity.ToTable("shopping_list");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedFrom).HasColumnName("created_from");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CreatedFromNavigation).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.CreatedFrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shopping_list_post");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shopping_list_user");
        });

        modelBuilder.Entity<Step>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("step_pk");

            entity.ToTable("step");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.PostId).HasColumnName("post_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Steps)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("step_post");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_pk");

            entity.ToTable("tag");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Emote)
                .HasMaxLength(100)
                .HasColumnName("emote");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("user");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FollowersCount)
                .HasDefaultValue(0)
                .HasColumnName("followers_count");
            entity.Property(e => e.FollowingCount)
                .HasDefaultValue(0)
                .HasColumnName("following_count");
            entity.Property(e => e.ProfileName)
                .HasMaxLength(50)
                .HasColumnName("profile_name");
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(200)
                .HasColumnName("profile_picture");
            entity.Property(e => e.RecipesCount)
                .HasDefaultValue(0)
                .HasColumnName("recipes_count");
            entity.Property(e => e.RegistrationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registration_date");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasMany(d => d.PostsNavigation).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Saved",
                    r => r.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("saved_post"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("saved_user"),
                    j =>
                    {
                        j.HasKey("UserId", "PostId").HasName("saved_pk");
                        j.ToTable("saved");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<Guid>("PostId").HasColumnName("post_id");
                    });

            entity.HasMany(d => d.UserFolloweds).WithMany(p => p.UserFollowings)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFollow",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserFollowedId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_followed_user"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserFollowingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_follows_user"),
                    j =>
                    {
                        j.HasKey("UserFollowingId", "UserFollowedId").HasName("user_follows_pk");
                        j.ToTable("user_follows");
                        j.IndexerProperty<Guid>("UserFollowingId").HasColumnName("user_following_id");
                        j.IndexerProperty<Guid>("UserFollowedId").HasColumnName("user_followed_id");
                    });

            entity.HasMany(d => d.UserFollowings).WithMany(p => p.UserFolloweds)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFollow",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserFollowingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_follows_user"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserFollowedId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_followed_user"),
                    j =>
                    {
                        j.HasKey("UserFollowingId", "UserFollowedId").HasName("user_follows_pk");
                        j.ToTable("user_follows");
                        j.IndexerProperty<Guid>("UserFollowingId").HasColumnName("user_following_id");
                        j.IndexerProperty<Guid>("UserFollowedId").HasColumnName("user_followed_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
