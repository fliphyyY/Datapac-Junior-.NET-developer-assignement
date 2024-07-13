using Library.BookContext;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
            var pom = await myBookInfo.CreateBook(bookData);

            if (!pom)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to saved a book!");
            }
            return Ok(new { message = $"The book with name `{bookData.Title}` has been successfully saved to database!" });
        }

        [AllowAnonymous]
        [HttpGet("getBook" + "/{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await myBookInfo.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [AllowAnonymous]
        [HttpPost("updateBook")]
        public async Task<IActionResult> UpdateBook(BookUpdateDto bookUpdate)
        {
            var result = await myBookInfo.UpdateBook(bookUpdate);
            if (result == false)
            {
                return NotFound(new { message = $"Update of bookUpdate with id `{bookUpdate.BookId}` has failed!" });


            }
            return Ok(new { message = $"Update of bookUpdate with id `{bookUpdate.BookId}` has been successful!" });
        }
    }
}
