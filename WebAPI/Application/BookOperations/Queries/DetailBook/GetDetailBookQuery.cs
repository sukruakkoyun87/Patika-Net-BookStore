using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Common;
using WebAPI.DBOperations;
namespace WebAPI.Application.Queries.BookOpreations.DetailBook
{
    public class GetDetailBookQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetDetailBookQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailModel Handle(){
            var book=_dbContext.Books.Include(x=>x.Genre).Include(x=>x.Author).Where(x=>x.Id==BookId).SingleOrDefault();
            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            BookDetailModel bdm=_mapper.Map<BookDetailModel>(book);//new BookDetailModel();
        return  bdm;
    }


    }

    public class BookDetailModel{

        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }

    }
}