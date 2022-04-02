using FluentValidation;
namespace WebAPI.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x=>x.Model.Name).MinimumLength(4).When(x=>x.Model.Name.Trim() != string.Empty);
        }
    }
}