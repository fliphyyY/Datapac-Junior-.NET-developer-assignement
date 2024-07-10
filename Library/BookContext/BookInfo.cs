using System.Runtime.CompilerServices;
using Library.Database;
using Library.Models;

namespace Library.BookContext
{
    public class BookInfo : IBookInfo
    {
        private readonly IBookCollectionGateway myBookCollectionGateway;

        public BookInfo(IBookCollectionGateway bookCollectionGateway)
        {
            myBookCollectionGateway = bookCollectionGateway;
        }
        public async Task<Book> GetBook(int id)
        {
           return await myBookCollectionGateway.FetchBook(id);
        }
    }
}
