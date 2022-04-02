using WebAPI.DBOperations;

namespace WebAPI.Application.Command.BookOpreations.DeleteBook
{
    public class DeleteBookCommand
    {

    public int BookId { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle(){
            var bookToDelete = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        if(bookToDelete is null)
        {
            throw new InvalidOperationException("Silinecek kitap Bulunamadı");
        }
            _dbContext.Books.Remove(bookToDelete);
            _dbContext.SaveChanges();
    }

}
}