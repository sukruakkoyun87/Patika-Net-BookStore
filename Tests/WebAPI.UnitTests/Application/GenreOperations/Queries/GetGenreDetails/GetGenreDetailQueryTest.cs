using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.GenreOperations.Queries.GetGenreDetails;
using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Queries.GetGenreDetails;

public class GetGenreDetailQueryTest:IClassFixture<CommonTestFixture>
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTest(CommonTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper= fixture.Mapper;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(12)]
    [InlineData(-3)]
    public void WhenInvalidGenreIdIsGiven_Validate_ShouldBeReturnsExpected(int id)
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
        query.GenreId = id;


        FluentActions
            .Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Contain("Genre Bulunamadı");
    }


    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void WhenValidGenreIdIsGiven_Validate_ShouldBeReturns(int id)
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
        query.GenreId = id;

        GenreDetailViewModel vm = new GenreDetailViewModel();
        FluentActions.Invoking(() => vm = query.Handle()).Invoke();

        var genre = _context.Genres.SingleOrDefault(x => x.Id == id);

        vm.Name.Should().Be(genre.Name);
    }

}