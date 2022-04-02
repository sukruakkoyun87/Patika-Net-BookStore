using WebAPI.DBOperations;
using WebAPI.Application.GenreOperations.Command.DeleteCommand;
using FluentValidation;

namespace WebAPI.Application.GenreOperations.Command.DeleteCommand
{
    public class DeleteGenreCommandValidator:AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(x => x.GenreId).GreaterThan(0);
        }
    }
}