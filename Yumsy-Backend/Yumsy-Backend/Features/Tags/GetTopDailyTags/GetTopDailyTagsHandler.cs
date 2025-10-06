using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Tags.GetTopDailyTags;

public class GetTopDailyTagsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetTopDailyTagsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetTopDailyTagsResponse> Handle(CancellationToken cancellationToken)
    {
        var since = DateTime.UtcNow.AddHours(-24);
        
        var tags = await _dbContext.PostTags
            .Where(pt => pt.Post.PostedDate >= since)
            .GroupBy(pt => new {pt.Tag.Id, pt.Tag.Name})
            .Select(g => new GetTopDailyTagResponse
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                Count = g.Count()
            })
            .OrderByDescending(t => t.Count)
            .Take(YumsyConstants.TOP_DAILY_TAGS_AMOUNT)
            .ToListAsync(cancellationToken);
        
        return new GetTopDailyTagsResponse
        {
            Tags = tags
        };
    }
}