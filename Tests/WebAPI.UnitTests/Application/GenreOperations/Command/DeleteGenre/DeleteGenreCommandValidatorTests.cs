using FluentAssertions;
using TestsSetup;
using WebAPI.Application.GenreOperations.Command.DeleteCommand;
using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Command.DeleteGenre;

public class DeleteGenreCommandValidatorTests:IClassFixture<CommonTestFixture>
{
    private readonly IBookStoreDbContext _context;

    public DeleteGenreCommandValidatorTests(CommonTestFixture fixture)
    {
        _context = fixture.Context;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void WhenInvalidGenreIdIsGiven_Validate_ShouldBeReturnErrors(int id)
    {
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = id;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result=validator.Validate(command);


        result.Errors.Count.Should().BeGreaterThan(0);


    }


    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void WhenValidBookIdGiven_Validate_ShouldBeReturns(int id)
    {
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = id;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result= validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}