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
        var yesterday = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1));
        
        var tags = await _dbContext.TopDailyTags
            .Where(tdt => tdt.Date == yesterday)
            .OrderBy(tdt => tdt.Rank)
            .Select(tdt => new GetTopDailyTagResponse
            {
                Id = tdt.Tag.Id,
                Name = tdt.Tag.Name
            })
            .ToListAsync(cancellationToken);
        
        return new GetTopDailyTagsResponse
        {
            Tags = tags
        };
    }
}