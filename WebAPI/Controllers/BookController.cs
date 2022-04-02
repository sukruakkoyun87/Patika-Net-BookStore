using Microsoft.AspNetCore.Mvc;
using WebAPI.DBOperations;
using AutoMapper;
using FluentValidation;
using WebAPI.Application.Queries.BookOpreations.GetBooks;
using WebAPI.Application.Queries.BookOpreations.DetailBook;
using WebAPI.Application.Command.BookOpreations.CreateBook;
using WebAPI.Application.Command.BookOpreations.UpdateBook;
using WebAPI.Application.Command.BookOpreations.DeleteBook;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;



    public BookController(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);

    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        BookDetailModel result;

            GetDetailBookQuery query = new GetDetailBookQuery(_context, _mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            return Ok(result);

    }



    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newbook)
    {
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        // try
        // {
            command.Model = newbook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            // if (!result.IsValid)
            // {
            //     return BadRequest(result.Errors.ForEach(x => x.PropertyName + " = " + x.ErrorMessage));
            // }else
            command.Handle();
        // }
        // catch (Exception ex)
        // {
        //     return BadRequest(ex.Message);
        // }
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
    {
        UpdateBookCommand command = new UpdateBookCommand(_context);
        
            command.BookId = id;
            command.Model = updateBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            
            command.Handle();
       
        return Ok();

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteBookCommand command = new DeleteBookCommand(_context);
       
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
      
        return Ok();
    }

}

