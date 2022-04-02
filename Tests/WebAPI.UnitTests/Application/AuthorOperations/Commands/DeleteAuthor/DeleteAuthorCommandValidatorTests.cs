using FluentAssertions;
using TestsSetup;
using WebAPI.Application.AuthorOperations.Command.DeleteAuthor;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;


    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-50)]

        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnErrors(int id)
        {
            //arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = id;

            //act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]

        public void WhenValidAuthorIdIsGiven_Validator_ShouldNotBeReturnErrors(int id)
        {
            //arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = id;

            //act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }


