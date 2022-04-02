using FluentValidation;

namespace WebAPI.Application.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommandValidator:AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(x => x.AuthorId).GreaterThan(0);
        }
    }
    
    
}