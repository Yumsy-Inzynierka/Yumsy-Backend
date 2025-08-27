using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Comments.AddComment;

public class Handler
{
    private readonly SupabaseDbContext _dbContext;

    public Handler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AddCommentResponse> Handle(AddCommentRequest addCommentRequest)
    {
        return new AddCommentResponse()
        {

        };
    }
}