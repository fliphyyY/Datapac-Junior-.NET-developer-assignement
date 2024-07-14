using Library.CustomResponse;
using Library.Database;
using Library.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.BookContext
{
    public class BookInfo : IBookInfo
    {
        private const bool Available = true;
        private const bool NotAvailable = false;

        private readonly IBookCollectionGateway myBookCollectionGateway;
        private readonly UserManager<User> myUserManager;

        public BookInfo(UserManager<User> userManager,IBookCollectionGateway bookCollectionGateway)
        {
            myBookCollectionGateway = bookCollectionGateway;
            myUserManager = userManager;
        }

        public async Task<ResponseHandler> CreateBook(BookData bookData)
        {
            var book = new Book()
            {
                Title = bookData.Title,
                Author = bookData.Author,
                Available = true
            };

            var result = await myBookCollectionGateway.CreateBook(book);


            return new ResponseHandler()
            {
                StatusCode = result > 0 ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError,
                Message = result > 0 ? $"The book with name `{bookData.Title}` has been successfully saved to database!" : $"Failed to save a book with the name `{book.Title}`.",
                Succeeded = result > 0
            };
        }

        public async Task<ResponseHandler> GetBook(int id)
        {
            var book = await myBookCollectionGateway.FetchBook(id);

            BookViewModel bookViewModel = null;
            if (book is not null)
            {
                  bookViewModel = new BookViewModel()
                 {
                     BookId = book.BookId,
                     Author = book.Author,
                     Title = book.Title,
                     Available = book.Available,
                 };
            }

            return new ResponseHandler()
            {
                StatusCode = book is null ? StatusCodes.Status404NotFound : StatusCodes.Status200OK,
                Message = String.Empty,
                Succeeded = book is null,
                Data = book is null ? null : bookViewModel,
            };

        }

        public async Task<ResponseHandler> UpdateBook(BookUpdateDto bookUpdate)
        {
            var book = await myBookCollectionGateway.FetchBook(bookUpdate.BookId);
            if (book is null || (bookUpdate.Author is null && bookUpdate.Title is null))
            {
                return new ResponseHandler()
                {
                    Message = $"Update of bookUpdate with id `{bookUpdate.BookId}` has failed!",
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false
                };
            }

            book.Author = bookUpdate.Author ?? book.Author;
            book.Title = bookUpdate.Title ?? book.Title;
            var result = await myBookCollectionGateway.UpdateBook(book);

            return new ResponseHandler()
            {
                StatusCode = result > 0 ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError,
                Message = result > 0 ? $"Update of bookUpdate with id `{bookUpdate.BookId}` has been successful!" : $"Update of bookUpdate with id `{bookUpdate.BookId}` has failed!",
                Succeeded = result > 0
            };
        }

        public async Task<ResponseHandler> DeleteBook(int id)
        {
            var book = await myBookCollectionGateway.FetchBook(id);
            if (book is null)
            {
                return new ResponseHandler()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"The book with id `{id}` has not been found!",
                    Succeeded = false
                };
            }
            var result = await myBookCollectionGateway.DeleteBook(id, book);

            return new ResponseHandler()
            {
                StatusCode = result > 0 ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError,
                Message = result > 0 ? $"The book with name `{book.Title}` has been deleted!" : $"The deleting of the book with name {book.Title} has failed!",
                Succeeded = result > 0
            };
        }

        public async Task<ResponseHandler> BorrowBook(BorrowedBookDto borrowedBook)
        {
            var book = await myBookCollectionGateway.FetchBook(borrowedBook.BorrowedBookId);
            if (book is null)
            {
                return new ResponseHandler()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "The book was not found!",
                    Succeeded = false
                };
            }

            if (!book.Available)
            {
                return  new ResponseHandler()
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = $"The book with name {book.Title} is not available for borrowing!",
                    Succeeded = false
                };
            }

            var user = await myUserManager.FindByIdAsync(borrowedBook.UserId.ToString());
            if (user is null)
            {
                return new ResponseHandler()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "The user does not exist!",
                    Succeeded = false
                };
            }

            var newBook = new BorrowedBook()
            {
                UserId = borrowedBook.UserId,//
                BookId = borrowedBook.BorrowedBookId,
                BorrowedDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                ReturnedDate = null
            };

            var result = await myBookCollectionGateway.BorrowBook(newBook);

            await myBookCollectionGateway.ChangeAvailableStatus(book.BookId, NotAvailable);

            return new ResponseHandler()
            {
                StatusCode = result > 0 ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError,
                Message = result > 0 ? $"The book with name {book.Title} has been borrowed!" : $"The borrowing of the book with name {book.Title} has failed!",
                Succeeded = result > 0 
            };
        }
    }
}
