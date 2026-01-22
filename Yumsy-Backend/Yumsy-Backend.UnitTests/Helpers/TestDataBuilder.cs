using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.UnitTests.Helpers;

public static class TestDataBuilder
{
    public static User CreateUser(
        Guid? id = null,
        string email = "test@example.com",
        string username = "testuser",
        string? profileName = "Test User",
        string? bio = null,
        string role = "user")
    {
        return new User
        {
            Id = id ?? Guid.NewGuid(),
            Email = email,
            Username = username,
            ProfileName = profileName,
            Bio = bio,
            Role = role,
            FollowersCount = 0,
            FollowingCount = 0,
            RecipesCount = 0,
            RegistrationDate = DateTime.UtcNow
        };
    }

    public static Post CreatePost(
        Guid? id = null,
        Guid? userId = null,
        string title = "Test Recipe",
        string description = "Test Description",
        int cookingTime = 30,
        int likesCount = 0,
        int commentsCount = 0,
        int savedCount = 0)
    {
        return new Post
        {
            Id = id ?? Guid.NewGuid(),
            UserId = userId ?? Guid.NewGuid(),
            Title = title,
            Description = description,
            CookingTime = cookingTime,
            LikesCount = likesCount,
            CommentsCount = commentsCount,
            SavedCount = savedCount,
            PostedDate = DateTime.UtcNow
        };
    }

    public static Comment CreateComment(
        Guid? id = null,
        Guid? userId = null,
        Guid? postId = null,
        string content = "Test comment",
        Guid? parentCommentId = null)
    {
        return new Comment
        {
            Id = id ?? Guid.NewGuid(),
            UserId = userId ?? Guid.NewGuid(),
            PostId = postId ?? Guid.NewGuid(),
            Content = content,
            ParentCommentId = parentCommentId,
            CommentedDate = DateTime.UtcNow,
            LikesCount = 0
        };
    }

    public static Like CreateLike(
        Guid? userId = null,
        Guid? postId = null)
    {
        return new Like
        {
            UserId = userId ?? Guid.NewGuid(),
            PostId = postId ?? Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };
    }

    public static CommentLike CreateCommentLike(
        Guid? id = null,
        Guid? userId = null,
        Guid? commentId = null)
    {
        return new CommentLike
        {
            Id = id ?? Guid.NewGuid(),
            UserId = userId ?? Guid.NewGuid(),
            CommentId = commentId ?? Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Saved CreateSaved(
        Guid? userId = null,
        Guid? postId = null)
    {
        return new Saved
        {
            UserId = userId ?? Guid.NewGuid(),
            PostId = postId ?? Guid.NewGuid(),
            SavedAt = DateTime.UtcNow
        };
    }

    public static UserFollower CreateUserFollower(
        Guid? followerId = null,
        Guid? followingId = null)
    {
        return new UserFollower
        {
            FollowerId = followerId ?? Guid.NewGuid(),
            FollowingId = followingId ?? Guid.NewGuid(),
            FollowedAt = DateTime.UtcNow
        };
    }

    public static TagCategory CreateTagCategory(
        Guid? id = null,
        string name = "Test Category")
    {
        return new TagCategory
        {
            Id = id ?? Guid.NewGuid(),
            Name = name
        };
    }

    public static Tag CreateTag(
        Guid? id = null,
        string name = "Test Tag",
        Guid? tagCategoryId = null,
        string? emote = null)
    {
        return new Tag
        {
            Id = id ?? Guid.NewGuid(),
            Name = name,
            TagCategoryId = tagCategoryId ?? Guid.NewGuid(),
            Emote = emote
        };
    }

    public static Ingredient CreateIngredient(
        Guid? id = null,
        string name = "Test Ingredient",
        string? brand = null,
        string? mainCategory = null,
        decimal energyKcal = 100,
        decimal fat = 10,
        decimal carbs = 20,
        decimal proteins = 15)
    {
        return new Ingredient
        {
            Id = id ?? Guid.NewGuid(),
            Name = name,
            Brand = brand,
            MainCategory = mainCategory,
            EnergyKcal100g = energyKcal,
            Fat100g = fat,
            Carbohydrates100g = carbs,
            Proteins100g = proteins,
            SearchCount = 0
        };
    }

    public static ShoppingList CreateShoppingList(
        Guid? id = null,
        Guid? userId = null,
        string title = "Test Shopping List",
        Guid? createdFromId = null)
    {
        return new ShoppingList
        {
            Id = id ?? Guid.NewGuid(),
            UserId = userId ?? Guid.NewGuid(),
            Title = title,
            CreatedFromId = createdFromId
        };
    }

    public static IngredientShoppingList CreateIngredientShoppingList(
        Guid? ingredientId = null,
        Guid? shoppingListId = null,
        int quantity = 1)
    {
        return new IngredientShoppingList
        {
            IngredientId = ingredientId ?? Guid.NewGuid(),
            ShoppingListId = shoppingListId ?? Guid.NewGuid(),
            Quantity = quantity
        };
    }

    public static PostTag CreatePostTag(
        Guid? postId = null,
        Guid? tagId = null)
    {
        return new PostTag
        {
            PostId = postId ?? Guid.NewGuid(),
            TagId = tagId ?? Guid.NewGuid()
        };
    }

    public static IngredientPost CreateIngredientPost(
        Guid? ingredientId = null,
        Guid? postId = null,
        int quantity = 100)
    {
        return new IngredientPost
        {
            IngredientId = ingredientId ?? Guid.NewGuid(),
            PostId = postId ?? Guid.NewGuid(),
            Quantity = quantity
        };
    }

    public static TopDailyTag CreateTopDailyTag(
        Guid? id = null,
        Guid? tagId = null,
        DateOnly? date = null,
        int rank = 1)
    {
        return new TopDailyTag
        {
            Id = id ?? Guid.NewGuid(),
            TagId = tagId ?? Guid.NewGuid(),
            Date = date ?? DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)),
            Rank = rank
        };
    }

