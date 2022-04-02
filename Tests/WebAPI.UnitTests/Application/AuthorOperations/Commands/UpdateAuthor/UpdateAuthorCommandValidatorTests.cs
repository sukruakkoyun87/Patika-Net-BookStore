using FluentAssertions;
using TestsSetup;
using WebAPI.Application.AuthorOperations.Command.UpdateAuthor;

using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidatorTests:IClassFixture<CommonTestFixture>
{
    private readonly IBookStoreDbContext _context;

    public UpdateAuthorCommandValidatorTests(CommonTestFixture fixture)
    {
        _context = fixture.Context;
    }

    [Fact]
    public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var authorId = 5;

        UpdateAuthorModel updateAuthor = new UpdateAuthorModel() { FirstName= "Na", LastName = "La" };
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        command.AuthorId = authorId;
        command.Model = updateAuthor;

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);



        result.Errors.Count.Should().BeGreaterThan(0);
    }


    [Fact]
    public void WhenValidAuthorIdGiven_Author_ShouldBeReturn()
    {
        var authorId = 1;

        UpdateAuthorModel updateAuthor = new UpdateAuthorModel() { FirstName = "Name", LastName = "LastName" };
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        command.AuthorId = authorId;
        command.Model = updateAuthor;

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);



        result.Errors.Count.Should().Be(0);

    }
}