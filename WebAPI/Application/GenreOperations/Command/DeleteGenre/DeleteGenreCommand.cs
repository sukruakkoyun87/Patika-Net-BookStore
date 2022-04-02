using WebAPI.DBOperations;

namespace WebAPI.Application.GenreOperations.Command.DeleteCommand
{
    public class DeleteGenreCommand
    {

        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public DeleteGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var genreToDelete = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);

            if (genreToDelete is null)
            {
                throw new InvalidOperationException("Silinecek kitap Türü Bulunamadı");
            }
            _dbContext.Genres.Remove(genreToDelete);
            _dbContext.SaveChanges();
        }
    }
}
