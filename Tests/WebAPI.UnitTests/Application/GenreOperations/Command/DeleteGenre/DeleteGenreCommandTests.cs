using System;
using System.Linq;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.GenreOperations.Command.DeleteCommand;
using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Command.DeleteGenre;

public class DeleteGenreCommandTests:IClassFixture<CommonTestFixture>
{
    private readonly IBookStoreDbContext _context;

    public DeleteGenreCommandTests(CommonTestFixture fixture)
    {
        _context = fixture.Context;
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(10)]
    public void WhenInvalidGenreIdIsGiven_InvalidOperationExeption_ShouldBeReturn(int id)
    {

        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId=id;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Silinecek kitap Türü Bulunamadı");

    }



    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted(int id)
    {

        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var genre = _context.Genres.SingleOrDefault(x => x.Id == id);
        genre.Should().BeNull();

    }





}