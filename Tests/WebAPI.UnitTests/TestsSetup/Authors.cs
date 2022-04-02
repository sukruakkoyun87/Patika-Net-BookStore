using System;
using WebAPI.DBOperations;
using WebAPI.Entities;

namespace TestsSetup;

public static class Authors
{
    public static void AddAuthor(this BookStoreDbContext context)
    {
        context.Authors.AddRange(
            new Author { FirstName = "J.K.", LastName = "Rowling", BirthDate = new DateTime(1965, 7, 31) },
            new Author { FirstName = "J.R.R.", LastName = "Tolkien", BirthDate = new DateTime(1892, 3, 16) },
            new Author { FirstName = "George", LastName = "Orwell", BirthDate = new DateTime(1903, 3, 25) }
            );
    }
   
}