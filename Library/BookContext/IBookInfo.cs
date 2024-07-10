using System.Collections;
using Library.Models;

namespace Library.BookContext
{
    public interface IBookInfo
    {
         Task<Book> GetBook(int id);
    }
}
