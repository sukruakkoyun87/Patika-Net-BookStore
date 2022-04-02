using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entities;

namespace WebAPI.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(IBookStoreDbContext dbcontext, IMapper mapper = null)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public void Handle(){

            var newgenre = _dbcontext.Genres.SingleOrDefault(x => x.Name == Model.Name);

            if (newgenre is not null)
            {
                throw new InvalidOperationException("Kitap Türü zaten Mevcut");
            }
            newgenre = _mapper.Map<Genre>(Model);  //new Book();

            _dbcontext.Genres.Add(newgenre);
            _dbcontext.SaveChanges();

        }



    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}