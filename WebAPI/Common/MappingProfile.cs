using AutoMapper;
using WebAPI.Application.AuthorOperations.Command.CreateAuthor;
using WebAPI.Application.AuthorOperations.Queries.GetAuthors;
using WebAPI.Application.AuthorOperations.Queries.GetAuthorsDetails;
using WebAPI.Application.Command.BookOpreations.CreateBook;
using WebAPI.Application.GenreOperations.Command.CreateGenre;
using WebAPI.Application.GenreOperations.Queries.GetGenreDetails;
using WebAPI.Application.GenreOperations.Queries.GetGenres;
using WebAPI.Application.Queries.BookOpreations.DetailBook;
using WebAPI.Application.Queries.BookOpreations.GetBooks;
using WebAPI.Entities;


namespace WebAPI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //  Book
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailModel>().ForMember(x => x.Genre, y => y.MapFrom(z => z.Genre.Name)).ForMember(x => x.Author, y => y.MapFrom(z => z.Author.FirstName + " " + z.Author.LastName));
            CreateMap<Book, BooksViewModel>().ForMember(x => x.Genre, y => y.MapFrom(z => z.Genre.Name)).ForMember(x => x.Author, y => y.MapFrom(z => z.Author.FirstName + " " + z.Author.LastName));

            //  Genre
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            // Author
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorsDetailViewModel>();

        }
    }

}
