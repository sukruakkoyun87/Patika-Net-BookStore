using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authorList = _dbContext.Authors.OrderBy(x => x.Id);
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorList);

            return vm;
        }

    }
    public class AuthorsViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
    }
}
