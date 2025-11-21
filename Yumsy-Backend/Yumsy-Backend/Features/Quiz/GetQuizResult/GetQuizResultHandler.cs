using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Quiz.GetQuizResult;

public class GetQuizResultHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetQuizResultHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetQuizResultResponse> Handle(GetQuizResultRequest request, CancellationToken cancellationToken)
    {
        
        var matchedPosts = await _dbContext.Posts
            .Select(p => new
            {
                Post = p,
                MatchCount = p.PostTags
                    .Count(pt => request.TagsId.Contains(pt.TagId)),
            })
            .OrderByDescending(x => x.MatchCount)
            .ThenByDescending(x => x.Post.LikesCount)
            .Select(x => x.Post)
            .FirstOrDefaultAsync();
        
        return new GetQuizResultResponse();
    }
}