using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Common;
using WebAPI.DBOperations;
namespace WebAPI.Application.Queries.BookOpreations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(x=>x.Author).Include(x=>x.Genre).OrderBy(x => x.Title).ToList();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);

            return vm;
        }


    }

    public class BooksViewModel
    {

        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }

    }
}