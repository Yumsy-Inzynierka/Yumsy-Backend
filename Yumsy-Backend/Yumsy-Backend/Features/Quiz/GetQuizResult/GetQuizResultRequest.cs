using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Quiz.GetQuizResult;

public class GetQuizResultRequest
{
    public IEnumerable<Guid> TagsId { get; init; }
}