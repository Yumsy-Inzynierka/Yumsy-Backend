using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Posts.GetHomeFeedForUser;

public class GetHomeFeedForUserHandler
{
    private readonly SupabaseDbContext _dbContext;

    public GetHomeFeedForUserHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetHomeFeedForUserResponse> Handle(
        GetHomeFeedForUserRequest request,
        CancellationToken cancellationToken)
    {
        var recommendedPosts = await _dbContext.Database
            .SqlQueryRaw<GetRecommendedPostDTO>(@"
                SELECT 
                    post_id AS ""Id""
                FROM recommend_posts({0}, {1}, {2}, {3})",
                request.UserId, 7, 6, 3)
            .ToListAsync(cancellationToken);

        if (!recommendedPosts.Any())
        {
            return new GetHomeFeedForUserResponse
            {
                Posts = new List<GetHomeFeedForUserPostResponse>()
            };
        }

        var recommendedPostIds = recommendedPosts.Select(r => r.Id).ToList();

        var posts = await _dbContext.Posts
            .Where(p => recommendedPostIds.Contains(p.Id))
            .Select(p => new GetHomeFeedForUserPostResponse
            {
                Id = p.Id,
                PostTitle = p.Title,
                UserId = p.UserId,
                Username = p.CreatedBy.Username,
                Description = p.Description,
                CookingTime = p.CookingTime,
                
                Image = p.PostImages
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault(),

                TimePosted = p.PostedDate,
                LikesCount = p.LikesCount,
                CommentsCount = p.CommentsCount,
                IsLiked = p.Likes.Any(l => l.UserId == request.UserId),

                Tags = p.PostTags.Select(pt => new GetHomeFeedForUserPostTagResponse
                {
                    Id = pt.Tag.Id,
                    Name = pt.Tag.Name
                })
            })
            .ToListAsync(cancellationToken);

        var rnd = new Random();
        posts = posts
            .OrderBy(p => rnd.Next())
            .ToList();

        return new GetHomeFeedForUserResponse
        {
            Posts = posts
        };
    }
} 