using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.SearchUsers;


public class SearchUsersRequest
{
    [FromQuery(Name = "query")]
    public string Query { get; init; }

    [FromQuery(Name = "page")]
    public int Page { get; init; } = 1;
}