using FluentValidation;
using WebAPI.Application.GenreOperations.Queries.GetGenreDetails;

namespace WebAPI.Application.Queries.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(x => x.GenreId).GreaterThan(0);
        }
    }
}
