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

        public async Task<Book> FetchBook(int id)
        {
            return await myLibraryDbContext.Books.FirstOrDefaultAsync(b => b.BookId == id);
            
        }
    }
}
