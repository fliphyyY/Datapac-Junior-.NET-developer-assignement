using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Database
{
    public interface IBookCollectionGateway
    {
        Task<bool> CreateBook(Book book);
        Task<Book> FetchBook(int id);

        Task<bool> UpdateBook(BookUpdateDto bookUpdate);

    }
}
