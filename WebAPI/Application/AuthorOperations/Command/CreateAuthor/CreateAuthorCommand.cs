using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entities;

namespace WebAPI.Application.AuthorOperations.Command.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext dbcontext, IMapper mapper )
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }


        public void Handle()
        {
            var newauthor = _dbcontext.Authors.SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName == Model.LastName && x.BirthDate == Model.BirthDate);
            if (newauthor is not null)
            {
                throw new InvalidOperationException("Yazar zaten mevcut");
            }

            var author = _mapper.Map<Author>(Model);
            _dbcontext.Authors.Add(author);
            _dbcontext.SaveChanges();

        }

    }

    public class CreateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
