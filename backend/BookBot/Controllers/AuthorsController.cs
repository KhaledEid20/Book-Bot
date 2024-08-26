using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IUnitOfWork _unit { get; set; }
        public AuthorsController(IUnitOfWork unit)
        {
            _unit = unit;
        }
        #region GetContollers
        [HttpGet("GetAllAuthors")]
        public async Task<ActionResult<List<Author>>> returnAllAuthors()
        {
            return Ok(await _unit.author.ReturnAllAuthors());
        }
        [HttpGet("GetAuthor")]
        public async Task<ActionResult<List<Author>>> getAuthor(string s)
        {
            return Ok(await _unit.author.AuthorSearch(s));
        }
        [HttpGet("Author'sBooks")]
        public async Task<ActionResult<List<BookDto>>> AuthorsBook(int id)
        {
            return Ok(await _unit.author.AuthorBooks(id));
        }
        #endregion
        #region PostControllers  
        [HttpPost("AddAuthor")]
        public async Task<ActionResult<string>> addAuthor(AuthorDTO newAuthor)
        {
            return Ok(await _unit.author.AddAuthor(newAuthor));
        }
        #endregion
        #region DeleteMethods
        [HttpDelete("DeleteMethod")]
        public async Task<ActionResult<string>> DeleteAuthor(int id)
        {
            return Ok(await _unit.author.DeleteAuthor(id));
        }
        #endregion
    }
}
