using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Entities;

namespace WebAPI.DBOperations
{
    public class DataGenarator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Authors.AddRange(
                    new Author { FirstName = "J.K.", LastName = "Rowling", BirthDate = new DateTime(1965, 7, 31) },
                    new Author { FirstName = "J.R.R.", LastName = "Tolkien", BirthDate = new DateTime(1892, 3, 16) },
                    new Author { FirstName = "George", LastName = "Orwell", BirthDate = new DateTime(1903, 3, 25) }
                );
                context.Genres.AddRange(
                    new Genre { Name = "Personal Growth", isActive = true },
                    new Genre { Name = "Sience Fiction", isActive = true },
                    new Genre { Name = "Romance", isActive = true }
                );
                context.Books.AddRange(
                    new Book { /*Id = 1,*/ Title = "Lean Startup", GenreId = 1, AuthorId = 1, PageCount = 200, PublishDate = new DateTime(2001, 6, 12) },
                    new Book { /*Id = 2,*/ Title = "Herland", GenreId = 2, AuthorId = 2, PageCount = 250, PublishDate = new DateTime(2010, 5, 23) },
                    new Book { /*Id = 3,*/ Title = "Dune", GenreId = 1, AuthorId = 3, PageCount = 540, PublishDate = new DateTime(2002, 12, 21) }
                );
                context.SaveChanges();
            }
        }
    }
}
