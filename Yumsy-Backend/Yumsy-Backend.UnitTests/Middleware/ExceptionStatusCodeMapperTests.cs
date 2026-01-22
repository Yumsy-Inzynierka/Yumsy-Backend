using System.Security;
using FluentAssertions;
using FluentValidation;
using Yumsy_Backend.Middlewares.ExceptionHandlingMiddleware;

namespace Yumsy_Backend.UnitTests.Middleware;

public class ExceptionStatusCodeMapperTests
{
    private readonly ExceptionStatusCodeMapper _mapper = new();

    [Fact]
    public void Map_Should_Return400_ForArgumentNullException()
    {
        var exception = new ArgumentNullException("parameter");

        var result = _mapper.Map(exception);

        result.Should().Be(400);
    }

    [Fact]
    public void Map_Should_Return400_ForArgumentException()
    {
        var exception = new ArgumentException("Invalid argument");

        var result = _mapper.Map(exception);

        result.Should().Be(400);
    }

    [Fact]
    public void Map_Should_Return400_ForFormatException()
    {
        var exception = new FormatException("Invalid format");

        var result = _mapper.Map(exception);

        result.Should().Be(400);
    }

    [Fact]
    public void Map_Should_Return401_ForUnauthorizedAccessException()
    {
        var exception = new UnauthorizedAccessException("Unauthorized");

        var result = _mapper.Map(exception);

        result.Should().Be(401);
    }

    [Fact]
    public void Map_Should_Return403_ForSecurityException()
    {
        var exception = new SecurityException("Forbidden");

        var result = _mapper.Map(exception);

        result.Should().Be(403);
    }

    [Fact]
    public void Map_Should_Return404_ForKeyNotFoundException()
    {
        var exception = new KeyNotFoundException("Not found");

        var result = _mapper.Map(exception);

        result.Should().Be(404);
    }

    [Fact]
    public void Map_Should_Return409_ForInvalidOperationException()
    {
        var exception = new InvalidOperationException("Conflict");

        var result = _mapper.Map(exception);

        result.Should().Be(409);
    }

    [Fact]
    public void Map_Should_Return422_ForValidationException()
    {
        var exception = new ValidationException("Validation failed");

        var result = _mapper.Map(exception);

        result.Should().Be(422);
    }

    [Fact]
    public void Map_Should_Return500_ForUnknownException()
    {
        var exception = new Exception("Unknown error");

        var result = _mapper.Map(exception);

        result.Should().Be(500);
    }

    [Fact]
    public void Map_Should_Return500_ForNullReferenceException()
    {
        var exception = new NullReferenceException("Null reference");

        var result = _mapper.Map(exception);

        result.Should().Be(500);
    }

    [Fact]
    public void Map_Should_Return500_ForNotImplementedException()
    {
        var exception = new NotImplementedException("Not implemented");

        var result = _mapper.Map(exception);

        result.Should().Be(500);
    }

    [Theory]
    [InlineData(typeof(OutOfMemoryException))]
    [InlineData(typeof(StackOverflowException))]
    [InlineData(typeof(IndexOutOfRangeException))]
    public void Map_Should_Return500_ForSystemExceptions(Type exceptionType)
    {
        Exception exception;
        if (exceptionType == typeof(StackOverflowException))
        {
            exception = new Exception("Simulated stack overflow");
        }
        else
        {
            exception = (Exception)Activator.CreateInstance(exceptionType, "Test exception")!;
        }

        var result = _mapper.Map(exception);

        result.Should().Be(500);
    }
}
