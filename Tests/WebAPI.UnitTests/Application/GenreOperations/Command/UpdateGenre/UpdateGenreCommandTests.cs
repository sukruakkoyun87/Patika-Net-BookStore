using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.GenreOperations.Command.UpdateGenre;
using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Command.UpdateGenre;

public class UpdateGenreCommandTests:IClassFixture<CommonTestFixture>
{
    private readonly IBookStoreDbContext _context;
    
    public UpdateGenreCommandTests(CommonTestFixture fixture)
    {
        _context = fixture.Context;
    }

    [Fact]
    public void WhenInvalidBookIdIsGiven_InvalidOpreationException_ShouldBeReturns()
    {

        var genreId = 4;
        UpdateGenreModel model = new UpdateGenreModel() {Name = "Note", isActive = true};
        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.GenreId = genreId;
        command.Model = model;


        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Kitap Türü Bulunamadı");
    }

    [Fact]
    public void WhenValidGenreIdIsGiven_Genre_ShouldBeUpdated()
    {
        var genreId = 2;

        var updateGenre = new UpdateGenreModel() { Name = "Note", isActive = true };

        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.GenreId = genreId;
        command.Model = updateGenre;

        FluentActions.Invoking(()=>command.Handle()).Invoke();
        var genre = _context.Genres.SingleOrDefault(x => x.Id == genreId);

        genre.Name.Should().Be(updateGenre.Name);
       

    }

}