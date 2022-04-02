using Microsoft.AspNetCore.Mvc;
using WebAPI.DBOperations;
using AutoMapper;
using FluentValidation;
using WebAPI.Application.GenreOperations.Queries.GetGenres;
using WebAPI.Application.GenreOperations.Queries.GetGenreDetails;
using WebAPI.Application.Queries.GenreOperations.Queries.GetGenreDetails;
using WebAPI.Application.GenreOperations.Command.CreateGenre;
using WebAPI.Application.GenreOperations.Command.UpdateGenre;
using WebAPI.Application.GenreOperations.Command.DeleteCommand;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]s")]
public class GenreController : ControllerBase
{
    private readonly IBookStoreDbContext _dbcontext;
    private readonly IMapper _mapper;

    public GenreController(IBookStoreDbContext dbcontext, IMapper mapper)
    {
        _dbcontext = dbcontext;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
        GetGenresQuery query = new GetGenresQuery(_dbcontext, _mapper);
        var obj = query.Handle();
        return Ok(obj);
    }

    [HttpGet("{id}")]
    public ActionResult GetGenreDetail(int id)
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(_dbcontext, _mapper);
        query.GenreId = id;
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        validator.ValidateAndThrow(query);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
    {
        CreateGenreCommand command = new CreateGenreCommand(_dbcontext, _mapper);
        command.Model = newGenre;

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();

    }

    [HttpPut("{id}")]
    public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
    {
        UpdateGenreCommand command = new UpdateGenreCommand(_dbcontext);

        command.GenreId = id;
        command.Model = updateGenre;
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGenre(int id)
    {
        DeleteGenreCommand command = new DeleteGenreCommand(_dbcontext);
        command.GenreId = id;
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }


}

