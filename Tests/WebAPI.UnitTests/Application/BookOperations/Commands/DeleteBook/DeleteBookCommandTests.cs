using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.Command.BookOpreations.DeleteBook;
using WebAPI.Application.Command.BookOpreations.UpdateBook;
using WebAPI.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public DeleteBookCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;

        }


        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperationExeption_ShouldBeReturn()
        {
            var bookId = 10;
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId= bookId;

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap Bulunamadı");

        }

        [Fact]
        public  void  WhenValidBookIdIsGiven_Book_ShouldBeDeleted()
        {
            var bookId = 1;
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(x => x.Id == bookId);
            book.Should().BeNull();
        }


    }

}