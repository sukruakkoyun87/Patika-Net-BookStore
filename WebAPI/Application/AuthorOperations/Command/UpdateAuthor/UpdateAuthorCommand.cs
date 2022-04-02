using AutoMapper;
using WebAPI.DBOperations;

namespace WebAPI.Application.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _dbcontext;


        public UpdateAuthorCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;

        }

        public void Handle()
        {
            var authorToUpdate = _dbcontext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (authorToUpdate is null)
            {
                throw new InvalidOperationException("Güncellenecek yazar bulunamadı");
            }

            authorToUpdate.FirstName = string.IsNullOrEmpty(Model.FirstName.Trim()) ? authorToUpdate.FirstName : Model.FirstName;
            authorToUpdate.LastName = string.IsNullOrEmpty(Model.LastName.Trim()) ? authorToUpdate.LastName : Model.LastName;


            _dbcontext.SaveChanges();

        }


    }

    public class UpdateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}