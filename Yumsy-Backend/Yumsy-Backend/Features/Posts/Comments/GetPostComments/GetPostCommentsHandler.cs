using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Posts.Comments.GetPostComments;

public class GetPostCommentsHandler
{
    private readonly SupabaseDbContext _dbContext;

    public GetPostCommentsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetPostCommentsResponse> Handle(GetPostCommentsRequest request, CancellationToken cancellationToken)
    {
        var postExists = await _dbContext.Posts
            .AnyAsync(p => p.Id == request.PostId, cancellationToken);

        if (!postExists)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found.");

        var comments = await _dbContext.Comments
            .AsNoTracking()
            .Include(c => c.User)
            .Include(c => c.CommentLikes)
            .Include(c => c.ChildComments)
                .ThenInclude(cc => cc.User)
            .Include(c => c.ChildComments)
                .ThenInclude(cc => cc.CommentLikes)
            .Where(c => c.PostId == request.PostId && c.ParentCommentId == null)
            .OrderByDescending(c => c.CommentLikes.Count)
            .Take(YumsyConstants.FETCHED_COMMENTS_AMOUNT)
            .ToListAsync(cancellationToken);

        var likedCommentIds = await _dbContext.CommentLikes
            .Where(l => l.UserId == request.UserId)
            .Select(l => l.CommentId)
            .ToListAsync(cancellationToken);

        var userProfilePictureUrl = await _dbContext.Users
            .Where(u => u.Id == request.UserId)
            .Select(u => u.ProfilePicture).FirstOrDefaultAsync();
        
        var responseComments = comments.Select(c => new GetPostCommentResponse
        {
            Id = c.Id,
            Content = c.Content,
            CommentedDate = c.CommentedDate,
            UserId = c.UserId,
            Username = c.User.Username,
            UserProfilePictureUrl = c.User.ProfilePicture,
            LikesCount = c.CommentLikes.Count,
            IsLiked = likedCommentIds.Contains(c.Id),
            ParentCommentId = c.ParentCommentId,
            ChildComments = c.ChildComments
                .OrderByDescending(cc => cc.CommentLikes.Count)
                .Select(cc => new GetPostCommentResponse
                {
                    Id = cc.Id,
                    Content = cc.Content,
                    CommentedDate = cc.CommentedDate,
                    UserId = cc.UserId,
                    Username = cc.User.Username,
                    UserProfilePictureUrl = cc.User.ProfilePicture,
                    LikesCount = cc.CommentLikes.Count,
                    IsLiked = likedCommentIds.Contains(cc.Id),
                    ParentCommentId = cc.ParentCommentId,
                    ChildComments = new List<GetPostCommentResponse>()
                })
                .ToList()
        }).ToList();

        return new GetPostCommentsResponse
        {
            UserProfilePictureUrl = userProfilePictureUrl,
            Comments = responseComments
        };
    }
}
