namespace Yumsy_Backend.Features.Tags.GetTopDailyTags;
public record GetTopDailyTagsResponse
{
    public List<GetTopDailyTagResponse> Tags { get; init; }
}

public record GetTopDailyTagResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Count { get; init; }
}