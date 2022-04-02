using FluentValidation;



namespace WebAPI.Application.Queries.BookOpreations.DetailBook
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetDetailBookQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
        }
    }
}
