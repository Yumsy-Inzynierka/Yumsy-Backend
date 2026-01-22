using FluentAssertions;
using Yumsy_Backend.Features.Quiz.GetQuizResult;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Quiz;

public class GetQuizResultHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_QuizAnswerDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new GetQuizResultHandler(context);
        var request = new GetQuizResultRequest
        {
            Body = new GetQuizResultRequestBody
            {
                MinCookingTime = 0,
                MaxCookingTime = 60,
                QuizAnswersIds = new[] { Guid.NewGuid() }
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("One or more quiz answers not found.");
    }

    [Fact]
    public async Task Handle_Should_ReturnPost_When_QuizAnswersExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id, cookingTime: 30);
        var category = TestDataBuilder.CreateTagCategory();
        var tag = TestDataBuilder.CreateTag(tagCategoryId: category.Id);
        var question = TestDataBuilder.CreateQuizQuestion(tagCategoryId: category.Id);
        var answer = TestDataBuilder.CreateQuizAnswer(quizQuestionId: question.Id, tagId: tag.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.TagCategories.Add(category);
        context.Tags.Add(tag);
        context.QuizQuestions.Add(question);
        context.QuizAnswers.Add(answer);
        await context.SaveChangesAsync();

        var handler = new GetQuizResultHandler(context);
        var request = new GetQuizResultRequest
        {
            Body = new GetQuizResultRequestBody
            {
                MinCookingTime = 0,
                MaxCookingTime = 60,
                QuizAnswersIds = new[] { answer.Id }
            }
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(post.Id);
    }
}
