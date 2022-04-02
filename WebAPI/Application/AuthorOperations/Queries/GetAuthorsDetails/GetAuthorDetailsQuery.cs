using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Application.AuthorOperations.Queries.GetAuthorsDetails
{
    public class GetAuthorDetailsQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public GetAuthorDetailsQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public AuthorsDetailViewModel Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }

            AuthorsDetailViewModel vm = _mapper.Map<AuthorsDetailViewModel>(author);

            return vm;
        }

    }

    public class AuthorsDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
    }
}