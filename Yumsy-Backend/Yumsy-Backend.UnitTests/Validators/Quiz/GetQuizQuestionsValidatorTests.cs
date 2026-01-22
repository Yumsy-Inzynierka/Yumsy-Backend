using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Quiz.GetQuizQuestions;

namespace Yumsy_Backend.UnitTests.Validators.Quiz;

public class GetQuizQuestionsValidatorTests
{
    private readonly GetQuizQuestionsValidator _validator = new();

    [Fact]
    public void Should_NotHaveError_ForAnyRequest()
    {
        var request = new GetQuizQuestionsRequest();
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
