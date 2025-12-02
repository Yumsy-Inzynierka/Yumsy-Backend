using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Quiz.GetQuizResult;

//[Authorize]
[ApiController]
[Route("api/quizzes")]
public class GetSavedPostsController : ControllerBase
{
    private readonly GetQuizResultHandler _getQuizResultHandler;
    private readonly IValidator<GetQuizResultRequest> _validator;

    public GetSavedPostsController(GetQuizResultHandler getQuizResultHandler, IValidator<GetQuizResultRequest> validator)
    {
        _getQuizResultHandler = getQuizResultHandler;
        _validator = validator;
    }

    [HttpGet("/result")]
    public async Task<ActionResult<GetQuizResultResponse>> Handle(
        [FromRoute] GetQuizResultRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _getQuizResultHandler.Handle(request, cancellationToken);
            
        return Ok(response);
    }
}