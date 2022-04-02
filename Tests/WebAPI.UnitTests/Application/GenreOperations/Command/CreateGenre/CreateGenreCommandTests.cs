using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.GenreOperations.Command.CreateGenre;
using WebAPI.DBOperations;
using WebAPI.Entities;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Command.CreateGenre;

public class CreateGenreCommandTests:IClassFixture<CommonTestFixture>
{
    private readonly IBookStoreDbContext _dbContext;
    private  readonly  IMapper _mapper;

    public CreateGenreCommandTests(CommonTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void WhenAllreadyExistGenreNameGiven_InvalidOPreationException_ShouldBeReturn()
    {
        var genre = new Genre {Name = "Note", isActive = true};

        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
        command.Model = new CreateGenreModel {Name = genre.Name};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Kitap Türü zaten Mevcut");

    }

    [Fact]
    public void WhenValidInputGiven_Genre_ShouldBeCreated()
    {
        CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);

        CreateGenreModel model = new CreateGenreModel()
        {
            Name = "Note"
        };
        command.Model = model;

        FluentActions.Invoking(()=>command.Handle()).Invoke();

        var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == model.Name);
        genre.Should().NotBeNull();
        genre.Name.Should().Be(model.Name);
    }
}