using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Quiz.GetQuizResult;

public class GetQuizResultRequest
{
    [FromBody]
    public GetQuizResultRequestBody Body { get; set; } = default!;
}
public class GetQuizResultRequestBody
{
    public int MinCookingTime { get; init; }
    public int MaxCookingTime { get; init; }
    public IEnumerable<Guid> QuizAnswersIds { get; init; }
}
