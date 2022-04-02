using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.AuthorOperations.Queries.GetAuthorsDetails;
using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Queries.GetDetailsAuthor;

public class GetAuthorDetailQueryTests:IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(66)]
    [InlineData(42)]
    [InlineData(-5)]
    public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError(int authorId)
    {
        GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(_context, _mapper);
        query.AuthorId = authorId;

        FluentActions
            .Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Yazar Bulunamadı");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void WhenValidAuthorIdIsGiven_InvalidOperationException_ShouldNotBeReturnError(int authorId)
    {
        GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(_context, _mapper);
        query.AuthorId = authorId;

        AuthorsDetailViewModel vm = new AuthorsDetailViewModel();
        FluentActions.Invoking(() => vm = query.Handle()).Invoke();

        var author = _context.Authors.SingleOrDefault(x => x.Id == authorId);

        vm.FirstName.Should().Be(author.FirstName);
        vm.LastName.Should().Be(author.LastName);
        vm.BirthDate.Should().Be(author.BirthDate.Date.ToString());
    }

}