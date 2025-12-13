using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Temporary.DropSeenPost;

public class DropSeenPostRequest
{
    public Guid UserId { get; set; }

}