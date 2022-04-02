using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Application.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _dbcontext;
        public int AuthorId { get; set; }

        public DeleteAuthorCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;

        }
        public void Handle(){
            var authorToDelete = _dbcontext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (authorToDelete is null)
            {
                throw new InvalidOperationException("Silinecek yazar bulunamadı");
            }
            if (_dbcontext.Books.Any(x => x.AuthorId == AuthorId))
            {
                throw new InvalidOperationException("Kitabı yayında olan bir yazar silinemez");
            }
            

            _dbcontext.Authors.Remove(authorToDelete);
            _dbcontext.SaveChanges();

        }


    }
}