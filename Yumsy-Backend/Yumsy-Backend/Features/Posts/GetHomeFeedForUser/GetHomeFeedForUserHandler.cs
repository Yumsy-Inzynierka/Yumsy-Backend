using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Posts.GetHomeFeedForUser;

public class GetHomeFeedForUserHandler
{
    private readonly SupabaseDbContext _context;

    public GetHomeFeedForUserHandler(SupabaseDbContext context)
    {
        _context = context;
    }

    public async Task<GetHomeFeedForUserResponse> Handle(GetHomeFeedForUserRequest getHomeFeedForUserRequest, CancellationToken cancellationToken)
    {
        var posts = await _context.Posts
            .Include(p => p.CreatedBy)
            .Include(p => p.PostImages)
            .OrderBy(x => Guid.NewGuid()) //pseudo-losowe wybieranie postów
            .Take(10)
            .Select(p => new GetHomeFeedForUserPostResponse()
            {
                Id = p.Id,
                PostTitle = p.Title,
                UserId = p.UserId,
                Username = p.CreatedBy.Username,
                Image = p.PostImages.First().ImageUrl,
                TimePosted = p.PostedDate,
                LikesCount = p.LikesCount,
                CommentsCount = p.CommentsCount,
            })
            .ToListAsync(cancellationToken);

        return new GetHomeFeedForUserResponse()
        {
            Posts = posts
        };
    }
} 