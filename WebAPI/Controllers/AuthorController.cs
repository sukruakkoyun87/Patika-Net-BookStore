using Microsoft.AspNetCore.Mvc;
using WebAPI.DBOperations;
using AutoMapper;
using FluentValidation;
using WebAPI.Application.AuthorOperations.Queries.GetAuthors;
using WebAPI.Application.AuthorOperations.Queries.GetAuthorsDetails;
using WebAPI.Application.AuthorOperations.Command.CreateAuthor;
using WebAPI.Application.AuthorOperations.Command.UpdateAuthor;
using WebAPI.Application.AuthorOperations.Command.DeleteAuthor;


namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]s")]
public class AuthorController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;



    public AuthorController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);

    }

    [HttpGet("{id}")]
    public IActionResult GetAuthorDetails(int id)
    {
        AuthorsDetailViewModel result;

        GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(_context, _mapper);
        query.AuthorId = id;
        GetAuthorDetailsQueryValidator validator = new GetAuthorDetailsQueryValidator();
        validator.ValidateAndThrow(query);
        result = query.Handle();
        return Ok(result);

    }



    [HttpPost]
    public IActionResult AddBook([FromBody] CreateAuthorModel newauthhor)
    {
        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        // try
        // {
        command.Model = newauthhor;
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
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
    public IActionResult UpdateBook(int id, [FromBody] UpdateAuthorModel updateAuthor)
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

        command.AuthorId = id;
        command.Model = updateAuthor;

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);

        command.AuthorId = id;
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

}

