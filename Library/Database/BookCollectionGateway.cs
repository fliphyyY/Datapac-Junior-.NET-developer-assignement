using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using Library.Context;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Database
{
    public class BookCollectionGateway : IBookCollectionGateway
    {
        private readonly LibraryDbContext myLibraryDbContext;

        public BookCollectionGateway(LibraryDbContext libraryDbContext)
        {
            myLibraryDbContext = libraryDbContext;
        }

        public async Task<bool> CreateBook(Book book)
        {
            

            var pom = await myLibraryDbContext.Books.AddAsync(book);
            var result = await myLibraryDbContext.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;

        }

        public async Task<Book> FetchBook(int id)
        {
            return await myLibraryDbContext.Books.FirstOrDefaultAsync(b => b.BookId == id);
            
        }

        public async Task<bool> UpdateBook(BookUpdateDto bookUpdate)
        {
        

            await using (var db =  myLibraryDbContext)
            {

                var book = await myLibraryDbContext.Books.FirstOrDefaultAsync(b => b.BookId == bookUpdate.BookId);
                if (book is  null || (bookUpdate.Author is null && bookUpdate.Title is null))
                {
                    return false;
                }

                book.Author = bookUpdate.Author ?? book.Author;
                book.Title = bookUpdate.Title ?? book.Title;
                var result = await db.SaveChangesAsync();

                if (result > 0)
                {
                    return true;
                }

                return false;


            }
        }
    }
}
