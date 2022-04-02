using FluentValidation;
using WebAPI.Application.AuthorOperations.Command.UpdateAuthor;

namespace WebAPI.Application.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).MinimumLength(3).When(x => x.Model.FirstName.Trim() != string.Empty);
            RuleFor(command => command.Model.LastName).MinimumLength(3).When(x => x.Model.LastName.Trim() != string.Empty);
            
        }
    }
}