namespace Yumsy_Backend.Features.Quiz.GetQuizQuestions;

public record GetQuizQuestionsResponse
{
    public IEnumerable<GetQuizQuestionsQuestionResponse> Questions { get; init; }
    public GetQuizQuestionsCookingTimeQuestionResponse CookingTimeQuestion { get; set; }
}

public record GetQuizQuestionsQuestionResponse
{
    public string Question { get; init; }
    public IEnumerable<GetQuizQuestionsAnswerResponse> Answers { get; init; }
}

public record GetQuizQuestionsAnswerResponse
{
    public Guid Id { get; init; }
    public string Answer { get; init; }
}

public record GetQuizQuestionsCookingTimeQuestionResponse
{
    public string Question { get; init; }
    public IEnumerable<GetQuizQuestionsCookingTimeQuestionAnswerResponse> Answers { get; init; }
}

public record GetQuizQuestionsCookingTimeQuestionAnswerResponse
{
    public string Answer { get; init; }
    public int MinCookingTime { get; init; }
    public int MaxCookingTime { get; init; }
}