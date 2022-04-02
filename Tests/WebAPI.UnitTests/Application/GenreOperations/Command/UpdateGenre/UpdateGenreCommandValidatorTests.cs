using System;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.GenreOperations.Command.UpdateGenre;
using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Command.UpdateGenre;

public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly IBookStoreDbContext _context;

    public UpdateGenreCommandValidatorTests(CommonTestFixture fixture)
    {
        _context = fixture.Context;
    }


    [Fact]

    public void WhenInvalidGenreIdIsGiven_InvalidOperationExceprion_ShouldBeReturnError()
    {

        var genreId = 6;

        var updateGenre = new UpdateGenreModel() {Name = "Not", isActive = true};

        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.GenreId = genreId;
        command.Model = updateGenre;

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result=validator.Validate(command);


        result.Errors.Count.Should().BeGreaterThan(0);

    }

    [Fact]
    public void WhenInvalidGenreNamesGiven_InvalidOperationExceprion_ShouldBeReturnError()
    {
        var genreId = 2;

        var updateGenre = new UpdateGenreModel() { Name = "Fantasy", isActive = true };

        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.GenreId = genreId;
        command.Model = updateGenre;


        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);


        result.Errors.Count.Should().Be(0);

    }
}