using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entities;

namespace WebAPI.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model { get; set; }
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _dbcontext;

        public UpdateGenreCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;

        }

        public void Handle()
        {

            var newgenre = _dbcontext.Genres.SingleOrDefault(x => x.Id == GenreId);

            if (newgenre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }
            if (_dbcontext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            {
                throw new InvalidOperationException("Bu isimde bir tür bulunmaktadır");
            }

            newgenre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ?  newgenre.Name:Model.Name;
            newgenre.isActive = Model.isActive;  //new Genre();


            _dbcontext.SaveChanges();

        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool isActive { get; set; }
    }
}