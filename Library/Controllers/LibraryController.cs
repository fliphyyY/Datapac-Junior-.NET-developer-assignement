using Library.BookContext;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    public class LibraryController : ControllerBase
    {

        private readonly IBookInfo myBookInfo;

        public LibraryController(IBookInfo bookInfo)
        {
            myBookInfo = bookInfo;
        }

        [AllowAnonymous]
        [HttpPost("createBook")]
        public async Task<IActionResult> CreateBook(BookData bookData)
        {
            var result = await myBookInfo.CreateBook(bookData);
            return StatusCode(result.StatusCode, result.Message);
        }

        [AllowAnonymous]
        [HttpGet("getBook" + "/{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var result =  await myBookInfo.GetBook(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        [AllowAnonymous]
        [HttpPost("updateBook")]
        public async Task<IActionResult> UpdateBook(BookUpdateDto bookUpdate)
        {
            var result = await myBookInfo.UpdateBook(bookUpdate);
            return StatusCode(result.StatusCode, result.Message);
        }

        [AllowAnonymous]
        [HttpPost("deleteBook" + "/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await myBookInfo.DeleteBook(id);
            return StatusCode(result.StatusCode, result.Message);
        }


        [AllowAnonymous]
        [HttpPost("borrowBook")]
        public async Task<IActionResult> BorrowBook(BorrowedBookDto borrowedBook)
        {
            var result = await myBookInfo.BorrowBook(borrowedBook);
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