    public static TopDailyPost CreateTopDailyPost(
        Guid? id = null,
        Guid? postId = null,
        DateOnly? date = null,
        int rank = 1)
    {
        return new TopDailyPost
        {
            Id = id ?? Guid.NewGuid(),
            PostId = postId ?? Guid.NewGuid(),
            Date = date ?? DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)),
            Rank = rank
        };
    }

    public static QuizQuestion CreateQuizQuestion(
        Guid? id = null,
        string question = "Test Question",
        Guid? tagCategoryId = null,
        bool mandatory = false)
    {
        return new QuizQuestion
        {
            Id = id ?? Guid.NewGuid(),
            Question = question,
            TagCategoryId = tagCategoryId ?? Guid.NewGuid(),
            Mandatory = mandatory
        };
    }

    public static QuizAnswer CreateQuizAnswer(
        Guid? id = null,
        Guid? quizQuestionId = null,
        string answer = "Test Answer",
        Guid? tagId = null)
    {
        return new QuizAnswer
        {
            Id = id ?? Guid.NewGuid(),
            QuizQuestionId = quizQuestionId ?? Guid.NewGuid(),
            Answer = answer,
            TagId = tagId
        };
    }

    public static SeenPost CreateSeenPost(
        Guid? userId = null,
        Guid? postId = null)
    {
        return new SeenPost
        {
            UserId = userId ?? Guid.NewGuid(),
            PostId = postId ?? Guid.NewGuid()
        };
    }

    public static PostImage CreatePostImage(
        Guid? id = null,
        Guid? postId = null,
        string imageUrl = "https://example.com/image.jpg")
    {
        return new PostImage
        {
            Id = id ?? Guid.NewGuid(),
            PostId = postId ?? Guid.NewGuid(),
            ImageUrl = imageUrl
        };
    }

    public static Step CreateStep(
        Guid? id = null,
        Guid? postId = null,
        int stepNumber = 1,
        string description = "Test step description",
        string? imageUrl = null)
    {
        return new Step
        {
            Id = id ?? Guid.NewGuid(),
            PostId = postId ?? Guid.NewGuid(),
            StepNumber = stepNumber,
            Description = description,
            ImageUrl = imageUrl
        };
    }
}
