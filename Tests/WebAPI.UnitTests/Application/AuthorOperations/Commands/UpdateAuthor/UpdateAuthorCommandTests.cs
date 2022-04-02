using System;
using System.Linq;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.AuthorOperations.Command.UpdateAuthor;
using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandTests:IClassFixture<CommonTestFixture>
{
    private readonly IBookStoreDbContext _context;

    public UpdateAuthorCommandTests(CommonTestFixture fixture)
    {
        _context = fixture.Context;
    }


    [Fact]
    public void WhenInvalidAuthorIdIsGiven_InvalidOperationExeption_ShouldBeReturn()
    {
        var authorId = 5;

        UpdateAuthorModel updateAuthorModel = new UpdateAuthorModel() {FirstName = "Name",LastName = "LastName"};
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        command.AuthorId= authorId;
        command.Model = updateAuthorModel;


        FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek yazar bulunamadı");

    }

    [Fact]
    public void WhenValidBookIdIsGiven_Author_ShouldBeUpdated()
    {
        var authorId = 1;

        var updateAuthorModel = new UpdateAuthorModel() { FirstName = "Name", LastName = "LastName" };
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        command.AuthorId = authorId;
        command.Model = updateAuthorModel;

        command.Handle();

        var author = _context.Authors.SingleOrDefault(x => x.Id == authorId);

        author.FirstName.Should().Be(updateAuthorModel.FirstName);
        author.LastName.Should().Be(updateAuthorModel.LastName);

    }

}