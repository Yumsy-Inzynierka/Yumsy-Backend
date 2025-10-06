using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Posts.Comments.GetPostComments;

public class GetPostCommentsHandler
{
    private readonly SupabaseDbContext _dbContext;

    public GetPostCommentsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetPostCommentsResponse> Handle(GetPostCommentsRequest getPostCommentsRequest, CancellationToken cancellationToken)
    {
        var comments = await _dbContext.Comments
            .AsNoTracking()
            .Include(c => c.User)
            .Include(c => c.ChildComments)
            .ThenInclude(cc => cc.User)
            .Where(c => c.PostId == getPostCommentsRequest.PostId && c.ParentCommentId == null)
            .OrderBy(c => c.CommentLikes.Count)
            .Take(YumsyConstants.FETCHED_COMMENTS_AMOUNT)
            .ToListAsync(cancellationToken);
        
        return new GetPostCommentsResponse()
        {
            Comments = comments.Select(MapToDto).ToList()
        };
    }

    private GetPostCommentResponse MapToDto(Comment comment)
    {
        return new GetPostCommentResponse()
        {
            Id = comment.Id,
            Content = comment.Content,
            CommentedDate = comment.CommentedDate,
            UserId = comment.UserId,
            Username = comment.User.Username,
            UserProfilePictureUrl = comment.User.ProfilePicture,
            LikesCount = comment.CommentLikes.Count,
            ParentCommentId = comment.ParentCommentId,
            ChildComments = comment.ChildComments?.Select(child => new GetPostCommentResponse()
            {
                Id = child.Id,
                Content = child.Content,
                CommentedDate = child.CommentedDate,
                UserId = child.UserId,
                Username = child.User.Username,
                UserProfilePictureUrl = child.User.ProfilePicture,
                LikesCount = child.CommentLikes.Count,
                ParentCommentId = child.ParentCommentId,
                ChildComments = null
            }).ToList()
        };
    }
}