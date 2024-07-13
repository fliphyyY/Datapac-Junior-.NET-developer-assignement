using System.Data.SqlTypes;
using System.Net;
using System.Runtime.CompilerServices;
using Library.CustomResponse;
using Library.Database;
using Library.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.BookContext
{
    public class BookInfo : IBookInfo
    {
        private readonly bool Available = true;
        private readonly bool NotAvailable = false;

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
            /* var User = new User()
             {
                 UserName = "test123@gmail.com",
                 Email = "test123@gmail.com"
             };

             var User1 = new User()
             {
                 UserName = "jan.neznamy@gmail.com",
                 Email = "jan.neznamy@gmail.com"
             };

             var User2 = new User()
             {
                 UserName = "alfonz.golias@gmail.com",
                 Email = "alfonz.golias@gmail.com"
             };
             var pom = await myUserManager.CreateAsync(User, "heslo321Aqw#");
             await myUserManager.CreateAsync(User1, "heslo321QWWQs@");
             await myUserManager.CreateAsync(User2, "heslo321QWs@");*/
            var book = await myBookCollectionGateway.FetchBook(id);
            return new ResponseHandler()
            {
                StatusCode = book is null ? StatusCodes.Status404NotFound : StatusCodes.Status200OK,
                Message = String.Empty,
                Succeeded = book is not null,
                Data = book,
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
                    Message = $"The book with was not found!",
                    Succeeded = false
                };
            }

            if (!book.Available)
            {
                return  new ResponseHandler()
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Message = $"The book with name {book.Title} is not available!",
                    Succeeded = false
                };
            }

            var newBook = new BorrowedBook()
            {
                UserId = borrowedBook.UserId,
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
                Message = result > 0 ? $"The book with name {book.Title} has been borrowed!" : $"The borrowing of the book with name {book.Title} failed!",
                Succeeded = result > 0 
            };
        }
    }
}
