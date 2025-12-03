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
        var existingCount = await _dbContext.QuizAnswers
            .CountAsync(qa => request.QuizAnswersIds.Contains(qa.Id), cancellationToken);

        if (existingCount != request.QuizAnswersIds.Count())
            throw new KeyNotFoundException("One or more quiz answers not found.");
        
        var tagsIds = _dbContext.QuizAnswers
            .Where(qa => request.QuizAnswersIds.Contains(qa.Id))
            .Select(qa => qa.TagId).ToList();
        
        var matchedPosts = await _dbContext.Posts
            .Where(p => p.CookingTime <= request.MaxCookingTime && p.CookingTime >= request.MinCookingTime)
            .Select(p => new
            {
                Post = p,
                MatchCount = p.PostTags
                    .Count(pt => tagsIds.Contains(pt.TagId)),
            })
            .OrderByDescending(x => x.MatchCount)
            .ThenByDescending(x => x.Post.LikesCount)
            .Select(x => x.Post)
            .FirstOrDefaultAsync();
        
        return new GetQuizResultResponse();
    }
}