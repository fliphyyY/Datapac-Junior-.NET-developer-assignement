using Library.CustomResponse;
using Library.Models;

namespace Library.BookContext
{
    public interface IBookInfo
    {
        Task<ResponseHandler> CreateBook(BookData bookData);

        Task<ResponseHandler> GetBook(int id);

        Task<ResponseHandler> UpdateBook(BookUpdateDto bookUpdate);

        Task<ResponseHandler> DeleteBook(int id);

        Task<ResponseHandler> BorrowBook(BorrowedBookDto borrowedBook);
    }
}
