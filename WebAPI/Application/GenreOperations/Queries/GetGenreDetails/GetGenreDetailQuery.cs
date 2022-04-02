using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Application.GenreOperations.Queries.GetGenreDetails
{
    public class GetGenreDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int GenreId { get; set; }
        public GetGenreDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId && x.isActive);
            if (genre is null)
            {
                throw new InvalidOperationException("Genre Bulunamadı");
            }

            GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(genre);
            return vm;
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


}