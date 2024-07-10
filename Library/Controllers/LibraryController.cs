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
    }
}
