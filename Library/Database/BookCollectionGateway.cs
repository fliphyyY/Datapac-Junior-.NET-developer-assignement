using Library.Context;
using Library.Models;
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

        public async Task<int> CreateBook(Book book)
        {
            
            await myLibraryDbContext.Books.AddAsync(book);
            return await myLibraryDbContext.SaveChangesAsync();

        }

        public async Task<Book> FetchBook(int id)
        {
            return await myLibraryDbContext.Books.FirstOrDefaultAsync(b => b.BookId == id);
            
        }

        public async Task<int> UpdateBook(Book bookUpdate)
        {
                return await myLibraryDbContext.Books.Where(b => b.BookId == bookUpdate.BookId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(b => b.Title, bookUpdate.Title)
                        .SetProperty(b => b.Author, bookUpdate.Author) );
        }


        public async Task<int> ChangeAvailableStatus(int id, bool available)
        {
              await myLibraryDbContext.Books.Where(b => b.BookId == id)
                  .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.Available, available));
            return await myLibraryDbContext.SaveChangesAsync();

        }

        public async Task<int> DeleteBook(int id, Book book)
        {
                myLibraryDbContext.Books.Remove(book);
                return await myLibraryDbContext.SaveChangesAsync();
        }

        public async Task<int> BorrowBook(BorrowedBook borrowedBook)
        {
             await myLibraryDbContext.BorrowedBooks.AddAsync(borrowedBook);
             return await myLibraryDbContext.SaveChangesAsync();
        }


        public async Task<int> ReturnBook(int bookId)
        {
          return await myLibraryDbContext.BorrowedBooks.Where(b => b.BookId == bookId).ExecuteDeleteAsync();
        }

        public async Task<bool> IsBookBorrowed(int bookId)
        {
            return await myLibraryDbContext.BorrowedBooks.AnyAsync(b => b.BookId == bookId);
        }
    }
}
