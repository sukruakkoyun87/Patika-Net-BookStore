using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.Command.BookOpreations.DeleteBook;
using WebAPI.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
     
        public DeleteBookCommandValidatorTest(CommonTestFixture fixture)
        {
            
            _context = fixture.Context;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidBookIdIsGiven_Validate_ShouldBeReturnErrors(int id)
        {

           DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
    
        public void WhenValidBookIdGiven_Validate_ShouldBeReturns(int id){

            
           DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            
            result.Errors.Count.Should().Be(0);

        }
    }

}