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

    public async Task<GetHomeFeedForUserResponse> Handle(GetHomeFeedForUserRequest request, CancellationToken cancellationToken)
    {
        // na razie nie zmieniam bo i tak jest logika do zmiany
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
                Description = p.Description,
                CookingTime = p.CookingTime,
                Image = p.PostImages.First().ImageUrl,
                TimePosted = p.PostedDate,
                LikesCount = p.LikesCount,
                CommentsCount = p.CommentsCount,
                Tags = p.PostTags.Select(pt => new GetHomeFeedForUserPostTagResponse
                    {
                        Id = pt.Tag.Id,
                        Name = pt.Tag.Name
                    })
                    .ToList(),
                IsLiked = p.Likes.Any(l => l.UserId == p.UserId),
            })
            .ToListAsync(cancellationToken);

        return new GetHomeFeedForUserResponse()
        {
            Posts = posts
        };
    }
} 