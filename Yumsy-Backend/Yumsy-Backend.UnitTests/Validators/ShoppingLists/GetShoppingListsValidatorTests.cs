using FluentValidation.TestHelper;
using Yumsy_Backend.Features.ShoppingLists.GetShoppingLists;

namespace Yumsy_Backend.UnitTests.Validators.ShoppingLists;

public class GetShoppingListsValidatorTests
{
    private readonly GetShoppingListsValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = new GetShoppingListsRequest { UserId = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_NotHaveError_When_UserIdIsValid()
    {
        var request = new GetShoppingListsRequest { UserId = Guid.NewGuid() };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
