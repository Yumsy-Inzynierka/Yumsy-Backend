using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Tags.GetTags;

public class GetTagsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetTagsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetTagsResponse> Handle(GetTagsRequest getTagsRequest,
        CancellationToken cancellationToken)
    {
        var categories = await _dbContext.TagCategories
            .Include(tc => tc.Tags)
            .ToListAsync(cancellationToken);
        
        var response = new GetTagsResponse
        {
            Categories = categories.Select(category => new GetTagCategoryResponse
            {
                Id = category.Id,
                CategoryName = category.Name,
                Tags = category.Tags.Select(tag => new GetTagResponse()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                }).ToList()
            }).ToList()
        };

        return response;
    }
}