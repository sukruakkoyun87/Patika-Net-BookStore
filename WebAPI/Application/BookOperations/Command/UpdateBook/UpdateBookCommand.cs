using WebAPI.DBOperations;

namespace WebAPI.Application.Command.BookOpreations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        public int BookId { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var bookToUpdate = _dbContext.Books.FirstOrDefault(x => x.Id == BookId);

            if (bookToUpdate is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            bookToUpdate.GenreId = Model.GenreId != default ? Model.GenreId : bookToUpdate.GenreId;
            bookToUpdate.Title = Model.Title != default ? Model.Title : bookToUpdate.Title;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }

}