using System.Runtime.CompilerServices;
using Library.Database;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.BookContext
{
    public class BookInfo : IBookInfo
    {
        private readonly IBookCollectionGateway myBookCollectionGateway;

        public BookInfo(IBookCollectionGateway bookCollectionGateway)
        {
            myBookCollectionGateway = bookCollectionGateway;
        }

        public async Task<bool> CreateBook(BookData bookData)
        {
            var book = new Book()
            {
                Title = bookData.Title,
                Author = bookData.Author,
                Available = true
            };

            return await myBookCollectionGateway.CreateBook(book);
        }

        public async Task<Book> GetBook(int id)
        {
           return await myBookCollectionGateway.FetchBook(id);
        }

        public async Task<bool> UpdateBook(BookUpdateDto bookUpdate)
        {
            return await myBookCollectionGateway.UpdateBook(bookUpdate);
        }
    }
}
