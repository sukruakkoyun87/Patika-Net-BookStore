using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.Queries.BookOpreations.DetailBook;
using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.BookOperations.Queries.GetDetailsBook
{
    public class GetBookDetailsQueryValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly  IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookDetailsQueryValidatorTests(CommonTestFixture fixture)
        {
            _mapper = fixture.Mapper;
            _dbContext = fixture.Context;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-11)]
        public void WhenInvalidNotIdGiven_Validator_ShouldBeReturnError(int id)
        {
            GetDetailBookQuery query = new GetDetailBookQuery(_dbContext,_mapper);
            query.BookId = id;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);


            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenValidIdGiven_Validator_ShouldBeReturn(int id)
        {
            GetDetailBookQuery query = new GetDetailBookQuery(_dbContext, _mapper);
            query.BookId = id;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);


            result.Errors.Count.Should().Be(0);
        }
    }
}
