using Library.Models;

namespace Library.Database
{
    public interface IBookCollectionGateway
    {
        Task<Book> FetchBook(int id);
    }
}
