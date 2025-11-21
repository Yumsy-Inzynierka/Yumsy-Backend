using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Quiz.GetQuizQuestions;

[Authorize]
[ApiController]
[Route("api/quizzes")]
public class GetSavedPostsController : ControllerBase
{
    private readonly GetQuizQuestionsHandler _getQuizQuestionsHandler;
    private readonly IValidator<GetQuizQuestionsRequest> _validator;

    public GetSavedPostsController(GetQuizQuestionsHandler getQuizQuestionsHandler, IValidator<GetQuizQuestionsRequest> validator)
    {
        _getQuizQuestionsHandler = getQuizQuestionsHandler;
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<GetQuizQuestionsResponse>> Handle(
        [FromRoute] GetQuizQuestionsRequest getQuizQuestionsRequest,
        CancellationToken cancellationToken)
    {
        var response = await _getQuizQuestionsHandler.Handle(getQuizQuestionsRequest, cancellationToken);
            
        return Ok(response);
    }
}