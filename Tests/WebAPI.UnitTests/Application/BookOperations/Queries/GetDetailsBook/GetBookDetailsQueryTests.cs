using System;
using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.Queries.BookOpreations.DetailBook;
using WebAPI.DBOperations;
using Xunit;

namespace Application.BookOperations.Queries.GetDetailsBook
{
    public class GetBookDetailsQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailsQueryTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;

        }

        [Theory]
        [InlineData(0)]
        [InlineData(42)]
        [InlineData(-1)]
        public void WhenInvalidBookIdIsGiven_Validate_ShouldBeReturnErrors(int id)
        {

            GetDetailBookQuery query = new GetDetailBookQuery(_context, _mapper);
            query.BookId = id;

            FluentActions
            .Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Contain("Kitap Bulunamadı");
        }

       
    }
}