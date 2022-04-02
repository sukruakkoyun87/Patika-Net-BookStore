using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.Command.BookOpreations.CreateBook;
using WebAPI.DBOperations;
using WebAPI.Entities;
using Xunit;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture fixture)
        {   
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenAllreadyExistBookTitleisGiven_InvalidOpreationException_ShouldBeReturn()
        {
            //Arrange  (Hazırlık)
            var book = new Book()
            {
                Title = "WhenAllreadyExistBookTitleisGiven_InvalidOpreationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new DateTime(1990, 1, 10),
                GenreId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel()
            {
                Title = book.Title
            };
            //Act & Assert (Dogrulama) (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten Mevcut.");



        }


        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeCreated()
        {
            //Arrange  (Hazırlık)
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
           CreateBookModel model= new CreateBookModel()
            {
                Title = "Hobbit",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 1
            };
            command.Model=model;
            //Act  (Çalıştırma)

            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var book =_context.Books.SingleOrDefault(x => x.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }

    }

}