namespace Yumsy_Backend.Features.Quiz.GetQuizQuestions;

public record GetQuizQuestionsResponse
{
    public IEnumerable<GetQuizQuestionsQuestionResponse> Questions { get; init; }
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