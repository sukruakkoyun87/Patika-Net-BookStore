using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genreList = _dbContext.Genres.Where(x => x.isActive).OrderBy(x => x.Id);
            List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genreList);

            return vm;
        }



    }
    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}


