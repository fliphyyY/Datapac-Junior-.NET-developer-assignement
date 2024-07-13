using System.Collections;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.BookContext
{
    public interface IBookInfo
    {
        Task<bool> CreateBook(BookData bookData);

        Task<Book> GetBook(int id);

        Task<bool> UpdateBook(BookUpdateDto bookUpdate);
    }
}
