using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Quiz.GetQuizQuestions;

public class GetQuizQuestionsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetQuizQuestionsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetQuizQuestionsResponse> Handle(GetQuizQuestionsRequest request, CancellationToken cancellationToken)
    {
        var categories = await _dbContext.TagCategories
            .Select(x => x.Id)
            .OrderBy(r => EF.Functions.Random())
            .Take(4)
            .ToListAsync();

        var mandatoryQuestion = await _dbContext.QuizQuestions
            .Include(qq => qq.QuizAnswers)
            .Where(qq => qq.Mandatory == true)
            .FirstOrDefaultAsync();

        var questions = new List<GetQuizQuestionsQuestionResponse>();
        
        questions.Add(new GetQuizQuestionsQuestionResponse()
        {
            Question = mandatoryQuestion.Question,
            Answers = mandatoryQuestion
                .QuizAnswers.Select(x => new GetQuizQuestionsAnswerResponse()
                {
                    Id =  x.Id,
                    Answer = x.Answer,
                })
        });
        foreach (var c in categories)
        {
            var questionsFromCategory = await _dbContext.QuizQuestions
                .Include(q => q.QuizAnswers)
                .Where(q => q.TagCategoryId == c && q.Mandatory == false)
                .OrderBy(r => EF.Functions.Random())
                .FirstOrDefaultAsync();
            
            questions.Add(new GetQuizQuestionsQuestionResponse()
            {
                Question = questionsFromCategory.Question,
                Answers = questionsFromCategory
                    .QuizAnswers.Select(x => new GetQuizQuestionsAnswerResponse()
                    {
                        Id =  x.Id,
                        Answer = x.Answer,
                    })
            });
        }

        var cookingTimeQuestion = new GetQuizQuestionsCookingTimeQuestionResponse
        {
            Question = "How much time do you have?",
            Answers = new List<GetQuizQuestionsCookingTimeQuestionAnswerResponse>
            {
                new GetQuizQuestionsCookingTimeQuestionAnswerResponse
                {
                    Answer = "Barely enough time to breathe",
                    MinCookingTime = 0,
                    MaxCookingTime = 20
                },
                new GetQuizQuestionsCookingTimeQuestionAnswerResponse
                {
                    Answer = "A solid half-hour of culinary passion",
                    MinCookingTime = 20,
                    MaxCookingTime = 50
                },
                new GetQuizQuestionsCookingTimeQuestionAnswerResponse
                {
                    Answer = "I’ve got all day, let’s make it fancy",
                    MinCookingTime = 50,
                    MaxCookingTime = int.MaxValue
                }
            }
        };

        return new GetQuizQuestionsResponse()
        {
            Questions = questions,
            CookingTimeQuestion = cookingTimeQuestion
        };
    }
}