using System;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.Command.BookOpreations.UpdateBook;
using WebAPI.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{   
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public UpdateBookCommandValidatorTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;

        }


        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var bookId = 4;

            UpdateBookModel updateBook = new UpdateBookModel() { Title = "ti", GenreId = 1 };
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookId;
            command.Model = updateBook;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);



            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidBookIdGiven_Book_ShouldBeReturn()
        {
            var bookId = 1;

            UpdateBookModel updateBook = new UpdateBookModel() { Title = "title", GenreId = 1 };
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = updateBook;

           UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
}
}