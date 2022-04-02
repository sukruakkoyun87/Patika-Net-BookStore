using FluentAssertions;
using TestsSetup;
using WebAPI.Application.AuthorOperations.Queries.GetAuthorsDetails;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Queries.GetDetailsAuthor;

public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0)]
    [InlineData(-50)]

    public void WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnErrors(int id)
    {
        //arrange
        GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(null, null);
        query.AuthorId = id;

        //act
        GetAuthorDetailsQueryValidator validator = new GetAuthorDetailsQueryValidator();
        var result = validator.Validate(query);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]

    public void WhenValidAuthorIdIsGiven_Validator_ShouldNotBeReturnErrors(int id)
    {
        //arrange
        GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(null, null);
        query.AuthorId = id;

        //act
        GetAuthorDetailsQueryValidator validator = new GetAuthorDetailsQueryValidator();
        var result = validator.Validate(query);

        //assert
        result.Errors.Count.Should().Be(0);
    }
}