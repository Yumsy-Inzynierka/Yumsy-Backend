using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Posts.GetPostsByTag;


public class GetPostsByTagHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetPostsByTagHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetPostsByTagResponse> Handle(GetPostsByTagRequest getPostsByTagRequest, Guid userId, 
        CancellationToken cancellationToken)
    {
        var page = getPostsByTagRequest.CurrentPage;
        var pageSize = 14;
        
        var query = _dbContext.PostTags
            .Where(pt => pt.TagId == getPostsByTagRequest.TagId)
            .Join(
                _dbContext.Posts,
                postTag => postTag.PostId,
                post => post.Id,
                (postTag, post) => post
            )
            .Select(post => new
            {
                Post = post,
                Popularity = (post.LikesCount * 1) + (post.CommentsCount * 2) + (post.SavedCount * 3)
            })
            .OrderByDescending(x => x.Popularity)
            .ThenByDescending(x => x.Post.PostedDate);
        
        int totalCount = await query.CountAsync(cancellationToken);
        
        var posts = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new GetPostByTagResponse
            {
                Id = x.Post.Id,
                Image = _dbContext.PostImages
                    .Where(pi => pi.PostId == x.Post.Id)
                    .Select(pi => pi.ImageUrl)
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        return new GetPostsByTagResponse
        {
            Posts = posts,
            CurrentPage = page,
            HasMore = page * pageSize < totalCount
        };
    }
}