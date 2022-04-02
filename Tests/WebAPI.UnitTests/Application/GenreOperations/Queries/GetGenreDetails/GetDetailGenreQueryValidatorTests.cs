using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.GenreOperations.Queries.GetGenreDetails;
using WebAPI.Application.Queries.BookOpreations.DetailBook;
using WebAPI.Application.Queries.GenreOperations.Queries.GetGenreDetails;
using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Queries.GetGenreDetails;

public class GetDetailGenreQueryValidatorTests:IClassFixture<CommonTestFixture>
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetDetailGenreQueryValidatorTests(CommonTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper= fixture.Mapper;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-11)]
    public void WhenInvalidNotIdGiven_Validator_ShouldBeReturnError(int id)
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
        query.GenreId = id;

        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);


        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void WhenValidIdGiven_Validator_ShouldBeReturn(int id)
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
        query.GenreId = id;

        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);


        result.Errors.Count.Should().Be(0);
    }

}