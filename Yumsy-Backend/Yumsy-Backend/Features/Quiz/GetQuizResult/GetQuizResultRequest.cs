using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Quiz.GetQuizResult;

public class GetQuizResultRequest
{
    public int MinCookingTime { get; init; }
    public int MaxCookingTime { get; init; }
    public IEnumerable<Guid> QuizAnswersIds { get; init; }
}