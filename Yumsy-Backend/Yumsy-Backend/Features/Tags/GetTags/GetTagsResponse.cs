namespace Yumsy_Backend.Features.Tags.GetTags;

public record GetTagsResponse
{
    public List<GetTagCategoryResponse> Categories { get; set; } = new();
}

public record GetTagCategoryResponse
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public List<GetTagResponse> Tags { get; set; } = new();
}

public record GetTagResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}