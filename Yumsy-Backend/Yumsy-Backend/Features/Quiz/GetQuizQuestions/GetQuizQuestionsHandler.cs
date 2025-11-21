using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

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
            .Take(5)
            .ToListAsync();
        
        var questions = new List<GetQuizQuestionsQuestionResponse>();

        foreach (var c in categories)
        {
            var questionsFromCategory = await _dbContext.QuizQuestions
                .Include(q => q.QuizAnswers)
                .Where(q => q.TagCategoryId == c)
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

        return new GetQuizQuestionsResponse()
        {
            Questions = questions,
        };
    }
}