using System;
using System.Diagnostics.Contracts;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestsSetup;
using WebAPI.Application.AuthorOperations.Command.CreateAuthor;
using WebAPI.Application.Command.BookOpreations.CreateBook;
using WebAPI.DBOperations;
using WebAPI.Entities;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandTests:IClassFixture<CommonTestFixture>
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateAuthorCommandTests(CommonTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper= fixture.Mapper;
    }


    [Fact]
    public void WhenAllreadyExistAuthorTitleisGiven_InvalidOpreationException_ShouldBeReturn()
    {

        var author = new Author()
        {
            FirstName = "Şükrü",
            LastName = "Akkoyun",
            BirthDate = new DateTime(1990,04,12)
        };

        _context.Authors.Add(author);
        _context.SaveChanges();

        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        command.Model = new CreateAuthorModel()
        {
           FirstName  = author.FirstName,
           LastName = author.LastName,
           BirthDate = author.BirthDate
        };
        //Act & Assert (Dogrulama) (Çalıştırma)
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
    }

    [Fact]
    public void WhenValidInputAreGiven_Book_ShouldBeCreated()
    {

        

        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        CreateAuthorModel model = new CreateAuthorModel() {FirstName = "Şükrü", LastName = "Akkoyun",BirthDate = new DateTime(1988,05,22)};
        command.Model= model;

        //Act & Assert (Dogrulama) (Çalıştırma)
        FluentActions.Invoking(() => command.Handle()).Invoke();

        var author = _context.Authors.SingleOrDefault(x => x.FirstName == model.FirstName);
        author.Should().NotBeNull();
        author.LastName.Should().Be(model.LastName);
        author.BirthDate.Should().Be(model.BirthDate);

    }


}