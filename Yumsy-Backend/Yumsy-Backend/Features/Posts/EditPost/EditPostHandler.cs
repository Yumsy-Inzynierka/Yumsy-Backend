using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.EditPost;

public class EditPostHandler
{
    private readonly SupabaseDbContext _dbContext;

    public EditPostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(EditPostRequest request, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .Include(p => p.PostImages)
            .Include(p => p.PostTags)
            .Include(p => p.IngredientPosts)
            .Include(p => p.Steps)
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException("Post not found.");

        if (post.UserId != request.Body.UserId)
            throw new UnauthorizedAccessException("User is not the owner of this post.");

        // Aktualizacja podstawowych pól
        post.Title = request.Body.Title;
        post.Description = request.Body.Description;
        post.CookingTime = request.Body.CookingTime ?? 0;

        // -----------------------
        // Synchronizacja obrazów
        // -----------------------
        var newImages = request.Body.Images.Select(i => i.Image).ToList();
        foreach (var img in post.PostImages.ToList())
        {
            if (!newImages.Contains(img.ImageUrl))
                post.PostImages.Remove(img);
        }
        foreach (var img in newImages)
        {
            if (!post.PostImages.Any(pi => pi.ImageUrl == img))
                post.PostImages.Add(new PostImage { Id = Guid.NewGuid(), ImageUrl = img, PostId = post.Id });
        }

        // -----------------------
        // Synchronizacja tagów
        // -----------------------
        var newTagIds = request.Body.Tags.Select(t => t.Id).ToList();
        foreach (var tag in post.PostTags.ToList())
        {
            if (!newTagIds.Contains(tag.TagId))
                post.PostTags.Remove(tag);
        }
        foreach (var tagId in newTagIds)
        {
            if (!post.PostTags.Any(pt => pt.TagId == tagId))
                post.PostTags.Add(new PostTag { PostId = post.Id, TagId = tagId });
        }

        // -----------------------
        // Synchronizacja składników
        // -----------------------
        var newIngredients = request.Body.Ingredients.ToList();
        foreach (var ing in post.IngredientPosts.ToList())
        {
            if (!newIngredients.Any(ni => ni.Id == ing.IngredientId))
                post.IngredientPosts.Remove(ing);
        }
        foreach (var ing in newIngredients)
        {
            var existing = post.IngredientPosts.FirstOrDefault(ip => ip.IngredientId == ing.Id);
            if (existing != null) existing.Quantity = ing.Quantity;
            else post.IngredientPosts.Add(new IngredientPost { PostId = post.Id, IngredientId = ing.Id, Quantity = ing.Quantity });
        }

        // -----------------------
        // Synchronizacja kroków
        // -----------------------
        var newSteps = request.Body.RecipeSteps.ToList();
        foreach (var step in post.Steps.ToList())
        {
            if (!newSteps.Any(ns => ns.StepNumber == step.StepNumber))
                post.Steps.Remove(step);
        }
        foreach (var step in newSteps)
        {
            var existingStep = post.Steps.FirstOrDefault(s => s.StepNumber == step.StepNumber);
            if (existingStep != null)
            {
                existingStep.Description = step.Description;
                existingStep.ImageUrl = step.Image;
            }
            else
            {
                post.Steps.Add(new Step { Id = Guid.NewGuid(), StepNumber = step.StepNumber, Description = step.Description, ImageUrl = step.Image, PostId = post.Id });
            }
        }

        // Zapis w transakcji
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
