
using System;
using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.Command.BookOpreations.CreateBook;
using WebAPI.DBOperations;
using WebAPI.Entities;
using Xunit;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("Lord of the Rings",0,0)]
        [InlineData("Lord of the Rings",0,1)]
        [InlineData("Lord of the Rings",100,0)]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("Lor",100,1)]
        [InlineData("Lord",100,0)]
        [InlineData("Lord",0,1)]
        [InlineData(" ",0,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnsError(string title,int PageCount ,int genreId)
        {
            // Arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = PageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };

            //Act

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);


            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnsError()
        {
            // Arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord of the Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            //Act

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

                 //Assert
            result.Errors.Count.Should().BeGreaterThan(0);

    }

    [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel()
            {
                Title = "Notebook",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };

            //Act

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //Assert
             result.Errors.Count.Should().Be(0);

    }

}
}