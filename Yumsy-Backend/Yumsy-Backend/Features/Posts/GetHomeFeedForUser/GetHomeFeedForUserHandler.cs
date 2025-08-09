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
        // dokonczyc logike pobierania postów z bazy danych 
        //_context.get...

        //dodać logikę pobierania postów dla danego użytkownika
        var userId = Guid.Parse(request.UserId);

        var posts = await _context.Posts
            .Include(p => p.CreatedByNavigation)
            .Include(p => p.Photos)
            .OrderBy(x => Guid.NewGuid()) //pseudo-losowe wybieranie postów
            .Take(10)
            .Select(p => new GetHomeFeedForUserPostDto()
            {
                Id = p.Id,
                PostTitle = p.Title,
                UserId = p.CreatedBy,
                Username = p.CreatedByNavigation.Username,
                ImageURL = ImageHelper.DummyImageUrl,
                TimePosted = p.PostedDate
            })
            .ToListAsync(cancellationToken);

        return new GetHomeFeedForUserResponse()
        {
            Posts = posts
        };
    }
} 