using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.GetPostDetails;

public class GetPostDetailsRequest
{
    [FromRoute]
    public Guid PostId { get; set; }
}