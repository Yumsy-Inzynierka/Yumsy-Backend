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
            .CountAsync(qa => request.Body.QuizAnswersIds.Contains(qa.Id), cancellationToken);

        if (existingCount != request.Body.QuizAnswersIds.Count())
            throw new KeyNotFoundException("One or more quiz answers not found.");
        
        var tagsIds = _dbContext.QuizAnswers
            .Where(qa => request.Body.QuizAnswersIds.Contains(qa.Id))
            .Select(qa => qa.TagId).ToList();
        
        var matchedPost = await _dbContext.Posts
            .Include(p => p.PostImages)
            .Where(p => p.CookingTime <= request.Body.MaxCookingTime && p.CookingTime >= request.Body.MinCookingTime)
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
        
        if (matchedPost == null)
        {
            matchedPost = await _dbContext.Posts
                .OrderByDescending(p => p.LikesCount)
                .FirstOrDefaultAsync();
        }
        
        return new GetQuizResultResponse()
        {
            Id = matchedPost.Id,
            Title = matchedPost.Title,
            Username = await _dbContext.Users
                .Where(u => u.Id == matchedPost.UserId)
                .Select(u => u.Username)
                .FirstOrDefaultAsync(),
            Image = matchedPost.PostImages.Select(pi => pi.ImageUrl).FirstOrDefault(),
            CookingTime = matchedPost.CookingTime,
        };
    }
}