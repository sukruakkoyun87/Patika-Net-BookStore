using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.GenreOperations.Command.CreateGenre;
using WebAPI.DBOperations;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Command.CreateGenre;

public class CreateGenreCommandValidatorTests:IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("Not")]
    [InlineData(" ")]
    [InlineData("N")]
    public void WhenInvalidInputAreGiven_Validator_SholudBeReturnsError(string name)
    {
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        command.Model = new CreateGenreModel()
        {
            Name = name,
        };

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);


        result.Errors.Count.Should().BeGreaterThan(0);

    }

    [Fact]
    public void WhenValidInputGiven_Validator_ShouldNotBeReturnError()
    {
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        command.Model = new CreateGenreModel()
        {
            Name = "Westerns"
        };

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result=validator.Validate(command);

        result.Errors.Count.Should().Be(0);


    }


}