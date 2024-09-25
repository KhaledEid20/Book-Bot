using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private IUnitOfWork _unit { get; set; }

        public BookController(IUnitOfWork Unit)
        {
            _unit = Unit;
        }
        #region getControllers

        [HttpGet("GetBook")]
        public async Task<ActionResult<BookDto>> GetBook(string Name, string? Pd = null)
        {
            return Ok(await _unit.book.GetBookAsync(Name, Pd));
        }

        [HttpGet("DeepSearch")]
        public async Task<ActionResult<BookDto>> SearchDeep(string Name, BookGenre genre, string Lang, string Pd = null) {
            return Ok(await _unit.book.GetBookDeepSearch(Name, genre, Lang, Pd));
        }
        [HttpGet("getByGenre")]
        public async Task<ActionResult<List<string>>> getAllbyGenre(BookGenre genre)
        {
            return Ok(await _unit.book.searchByGenre(genre));
        }
        [HttpGet("GetAllBooks")]

        public async Task<ActionResult<List<Book>>> getAllBooks()
        {
            return Ok(await _unit.book.ReturnALlBooks());
        }
        #endregion
        #region PostControllers
        [HttpPost("AddBook")]
        public async Task<ActionResult<BookDto>> AddBook([FromForm]AddBook book) {
            return Ok(await _unit.book.addBook(book));
        }
        #endregion
        #region DeleteControllers
        [HttpDelete("RemoveBook")]
        public async Task<ActionResult<string>> RemoveBook(int id)
        {
            return Ok(await _unit.book.DeleteBook(id));
        }
        #endregion
    }
}