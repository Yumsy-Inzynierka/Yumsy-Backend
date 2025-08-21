using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.Models;


namespace Yumsy_Backend.Persistence.DbContext;

public class SupabaseDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SupabaseDbContext()
    {
    }

    public SupabaseDbContext(DbContextOptions<SupabaseDbContext> options)
        : base(options)
    {
    }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<CommentLike> CommentLikes { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<IngredientPost> IngredientPosts { get; set; }

    public DbSet<IngredientShoppingList> IngredientShoppingLists { get; set; }

    public DbSet<Like> Likes { get; set; }

    public DbSet<PostImage> Photos { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<QuizQuestion> QuizQuestions { get; set; }

    public DbSet<Recommendation> Recommendations { get; set; }

    public DbSet<RecommendationTag> RecommendationTags { get; set; }

    public DbSet<ShoppingList> ShoppingLists { get; set; }

    public DbSet<Step> Steps { get; set; }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PostTag> PostTags { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=aws-0-eu-north-1.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.kitubqamchqakbyysyuk;Password=k2ig2odPgpKNISFC;Ssl Mode=Require;Trust Server Certificate=true;Pooling=false;Timeout=60;Command Timeout=120;");
    }
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=aws-0-eu-north-1.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.kitubqamchqakbyysyuk;Password=k2ig2odPgpKNISFC;Ssl Mode=Require;Trust Server Certificate=true");
        */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFollower>()
            .HasKey(uf => new { uf.UserId, uf.FollowerId });

        modelBuilder.Entity<UserFollower>()
            .HasOne(uf => uf.User)
            .WithMany(u => u.Followers)
            .HasForeignKey(uf => uf.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserFollower>()
            .HasOne(uf => uf.Follower)
            .WithMany(u => u.Followings)
            .HasForeignKey(uf => uf.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
