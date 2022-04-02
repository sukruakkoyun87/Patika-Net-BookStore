using System;
using System.Linq;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.AuthorOperations.Command.DeleteAuthor;
using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandTests:IClassFixture<CommonTestFixture>
{
    private readonly IBookStoreDbContext _context;

    public DeleteAuthorCommandTests(CommonTestFixture fixture)
    {
        _context = fixture.Context;
    }

    [Fact]
    public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var authorId = 10;
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = authorId;

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek yazar bulunamadı");

    }

    [Fact]
    public void WhenAuthorHasABook_InvalidOperationException_ShouldBeReturnErrors()
    {
        var authorId = 1;
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = authorId;

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Kitabı yayında olan bir yazar silinemez");
    }
}