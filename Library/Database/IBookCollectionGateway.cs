using Library.Models;

namespace Library.Database
{
    public interface IBookCollectionGateway
    {
        Task<int> CreateBook(Book book);
        Task<Book> FetchBook(int id);
        Task<int> UpdateBook(Book bookUpdate);
        Task<int> ChangeAvailableStatus(int id, bool available);
        Task<int> DeleteBook(int id, Book book);
        Task<int> BorrowBook(BorrowedBook borrowedBook);

    }
}
