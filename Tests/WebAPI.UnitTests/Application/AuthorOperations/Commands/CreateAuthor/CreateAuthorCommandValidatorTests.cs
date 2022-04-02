using System;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.AuthorOperations.Command.CreateAuthor;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidatorTests :IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("Ada","")]
    [InlineData("","Eda")]
    [InlineData("as","At")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnsError(string firsName, string lastName)
    {
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel()
        {
            FirstName = firsName,
            LastName = lastName,
            BirthDate = new DateTime(1988, 04, 12)
        };

        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);


        result.Errors.Count.Should().BeGreaterThan(0);
    }
}