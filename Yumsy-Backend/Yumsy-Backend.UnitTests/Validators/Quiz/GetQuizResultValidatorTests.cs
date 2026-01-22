using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Quiz.GetQuizResult;

namespace Yumsy_Backend.UnitTests.Validators.Quiz;

public class GetQuizResultValidatorTests
{
    private readonly GetQuizResultValidator _validator = new();

    [Fact]
    public void Should_NotHaveError_ForAnyRequest()
    {
        var request = new GetQuizResultRequest();
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
