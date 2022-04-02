using System;
using System.Diagnostics.Contracts;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.Command.BookOpreations.UpdateBook;
using WebAPI.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public UpdateBookCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;

        }


        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperationExeption_ShouldBeReturn()
        {
            var bookId = 4;

            UpdateBookModel updateBook = new UpdateBookModel() { Title = "title", GenreId = 1 };
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookId;
            command.Model = updateBook;

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");

        }

        [Fact]
        public  void  WhenValidBookIdIsGiven_Book_ShouldBeUpdated()
        {
            var bookId = 1;
            var updateBook = new UpdateBookModel() { Title = "Note", GenreId = 1 };
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookId;
            command.Model = updateBook;

            command.Handle();

            var book = _context.Books.SingleOrDefault(x=>x.Id==bookId);

            book.Title.Should().Be(updateBook.Title);
            book.GenreId.Should().Be(updateBook.GenreId);
        }

      

    }

}