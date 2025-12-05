namespace Yumsy_Backend.Features.Quiz.GetQuizResult;

public record GetQuizResultResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public string Username { get; set; }
    public int CookingTime { get; set; }
}