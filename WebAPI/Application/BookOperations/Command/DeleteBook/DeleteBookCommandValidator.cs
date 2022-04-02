using FluentValidation;


namespace WebAPI.Application.Command.BookOpreations.DeleteBook
{
    public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {  
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
