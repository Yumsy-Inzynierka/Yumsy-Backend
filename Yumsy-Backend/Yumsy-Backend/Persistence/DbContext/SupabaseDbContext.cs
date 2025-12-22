using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Posts.GetExplorePagePosts;
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
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostImage> PostImages { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<Saved> Saved { get; set; }
    public DbSet<ShoppingList> ShoppingLists { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserFollower> UserFollowers { get; set; }
    public DbSet<SeenPost> SeenPosts { get; set; }
    public DbSet<TagCategory> TagCategories { get; set; }
    public DbSet<QuizQuestion> QuizQuestions { get; set; }
    public DbSet<QuizAnswer> QuizAnswers { get; set; }
    
    public DbSet<AppEventLog> AppEventLogs { get; set; }
    
    public DbSet<ErrorLog> ErrorLogs { get; set; }
    
    public DbSet<HttpLog> HttpLogs { get; set; }
    
    public DbSet<RecommendPostResultDTO> RecommendPosts { get; set; }
    
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
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<RecommendPostResultDTO>().HasNoKey();
    
    modelBuilder.Entity<UserFollower>()
        .HasKey(uf => new { uf.FollowerId, uf.FollowingId });

    modelBuilder.Entity<UserFollower>()
        .HasOne(uf => uf.Follower)
        .WithMany(u => u.Followings)
        .HasForeignKey(uf => uf.FollowerId)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<UserFollower>()
        .HasOne(uf => uf.Following)
        .WithMany(u => u.Followers)
        .HasForeignKey(uf => uf.FollowingId)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<Post>()
        .HasMany(p => p.PostImages)
        .WithOne(pi => pi.Post)
        .HasForeignKey(pi => pi.PostId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Post>()
        .HasMany(p => p.PostTags)
        .WithOne(pt => pt.Post)
        .HasForeignKey(pt => pt.PostId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Post>()
        .HasMany(p => p.Steps)
        .WithOne(s => s.Post)
        .HasForeignKey(s => s.PostId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<IngredientPost>(entity =>
    {
        entity.HasKey(ip => new { ip.IngredientId, ip.PostId });

        entity.HasOne(ip => ip.Post)
              .WithMany(p => p.IngredientPosts)
              .HasForeignKey(ip => ip.PostId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(ip => ip.Ingredient)
              .WithMany(i => i.IngredientPosts)
              .HasForeignKey(ip => ip.IngredientId)
              .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<Post>()
        .HasMany(p => p.Likes)
        .WithOne(l => l.Post)
        .HasForeignKey(l => l.PostId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Post>()
        .HasMany(p => p.Comments)
        .WithOne(c => c.Post)
        .HasForeignKey(c => c.PostId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Post>()
        .HasMany(p => p.Saved)
        .WithOne(s => s.Post)
        .HasForeignKey(s => s.PostId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<IngredientShoppingList>()
        .HasKey(isl => new { isl.IngredientId, isl.ShoppingListId });

    modelBuilder.Entity<IngredientShoppingList>()
        .HasOne(isl => isl.Ingredient)
        .WithMany(i => i.IngredientShoppingLists)
        .HasForeignKey(isl => isl.IngredientId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<IngredientShoppingList>()
        .HasOne(isl => isl.ShoppingList)
        .WithMany(sl => sl.IngredientShoppingLists)
        .HasForeignKey(isl => isl.ShoppingListId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<PostTag>()
        .HasOne(pt => pt.Tag)
        .WithMany(t => t.PostTags)
        .HasForeignKey(pt => pt.TagId)
        .OnDelete(DeleteBehavior.Cascade);
    
   }
}
