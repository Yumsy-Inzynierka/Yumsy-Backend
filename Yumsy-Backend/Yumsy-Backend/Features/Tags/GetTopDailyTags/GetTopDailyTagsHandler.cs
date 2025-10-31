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
        var hoursSince = -24;
        var since = DateTime.UtcNow.AddHours(hoursSince);
        const int pool = 15;

        var tagsTask = GetTags(since, cancellationToken);
        var tags = await tagsTask;

        while (tags.Count < pool)
        {
            hoursSince *= 2;
            since = DateTime.UtcNow.AddHours(hoursSince);
            tags = await GetTags(since, cancellationToken);

            // Zabezpieczenie, żeby nie cofać się w nieskończoność
            if (hoursSince < -24 * 90)
                break;
        }

        var topTags = tags
            .Take(YumsyConstants.TOP_DAILY_TAGS_AMOUNT)
            .ToList();

        return new GetTopDailyTagsResponse
        {
            Tags = topTags
        };
    }

    private async Task<List<GetTopDailyTagResponse>> GetTags(DateTime since, CancellationToken cancellationToken)
    {
        return await _dbContext.PostTags
            .Where(pt => pt.Post.PostedDate >= since)
            .GroupBy(pt => new { pt.Tag.Id, pt.Tag.Name })
            .Select(g => new GetTopDailyTagResponse
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                Count = g.Count()
            })
            .OrderByDescending(t => t.Count)
            .ToListAsync(cancellationToken);
    }
}