using AutoMapper;
using WebAPI.DBOperations;
using WebAPI.Entities;

namespace WebAPI.Application.Command.BookOpreations.CreateBook
{
    public class CreateBookCommand
    {   public CreateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var newbook = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (newbook is not null)
            {
                throw new InvalidOperationException("Kitap zaten Mevcut.");
            }
            newbook=_mapper.Map<Book>(Model);  //new Book();

            _dbContext.Books.Add(newbook);
            _dbContext.SaveChanges();

        }


    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}