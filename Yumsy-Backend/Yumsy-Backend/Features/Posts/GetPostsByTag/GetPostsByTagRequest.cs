using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.GetPostsByTag;


public class GetPostsByTagRequest
{
    [FromQuery(Name = "tag-id")]
    public Guid TagId { get; set; }
    
    [FromQuery(Name = "page")]
    public int CurrentPage { get; set; } = 1;
}