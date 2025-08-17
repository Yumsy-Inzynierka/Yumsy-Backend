using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Posts.GetHomeFeed;

public class GetHomeFeedForUserHandler
{
    private readonly SupabaseDbContext _context;

    public GetHomeFeedForUserHandler(SupabaseDbContext context)
    {
        _context = context;
    }

    public async Task<GetHomeFeedForUserResponse> Handle(GetHomeFeedForUserRequest request, CancellationToken cancellationToken)
    {
        //dodać logikę pobierania postów dla danego użytkownika
        var userId = Guid.Parse(request.UserId);

        var posts = await _context.Posts
            .Include(p => p.CreatedBy)
            .Include(p => p.Photos)
            .OrderBy(x => Guid.NewGuid()) //pseudo-losowe wybieranie postów
            .Take(10)
            .Select(p => new GetHomeFeedForUserPostResponse()
            {
                Id = p.Id,
                PostTitle = p.Title,
                UserId = p.UserId,
                Username = p.CreatedBy.Username,
                ImageURL = p.Photos.First().ImageUrl,
                TimePosted = p.PostedDate
            })
            .ToListAsync(cancellationToken);

        return new GetHomeFeedForUserResponse()
        {
            Posts = posts
        };
    }
} 