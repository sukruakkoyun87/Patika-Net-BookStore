using FluentValidation;

namespace WebAPI.Application.AuthorOperations.Queries.GetAuthorsDetails
{
    public class GetAuthorDetailsQueryValidator : AbstractValidator<GetAuthorDetailsQuery>
    {
        public GetAuthorDetailsQueryValidator()
        {
            RuleFor(x => x.AuthorId).GreaterThan(0);
        }
    }
}